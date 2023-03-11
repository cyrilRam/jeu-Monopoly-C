using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet
{
    //%%%%%%%%%%%%%%%%%%%%%%%%%% Classe CaseJeu %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
    // Elle permet la création et l'affichage à la console des cases de jeu
    public class CaseJeu
    {
        public string Name { get; set; }        // Nom de la case

        public int ligne { get; set; }          // position X de la case

        public int col { get; set; }            // position y de la case

        public ConsoleColor couleur { get; set; } 

        public string TypeCase { get; set; }    // Type de la case parmis les usivants:
                                                // RUE,GARE,DËPART,PRISON,ALLER,PRISON, CHANCE,COMMUNAUTE,EAU, ELECTRICITE, IMPOT,TAXE

        public int Prix { get; set; }           // Prix de la case

        public string Proprio { get; set; }     // Propriètaire de la case

        public int[] Tab_prix { get; set; }     // Loyer à payer en fonction des maisons contruites lorsque l'adeverasiare est proprio 
                                                //[loyer 0 maison, 1 maison,2 maisons, 3 maisons, 4 maisons, 1 Hotel]

        public int Prix_maison { get; set; }    // Cout de construction de maisons

        public int Nb_maisons { get; set; }     // nombre de maisons contruites dans la case

        // Constructeur
        public CaseJeu(string nomrue, int laligne, int lacol, ConsoleColor lacouelur, string Lacase,
            int leprix, string Leproprio, int[] tab_p, int p_m, int nb)

        {
            Name = nomrue;
            ligne = laligne;
            col = lacol;
            couleur = lacouelur;
            TypeCase = Lacase;
            Prix = leprix;
            Proprio = Leproprio;
            Tab_prix = tab_p;
            Prix_maison = p_m;
            Nb_maisons = nb;

        }


        // Affichage : méthode qui permet d'afficher la case à la console
        public void Affichage()
        {

            int nb_ligne = 10;
            int nb_col = Console.LargestWindowWidth / 11;

            Contour(nb_ligne, nb_col);

            // Barre de couleur
            for (int i = 1; i <= nb_col - 1; i++)
            {
                for (int j = 1; j <= 2; j++)
                {
                    Console.SetCursorPosition(i + col, j + ligne);
                    Console.BackgroundColor = couleur;
                    Console.WriteLine(" ");
                    Console.ResetColor();
                }
            }

            // Ecriture du nom
            int size = Name.Length;
            if (size <= 8)
            {
                Console.SetCursorPosition(col + 2 + (size - Name.Length) / 2, ligne + 3);
                Console.WriteLine(Name);

            }
            else
            {
                string[] subs = Name.Split(' ');
                Console.SetCursorPosition(col + 1, ligne + 3);
                Console.WriteLine(subs[0]);
                string restenom;
                if (subs.Length > 2)
                {
                    restenom = (subs[1] + ' ' + subs[2]);
                    Console.SetCursorPosition(col + 1, ligne + 4);
                    Console.WriteLine(restenom);
                }

                else
                {
                    Console.SetCursorPosition(col + 1, ligne + 4);
                    Console.WriteLine(subs[1]);

                }
            }

            //Ecriture du prix
            Console.SetCursorPosition(col + 6, ligne + nb_ligne - 1);
            if (Prix > 0)
            {
                Console.WriteLine($"{Prix}$ ");
            }

            // Ecriture du nom du proprio 
            
            if (this.Proprio!=" ")
            {
                Console.SetCursorPosition(col + 2, ligne + 1);
                Console.WriteLine(Proprio);
            }
            
            // Affichage des maisons 
            if(Nb_maisons>0 && this.Proprio != " " && this.TypeCase=="RUE")
            {
                int n = 4;
                for (int i = 1; i <= Nb_maisons; i++)
                {
                    Console.SetCursorPosition(col + n+ i, ligne + 1);
                  
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.BackgroundColor = couleur;
                    Console.Write("■");
                    Console.ResetColor();
                    n += 1;

                }  

            }

        }

        // Contour : méthode qui réalise et affiche à la console le contour des caeses
        public void Contour(int nb_ligne, int nb_col)
        {
            for (int i = 0; i <= nb_col; i++)
            {
                Console.SetCursorPosition(col + i, ligne);
                Console.WriteLine("_");

            }


            for (int i = 0; i <= nb_col; i++)
            {
                Console.SetCursorPosition(i + col, ligne + nb_ligne);
                Console.WriteLine("_");


            }

            for (int i = 1; i <= nb_ligne; i++)
            {
                Console.SetCursorPosition(col, ligne + i);
                Console.WriteLine("│");

            }

            for (int i = 1; i <= nb_ligne; i++)
            {
                Console.SetCursorPosition(col + nb_col, ligne + i);
                Console.WriteLine("│");

            }

        }
    }
}