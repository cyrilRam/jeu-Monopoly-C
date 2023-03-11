using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet
{   // %%%%%%%%%%%%%%%%%%%%%%%% Classe Plateau %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
    // Elle permet la création et l'affichage du plateau de jeu
    public class Plateau
    {
        // Création_Jeu : méthode qui permet de de créer le plateau de jeu en le stockant dans une liste (liste_cases)
        // à partir des differents tableau contenants les parametres des cases
        public static List<CaseJeu> Creation_Jeu()
        {  
            // Initialisation des tableaux contenant les differents paramtres des cases à l'aide des méthodes
            // définies ci-dessous

            string[] tableau_noms = tab_noms();

            ConsoleColor[] tableau_couleurs = tab_Couleurs();

            string[] tableau_typesCase = tab_Types();

            int[] tableau_prix = tab_prix();

            int[][] tableau_PrixPerdu = tab_PrixPerdu();

            int[] tableau_prixMaisons = tab_PrixMaison();



            int n = 1; //compteur pour les colonnes
            int y = 11;//compteur pour les lignes

            List<CaseJeu> liste_Cases = new List<CaseJeu>(40);//creation liste des 40 cases du jeu

            // Création des cases en HAUT 
            for (int i = 0; i < 11; i++)
            {
                liste_Cases.Add(new CaseJeu(tableau_noms[i], 1, n, tableau_couleurs[i], tableau_typesCase[i], tableau_prix[i], " ",
                    tableau_PrixPerdu[i], tableau_prixMaisons[i], 0));

                n += Console.LargestWindowWidth / 11;

            }

            //Création des cases à la DROITE
            for (int i = 11; i <= 19; i++)
            {

                liste_Cases.Add(new CaseJeu(tableau_noms[i], y, n - Console.LargestWindowWidth / 11, tableau_couleurs[i],
                                tableau_typesCase[i], tableau_prix[i], " ", tableau_PrixPerdu[i], tableau_prixMaisons[i], 0));

                y += 10;

            }

            // Création des cases en BAS
            n -= Console.LargestWindowWidth / 11;

            for (int i = 20; i <= 30; i++)
            {

                liste_Cases.Add(new CaseJeu(tableau_noms[i], y, n, tableau_couleurs[i], tableau_typesCase[i], tableau_prix[i], " ",
                                tableau_PrixPerdu[i], tableau_prixMaisons[i], 0));

                n -= Console.LargestWindowWidth / 11;


            }

            // Création des cases à la GAUCHE
            y -= 10;
            for (int i = 31; i <= 39; i++)
            {

                liste_Cases.Add(new CaseJeu(tableau_noms[i], y, n + Console.LargestWindowWidth / 11, tableau_couleurs[i],
                                tableau_typesCase[i], tableau_prix[i], " ", tableau_PrixPerdu[i], tableau_prixMaisons[i], 0));
                y -= 10;


            }

            return liste_Cases;

        }


        // AffichagePlateau : méthode qui permet d'afficher le plateau de jeu à l'aide de la méthode Affichage de
        // la classe CaseJeu
        public static void AffichagePlateau(List<CaseJeu> lst)
        {
            lst.ForEach(x => x.Affichage());
        }


        // tab_noms : méthode permmettant l'initialisation des noms des différentes cases du jeu 
        public static string[] tab_noms()
        {
            string[] noms = {
                    //Cases du Haut
                    "DEPART","BOULEVARD BELLEVILLE","CAISSE COMMUNAUTE","RUE LECOURBE","IMPOT SUR REVENU","GARE MONTPARNASSE","RUE VAUGIRARD",
                    "CHANCE","RUE COURCELLES","AVENUE REPUBLIQUE","PRISON",
                    
                    //Cases à Droite
                    "BOULEVARD LA VILETTE","COMPAGNIE ELECTRICITE","AVENUE DE NEUILLY","RUE DE PARADIS","GARE DE LYON","AVENUE MOZART",
                    "CAISSE COMMUNAUTE","BOULEVARD SAINT MICHEL" ,"PLACE PIGALLE",


                    //Cases du Bas
                    "PARC GRATUIT","AVENUE MATIGNON","CHANCE","BOULEVARD MALHESERBE","AVENUE HENRI MARTIN",
                    "GARE DU NORD","FAUBOURG SAINT HONORE","RUE LA BOURSE","COMPAGNIE DES EAUX","RUE LA FAYETTE","ALLEZ PRISON",
                    

                    //Cases à Gauche
                    "AVENUE BRETEUIL","AVENUE FOCH","CAISSE COMMUNAUTE","BOULEVARD DES CAPUCINES","GARE SAINT LAZARE","CHANCE",
                    "AVENUE CHAMPS ELYSEES","TAXE" ,"RUE LA PAIX",

            };

            return noms;
        }

        //tab_Couleurs : méthode permmettant l'initialisation des couleurs des différentes cases du jeu
        public static ConsoleColor[] tab_Couleurs()
        {
            ConsoleColor[] tabCoul = {

                    //Couleurs du Haut
                     ConsoleColor.Black, ConsoleColor.Gray,ConsoleColor.Black,ConsoleColor.Gray,ConsoleColor.Black,
                    ConsoleColor.Black,ConsoleColor.Blue,ConsoleColor.Black,ConsoleColor.Blue,ConsoleColor.Blue,ConsoleColor.Black,

                
                    //Couleurs Droite
                     ConsoleColor.Magenta,ConsoleColor.Black,ConsoleColor.Magenta,ConsoleColor.Magenta,ConsoleColor.Black,ConsoleColor.DarkYellow,
                    ConsoleColor.Black,ConsoleColor.DarkYellow,ConsoleColor.DarkYellow,

                    //Couleurs du bas
                       ConsoleColor.Black, ConsoleColor.Red,ConsoleColor.Black,ConsoleColor.Red,ConsoleColor.Red,
                    ConsoleColor.Black,ConsoleColor.Yellow,ConsoleColor.Yellow,ConsoleColor.Black,ConsoleColor.Yellow,ConsoleColor.Black,
                   
                     //Couleurs Gauche
                     ConsoleColor.Green,ConsoleColor.Green,ConsoleColor.Black,ConsoleColor.Green,ConsoleColor.Black,ConsoleColor.Black,
                    ConsoleColor.DarkBlue,ConsoleColor.Black,ConsoleColor.DarkBlue,

            };

            return tabCoul;
        }

        //tab_Types : méthode permmettant l'initialisation du type des différentes cases du jeu
        public static string[] tab_Types()
        {


            string[] LstTypes =
            {
                    //Haut
                    "DEPART","RUE","CAISSE COM","RUE","TAXE","GARE","RUE","CHANCE","RUE","RUE","PRISON",
                    

                    //DROITE
                     "RUE","COMPAGNIE","RUE","RUE","GARE","RUE","CAISSE COM","RUE","RUE",
                    
                    //BAs
                    "Parc Gratuit","RUE","CHANCE","RUE","RUE","GARE","RUE","RUE","COMPAGNIE","RUE","GO PRISON",

                    //GAUCHE
                    "RUE","RUE","CAISSE COM","RUE","GARE","CHANCE","RUE","TAXE","RUE",

            };

            return LstTypes;

        }

        // tab_prix : méthode permmettant l'initialisation des prix des différentes cases du jeu
        public static int[] tab_prix()
        {
            int[] lstPrix = { 
                    //Haut
                    0,60,0,50,200,200,100,0,100,120,0,

                    //Droite
                    140,150,140,160,200,180,0,180,200,

                    //BAS
                    0, 220, 0, 220, 240, 200, 260, 260, 150, 280, 0,
                    

                    //Gauche
                    300,300,0,320,200,0,350,100,400,

            };

            return lstPrix;
        }

        // tab_PrixPerdu : méthode permmettant l'initialisation des montants à payer des différentes cases du jeu
        // lorsqu'on tombe dessus
        public static int[][] tab_PrixPerdu()
        {
            int[][] lstPrix = {
                   //Ligne Haut
                   new int[]{0},new int[]{2,10,30,90,160,250}, new int[]{0},new int[]{4,20,40,150,250,350},new int[]{0},
                   new int[]{50,100,150,200},new int[]{6,30,90,270,400,550},
                   new int[]{0},new int[]{6,30,90,270,400,550},new int[] {8,40,100,300,450,600},new int[]{0},

                    //Droite
                   new int[]{10,50,150,450,625,750},new int[]{100,200}, new int[]{10,50,150,450,625,750},new int[]{12,60,180,500,700,900},
                   new int[]{50,100,150,200},new int[]{14,70,200,550,750,950},new int[]{0},new int[]{14,70,200,550,750,950},
                   new int[]{16,80,220,600,800,100},

                    //BAS
                   new int[]{0},new int[]{18,90,250,700,875,1050}, new int[]{0},new int[]{18,90,250,700,875,1050},new int[]{20,100,300,750,920,1100 },
                   new int[]{50,100,150,200},new int[]{22,110,330,800,975,1150},new int[]{22,110,330,800,975,1150},
                   new int[]{100,200},new int[] {24,120,360,850,1025,1200},new int[]{0},
                    

                    //Gauche
                    new int[]{26,130,390,900,1100,1275},new int[]{26,130,390,900,1100,1275}, new int[]{0},new int[]{28,150,450,1000,1200,1400},
                   new int[]{50,100,150,200},new int[]{0},new int[]{35,175,500,1100,1300,1500},new int[]{0},
                   new int[]{50,200,600,1400,1700,2000}



            };

            return lstPrix;
        }

            // tab_PrixMaison : méthode permmettant l'initialisation des prix d'achat des maisons des différentes cases du jeu
        public static int[] tab_PrixMaison()
        {
            int[] lstPrix = {
                //Haut
                0,50,0,50,0,0,50,50,0,50,0,

                //Droite
                100,0,100,100,0,100,0,100,100,

                //BAS
                0,150,0,150,150,0,150,150,0,150,0,

                //DROITE
                200,0,200,200,0,0,200,0,200

            };

            return lstPrix;
        }

    }
}