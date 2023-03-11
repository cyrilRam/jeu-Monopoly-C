using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;


namespace Projet
{   // %%%%%%%%%%%%%%%%%%%%%%%%%% Classe Monopoly %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
    // Elle contient l'ensemble des méthodes propres au jeu du Monopoly
    // Ces méthodes sont divisés dans les sections suivantes: 
    // Méthodes Jeu ; Méthodes Action ; Sous Méthodes ActionRueGareComp; Méthodes Affichage 

    public class Monopoly
    {
        // %%%%%%%%%%%%%%%%%%%%%%%%%% Declaration des variables globales %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%

        static Pion p1 = new Pion(7, 8, '■', ConsoleColor.Red,"J1");
        static Pion p2 = new Pion(9, 8, '■', ConsoleColor.Blue,"J2");

        static List<CaseJeu> liste_caseJeu= Plateau.Creation_Jeu();
       // static List<CaseJeu> liste_caseJeu = PlateauTest.Creation_Jeu2();//pour tester sur un plateau avec des maisons

        static Caisse CaisseJ1 = new Caisse("J1",1500,16,25);
        static Caisse CaisseJ2 = new Caisse("J2",1500,16,60);

        static Caisse CaisseCommune = new Caisse("PARC",0,16,100);

        // %%%%%%%%%%%%%%%%%%%%%%%% Methodes Monopoly : Jeu %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%

        // Run : elle permet le lancement du jeu
        public static void Run()
        {
            bool isRunning = true;

            Initialize();

            do
            {
                Tour(out isRunning);


            } while (isRunning);

        }

        // Initialize : elle initialise le jeu en  affichant le plateau, les pions et les montants dispo en caisse
        public static void Initialize()
        {
            p1.DrawPion();
            p2.DrawPion();
           // PlateauTest.AffichagePlateau(liste_caseJeu);
            Plateau.AffichagePlateau(liste_caseJeu);
            Affiche3Caisses();

        }

        // Tour : méthode qui permet d'effectuer 1 tour de monopoly
        public static void Tour(out bool isRunning)
        {
            
            
            // si le joueur n'est pas en prison alors il peut lancer les dés
            if (p1.isPrison == 0)
            {
                int pas_J1 = p1.pas;

                //On lance les des
                LancerJoueur(p1);

                //Si jamais on passe par la case depart on fait +200
                CaseJeu_Dep(p1, pas_J1, CaisseJ1);

                //On affiche les 3 caisses
                Affiche3Caisses();

            }
            //Actions du jouer en fonction de la case
            ActionJoueur(p1, p2, CaisseJ1, CaisseJ2, CaisseCommune);

            CheckPartiePerdu(out isRunning);

            if (isRunning == false) { return; }

            SeRendre(p1, p2,out isRunning);
            if (isRunning == false) { return; }

            if (p2.isPrison == 0)
            {
                
                int pas_J2 = p2.pas;

                LancerJoueur(p2);

                CaseJeu_Dep(p2, pas_J2, CaisseJ2);

                Affiche3Caisses();

            }
            ActionJoueur(p2, p1, CaisseJ2, CaisseJ1, CaisseCommune);
            
            CheckPartiePerdu(out isRunning);
            if (isRunning == false) { return; }

            SeRendre(p2, p1,out isRunning);
            if (isRunning == false) { return; }
        }

        // LancerJoueur : méthode permettant au joueur i de lancer les dés et de deplacer son pion
        public static void LancerJoueur(Pion pi)
        {
            int pas;

            Console.SetCursorPosition(25, 30);
            Console.WriteLine($"{pi.Name} : Veuillez appuyer sur la touche D pour lancer les dés");
            pas = De();

                do
                {
                

                    ConsoleKeyInfo x = Console.ReadKey();

                
                    if (x.Key == ConsoleKey.D)
                    {
                        Console.SetCursorPosition(25, 31);
                        Console.WriteLine("Vous pouver avancer de {0} pas", pas);
                        Thread.Sleep(1000);
                        
                        EffaceMessage();
                        p1.DrawPion();
                        p2.DrawPion();
                        pi.GlobalMove(pas);
                        break;

                    }

                }while (true);

        }

        // De : méthode qui renvoi le résultat du lancé de 2 dés
        public static int De()
        {
            var generator = new Random(Guid.NewGuid().GetHashCode());
            return generator.Next(2, 12 + 1);
        }


        // CaseJeu_Dep : méthode qui permet de détecter lorsqu'un joueur passe par la case depart et lui ajoute
        // 200 en caisse
        public static void CaseJeu_Dep(Pion pi, int pi_pas_prec, Caisse caisse_i)
        {
            //si le pas precedent est inferieur au pas actuel ca veut dire qu'on est passé par la case depart odnc +200
            if (pi.pas < pi_pas_prec)
            {
                EffaceMessage();
                Console.SetCursorPosition(25, 30);
                Console.WriteLine($"{pi.Name} vous venez de passer la case départ. Recevez 200$");
                caisse_i.Montant += 200;
                Thread.Sleep(1000);
                Affiche3Caisses();
                Thread.Sleep(2000);

            }

        }

        // CheckPartiePerdu : méthode qui vérifie si un des 2 joueurs n'a plus d'argent et appelle la méthode
        // PartiePerdu dans ce cas de figure
        public static void CheckPartiePerdu(out bool isRunning)
        {
            isRunning = true;

            if (CaisseJ1.Montant < 0)
            {
                PartiePerdu(p1, p2);
                isRunning = false;
                return;
            }
            else if (CaisseJ2.Montant < 0)
            {
                PartiePerdu(p2, p1);
                isRunning = false;
                return;
            }

        }
        

        // PartiePerdu : méthode qui indique la fin du jeu lorsque le montant dispo en caisse d'un joueur est null
        public static void PartiePerdu(Pion pi, Pion pj)
        {
            
            EffaceMessage();
            Console.SetCursorPosition(25, 30);
            Console.WriteLine($"{pi.Name} vous n'avez plus d'argent. La partie est finie pour vous. {pj.Name} vous avez gagné. Bravo!!!!");
            Console.SetCursorPosition(25, 31);
            Thread.Sleep(3000);
        }


        // SeRendre : méthode qui permet à n'importe quel joueur de se rendre après la fin de son propre tour
        // celui qui se rend est le perdant de la partie
        public static void SeRendre(Pion pi,Pion pj,out bool isRunning)
        {
            isRunning = true;

            EffaceMessage();
            Console.SetCursorPosition(25, 30);
            Console.WriteLine($"{pi.Name} appuyez C: pour continuer, ESCAPE : pour vous rendre");
            Thread.Sleep(3000);

            


            do
            {
                

                ConsoleKeyInfo x = Console.ReadKey();

                if (x.Key== ConsoleKey.Escape)
                {
                    isRunning = false;
                    EffaceMessage();
                    Console.SetCursorPosition(25, 30);
                    Console.WriteLine($"{pi.Name} vuous avez appuyé sur la touche Escape. Vous aves decidé donc de vous rendre");
                    Thread.Sleep(3000);
                    EffaceMessage();
                    Console.SetCursorPosition(25, 30);
                    Console.WriteLine($"{pj.Name} vous avez gagné. Bravo!!!!");
                    Console.SetCursorPosition(25, 31);
                    Thread.Sleep(3000);

                    break;
                }
                else if(x.Key == ConsoleKey.C)
                {
                    break;
                }

            } while (true);
           

            
        }

        // %%%%%%%%%%%%%%%%%%%%%%%% Methodes Monopoly : Action %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%

        // ActionJouer : méthode qui fait appel aux autres méthodes action en fonction du type de la case
        public static void ActionJoueur(Pion pi, Pion pj, Caisse caisse_i, Caisse caisse_j, Caisse CaisseCommune)
        {
            EffaceMessage();

            if (liste_caseJeu[pi.pas].TypeCase == "RUE" || liste_caseJeu[pi.pas].TypeCase == "GARE" ||
                liste_caseJeu[pi.pas].TypeCase == "COMPAGNIE")
            {
                ActionRueGareComp(pi, pj, caisse_i, caisse_j);
            }

            if (liste_caseJeu[pi.pas].TypeCase == "TAXE")
            {
                ActionTaxe(pi, caisse_i, CaisseCommune);

            }
            if (liste_caseJeu[pi.pas].TypeCase == "Parc Gratuit")
            {
                ActionParcGratuit(pi, caisse_i, CaisseCommune);
            }
            if (liste_caseJeu[pi.pas].TypeCase == "GO PRISON")
            {
                GetToPrison(pi, pj);
            }
            if (liste_caseJeu[pi.pas].TypeCase == "PRISON" && pi.isPrison > 0)
            {
                ActionPrison(pi);
            }
            if (liste_caseJeu[pi.pas].TypeCase == "CHANCE" || liste_caseJeu[pi.pas].TypeCase == "CAISSE COM")
            {
                ActionCarte(pi, caisse_i, CaisseCommune);

            }
        }

        // ActionRueGareComp : elle permet l'appel des sous méthodes loyer, acheter, et ajouter maison en fonction
        // des règles du jeu. Veuillez retrouver ces sous méthodes dans la séction sous méthodes ActionRueGareComp
        // ci dessous
        public static void ActionRueGareComp(Pion pi, Pion pj, Caisse caisse_i, Caisse caisse_j)
        {
            if (liste_caseJeu[pi.pas].Proprio == " " && (liste_caseJeu[pi.pas].TypeCase == "RUE" ||
                liste_caseJeu[pi.pas].TypeCase == "GARE" || liste_caseJeu[pi.pas].TypeCase == "COMPAGNIE"))
            {
                Acheter(pi, caisse_i);
            }

            if (liste_caseJeu[pi.pas].TypeCase == "RUE")
            {
                LoyerRue(pi, caisse_i, caisse_j);

            }
            else if (liste_caseJeu[pi.pas].TypeCase == "GARE" || liste_caseJeu[pi.pas].TypeCase == "COMPAGNIE")
            {
                LoyerGareComp(pi, pj, caisse_i, caisse_j);
            }

            if (liste_caseJeu[pi.pas].TypeCase == "RUE")
            {
                AjouterMaison(pi, caisse_i);
            }

        }

        // ActionTaxe : méthode qui permet le traitement suivant : le jouer qui tombe sur la case de type taxe 
        // paye le montant de la taxe
        public static void ActionTaxe(Pion pi, Caisse caisse_i, Caisse CaisseCommune)
        {
            EffaceMessage();
            Console.SetCursorPosition(25, 30);
            Console.WriteLine($" Vous venez de tomber sur la case {liste_caseJeu[pi.pas].Name}. " +
                $"Veuillez payer {liste_caseJeu[pi.pas].Prix} à la banque");
            Thread.Sleep(2000);
            caisse_i.Montant -= liste_caseJeu[pi.pas].Prix;
            CaisseCommune.Montant += liste_caseJeu[pi.pas].Prix;
            Affiche3Caisses();       
        }


        // ActionParcGratuit : méthode qui permet le traitement suivant : le jouer qui tombe sur la case de type 
        // Parc Gratuit récupére le montant dispo en banque
        public static void ActionParcGratuit(Pion pi, Caisse caisse_i, Caisse CaisseCommune)
        {

            EffaceMessage();
            Console.SetCursorPosition(25, 30);
            Console.WriteLine($"{pi.Name} vous remporter la caisse commune d'un montant de {CaisseCommune.Montant}");
            Thread.Sleep(1000);
            caisse_i.Montant += CaisseCommune.Montant;
            CaisseCommune.Montant -= CaisseCommune.Montant;
            Thread.Sleep(2000);
            Affiche3Caisses();
        }

        // GetToPrison : méthode qui permet le traitement suivant : le jouer qui tombe sur la case de aller prison 
        // et deplacer à la case prison; de plus si un joeur est en prison et que l'adversaire tombe sur la case
        // aller prison le joeur est libéré.
        public static void GetToPrison(Pion pi, Pion pj)
        {
            if (pj.isPrison > 0)
            {
                pj.isPrison = 0;
                EffaceMessage();
                Console.SetCursorPosition(25, 30);
                Console.WriteLine($" {pj.Name} vous pouvez sortir de prison");
                Thread.Sleep(3000);
            }

            pi.isPrison = 3;
            EffaceMessage();
            Console.SetCursorPosition(25, 30);
            Console.WriteLine($" {pi.Name} vous devez aller en prison et y rester pendant {pi.isPrison} tours");
            Thread.Sleep(3000);
            pi.pas = 10;

            if (pi.Color == ConsoleColor.Red)
            {
                Console.MoveBufferArea(pi.posY, pi.posX, 1, 1,liste_caseJeu[pi.pas].col+ Console.LargestWindowWidth/30, 7);
                pi.posY = liste_caseJeu[pi.pas].col + Console.LargestWindowWidth / 30;
                pi.posX = 7;
            }
            else if (pi.Color == ConsoleColor.Blue)
            {
                Console.MoveBufferArea(pi.posY, pi.posX, 1, 1, liste_caseJeu[pi.pas].col + Console.LargestWindowWidth / 30, 9);
                pi.posY = liste_caseJeu[pi.pas].col + Console.LargestWindowWidth / 30;
                pi.posX = 9;
            }

        }

        // Action Prison : cette méthode permet le traitement suivant :le joueur qui est dans la case prison
        // il y reste pendant 3 tours
        public static void ActionPrison(Pion pi)
        {

            EffaceMessage();
            Console.SetCursorPosition(25, 30);
            Console.WriteLine($" {pi.Name} vous êtes en prison. Il vous reste encore {pi.isPrison} tours à attendre");
            Thread.Sleep(3000);
            pi.isPrison--;
        }

        // ActionCarte : méthode qui permet de tirer une carte chance ou carte caisse communautaire et réalsier
        // le traitement indiqué par la carte
        public static void ActionCarte(Pion pi, Caisse caisse_i, Caisse CaisseCommune)
        {
            string[] carte;

            if (liste_caseJeu[pi.pas].TypeCase == "CHANCE")
            {
                carte = Carte.CarteAlea(Carte.CarteChances());

            }
            else
            {
                carte = Carte.CarteAlea(Carte.CarteCaisseCom());
            }

            EffaceMessage();
            Console.SetCursorPosition(25, 30);
            Console.WriteLine($"Vous êtes tombé sur une carte {liste_caseJeu[pi.pas].Name}. Tirage de la carte...");
            Thread.Sleep(3000);

            EffaceMessage();
            Console.SetCursorPosition(25, 30);
            Console.WriteLine(carte[0]);

            Thread.Sleep(3000);

            int montant = int.Parse(carte[1]);


            if (liste_caseJeu[pi.pas].TypeCase == "CHANCE")
            {

                caisse_i.Montant += montant;
                Affiche3Caisses();
                Thread.Sleep(1000);

            }
            else
            {
                caisse_i.Montant -= montant;
                CaisseCommune.Montant += montant;
                Affiche3Caisses();
                Thread.Sleep(1000);
            }

        }

        // %%%%%%%%%%%%%%%%%%%%%%%%  Sous Methodes ActionRueGareComp %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%

        // LoyerGareComp : elle permet au joueur propiétaire de collecter le loyer lorsque son adverasiare tombe sur sa
        // gare ou compagnie
        public static void LoyerGareComp(Pion pi, Pion pj, Caisse caisse_i, Caisse caisse_j)
        {

            int[] tab = Nb_CasesJoueur(pj, liste_caseJeu[pi.pas]);
            if (liste_caseJeu[pi.pas].Proprio != " " && liste_caseJeu[pi.pas].Proprio != pi.Name)
            {

                int nb_cases_gare_j;

                if (liste_caseJeu[pi.pas].TypeCase == "GARE")
                {
                    nb_cases_gare_j = tab[2];
                }
                else
                {
                    nb_cases_gare_j = tab[3];
                }

                EffaceMessage();
                Console.SetCursorPosition(25, 30);
                Console.WriteLine($" {liste_caseJeu[pi.pas].Name} appartient à {liste_caseJeu[pi.pas].Proprio}. " +
                    $"Veuillez payer {liste_caseJeu[pi.pas].Tab_prix[nb_cases_gare_j - 1]}");
                Thread.Sleep(3000);
                caisse_i.Montant -= liste_caseJeu[pi.pas].Tab_prix[nb_cases_gare_j - 1];
                caisse_j.Montant += liste_caseJeu[pi.pas].Tab_prix[nb_cases_gare_j - 1];
                Affiche3Caisses();

            }
        }

        // LoyerRue : elle permet au joueur propiétaire de collecter le loyer lrosque son adverasiare tombe sur sa
        // propriété
        public static void LoyerRue(Pion pi, Caisse caisse_i, Caisse caisse_j)
        {
            if (liste_caseJeu[pi.pas].Proprio != " " && liste_caseJeu[pi.pas].Proprio != pi.Name)
            {


                int nb_maisons = liste_caseJeu[pi.pas].Nb_maisons;
                EffaceMessage();
                Console.SetCursorPosition(25, 30);
                Console.WriteLine($" {liste_caseJeu[pi.pas].Name} appartient à {liste_caseJeu[pi.pas].Proprio}. " +
                    $"Veuillez payer {liste_caseJeu[pi.pas].Tab_prix[nb_maisons]}");
                Thread.Sleep(3000);
                caisse_i.Montant -= liste_caseJeu[pi.pas].Tab_prix[nb_maisons];
                caisse_j.Montant += liste_caseJeu[pi.pas].Tab_prix[nb_maisons];
                Affiche3Caisses();
            }

        }

        // Acheter : elle permet l'achat d'une propriété
        public static void Acheter(Pion pi, Caisse caisse_i)
        {
            EffaceMessage();
            Console.SetCursorPosition(25, 30);
            Console.WriteLine($"{pi.Name}: Appuyer sur sur la touche A pour acheter {liste_caseJeu[pi.pas].Name} ou sur P pour " +
                                        $"passer votre tour");
            do
            {

                ConsoleKeyInfo x = Console.ReadKey();

                if (x.Key == ConsoleKey.A)
                {

                    if (caisse_i.Montant - liste_caseJeu[pi.pas].Prix > 0)
                    {
                        liste_caseJeu[pi.pas].Proprio = pi.Name;

                        caisse_i.Montant -= liste_caseJeu[pi.pas].Prix;
                        Console.SetCursorPosition(liste_caseJeu[pi.pas].col + 2, liste_caseJeu[pi.pas].ligne + 1);
                        Console.WriteLine(liste_caseJeu[pi.pas].Proprio);
                        Affiche3Caisses();

                    }
                    else
                    {
                        Console.SetCursorPosition(25, 30);
                        Console.WriteLine($"{pi.Name} vous n'avez pas assez d'argent");
                    }

                    EffaceMessage();
                    break;
                }
                else if (x.Key == ConsoleKey.P)
                {
                    EffaceMessage();
                    break;
                }
            } while (true);


        }

        // Ajouter : elle permet au proprietaire de la case de batir une maison si le joueur possède toutes les cases 
        // de cette couleur 
        // Pour ce faire on a utilisé les méthodes Nb_CasesJoueur ( que vus trouvez ci-dessous) et la méthode
        // AfficheMaison de la séction Affichage
        public static void AjouterMaison(Pion pi, Caisse caisse_i)
        {
            //s'il est proprio de cette case on recupère le nb de cases de cette couleur qu'il possède et qui existent
            if (liste_caseJeu[pi.pas].Proprio == pi.Name && liste_caseJeu[pi.pas].Nb_maisons<5 )
            {
                int[] tab = Nb_CasesJoueur(pi, liste_caseJeu[pi.pas]);
                int nb_casesCoul = tab[1];
                int nb_CasesJi = tab[0];


                //S'il possède toutes les cases de cette couleur alors il peut avoir la possi d'acheter une maison
                if (nb_casesCoul == nb_CasesJi)
                {
                    EffaceMessage();
                    Console.SetCursorPosition(25, 30);
                    Console.WriteLine($"{pi.Name}: Appuyez sur M si vous voulez ajouter une/des maisons ou P pour passer. Le prix d'une maison est de {liste_caseJeu[pi.pas].Prix_maison}");

                    ConsoleKeyInfo x = Console.ReadKey();

                    do
                    {

                        //SI appuie sur M mais au moins assez pour une maison
                        if (x.Key == ConsoleKey.M)
                        {
                            if (caisse_i.Montant - liste_caseJeu[pi.pas].Prix_maison < 0)
                            {

                                EffaceMessage();
                                Console.SetCursorPosition(25, 30);
                                Console.WriteLine($"Vous n'avez pas assez d'argent pour ajouter une maison sur " +
                                    $"{liste_caseJeu[pi.pas].Name}");
                                goto pasArgent;
                            }
                            int j;


                            EffaceMessage();
                            Console.SetCursorPosition(25, 30);
                            Console.WriteLine($"Combien de maisons voulez vous acheter");

                            //le nb de maisons doit etre entre 1 et 5-le nb de maisons deja existantes, et avoir assez d'argent
                            while (!int.TryParse(Console.ReadLine(), out j) || j < 1 || j > 5 - liste_caseJeu[pi.pas].Nb_maisons
                                || caisse_i.Montant - (liste_caseJeu[pi.pas].Prix_maison * j) < 0)
                            {
                                if (j == 0 || j < 1 || j > 5 - liste_caseJeu[pi.pas].Nb_maisons)
                                {
                                    EffaceMessage();
                                    Console.SetCursorPosition(25, 30);
                                    Console.WriteLine($"Saisi Incorrecteur. Vous devez saisir un nombre entre 1 et{5 - liste_caseJeu[pi.pas].Nb_maisons }");
                                }

                                if (caisse_i.Montant - (liste_caseJeu[pi.pas].Prix_maison * j) < 0)
                                {
                                    EffaceMessage();
                                    Console.SetCursorPosition(25, 30);
                                    Console.WriteLine($"Il vous manque {-(caisse_i.Montant - (liste_caseJeu[pi.pas].Prix_maison * j))} pour" +
                                        $" acheter {j} maisons");
                                }

                            }

                            //affichage message
                            EffaceMessage();
                            Console.SetCursorPosition(25, 30);
                            Console.WriteLine($"Vous avez placé {j} maisons");
                            Thread.Sleep(3000);

                            //Ajustement caisse et affichage
                            caisse_i.Montant -= liste_caseJeu[pi.pas].Prix_maison * j;
                            Affiche3Caisses();

                            //affichage d'une maison
                            // faire une methode
                            int n = 5 + liste_caseJeu[pi.pas].Nb_maisons * 2;

                            //Ajustement nb maisons sur la case
                            liste_caseJeu[pi.pas].Nb_maisons += j;

                            for (int i = 1; i <= j; i++)
                            {

                                AffichageMaison(liste_caseJeu[pi.pas].ligne + 1, liste_caseJeu[pi.pas].col + n, liste_caseJeu[pi.pas].couleur);
                                n += 2;
                            }

                            break;
                        }
                        else if (x.Key == ConsoleKey.P)
                        {
                            EffaceMessage();
                            break;
                        }


                    } while (true);

                pasArgent:;

                }

            }

        }

        // Nb_CasesJoueur : cette methode elle permet de déterminer le nbre case d'une coleur, le nbre de gare et compagnie
        // possédé par un jouer
        public static int[] Nb_CasesJoueur(Pion pi, CaseJeu casei)
        //Methode pour connaitre le nb de cases qu'un jouer de la même couleur possède et le nb de cases de cette coul
        {
            int[] tab = new int[4];

            int nb_cases = 0;
            int nb_cases_ji = 0;
            int nb_cases_gare_ji = 0;
            int nb_cases_compagnie_ji = 0;


            ConsoleColor couleur = casei.couleur;

            for (int i = 0; i < 40; i++)
            {
                if (liste_caseJeu[i].couleur == couleur && liste_caseJeu[i].Proprio == pi.Name)
                {
                    nb_cases_ji += 1;
                    nb_cases += 1;
                }
                else if (liste_caseJeu[i].couleur == couleur)
                {
                    nb_cases += 1;
                }
                if (liste_caseJeu[i].TypeCase == "GARE" && liste_caseJeu[i].Proprio == pi.Name)
                {
                    nb_cases_gare_ji += 1;
                }

                if (liste_caseJeu[i].TypeCase == "COMPAGNIE" && liste_caseJeu[i].Proprio == pi.Name)
                {
                    nb_cases_compagnie_ji += 1;
                }


            }

            tab[0] = nb_cases_ji;
            tab[1] = nb_cases;
            tab[2] = nb_cases_gare_ji;
            tab[3] = nb_cases_compagnie_ji;


            return tab;

        }


        // %%%%%%%%%%%%%%%%%%%%%%%% Methodes Monopoly : Affichage %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%

        // Affiche3Caisses: méthode d'affichage des montants disponibles dans les 3 caisses
        public static void Affiche3Caisses()
        {
            // effacer les 3 caisses 
            for (int i = 25; i < 130; i++)
            {
                Console.SetCursorPosition(i, 16);
                Console.WriteLine(" ");
            }

            //On affiche les 3 caisses
            CaisseJ1.Affiche();
            CaisseJ2.Affiche();
            CaisseCommune.Affiche();
        }

        // EffacerMessage : méthode premettant d'effacer les messages du jeu à la console
        public static void EffaceMessage()
        {
            for (int i = 25; i < Console.LargestWindowWidth-29; i++)
            {
                for (int j = 25; j < 50; j++)
                {
                    Console.SetCursorPosition(i, j);
                    Console.WriteLine(" ");

                }
            }
                
        }

       
        // AffichageMaison : elle permet d'afficher une maison à la console
        public static void AffichageMaison(int ligne,int colonne,ConsoleColor couleur)
        {
            Console.SetCursorPosition(colonne, ligne);

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.BackgroundColor = couleur;
            Console.Write("■");
            Console.ResetColor();
        }
       

       


        


    }

}