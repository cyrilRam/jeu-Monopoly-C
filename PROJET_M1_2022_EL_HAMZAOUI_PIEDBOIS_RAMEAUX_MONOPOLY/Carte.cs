using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet
{
    // %%%%%%%%%%%%%%%%%%%%%%%%%% Classe Carte %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
    public class Carte
    {
        // CarteChances : Méthode permettant d'initialiser les differentes cartes chances 
        public static string[][] CarteChances()
        {
            string[][] tab = new string[5][];

            tab[0] = new string[] { "Concours de beauté. Recevez 10", "10" };

            tab[1] = new string[] { "Vous avez gané vos paris sportifs. Recevez 20", "20" };

            tab[2] = new string[] { "Salaire mensuel. Recevez 100", "100" };

            tab[3] = new string[] { "Vous avez gagné au lotto. Recevez 200", "200" };

            tab[4] = new string[] { "Vous avez gané le marathon du pays. Recevez 50", "50" };

            

            return tab;
        }

        // CarteCaisseCom : Méthode permettant d'initialiser les differentes cartes ciasse communautaire
        public static string[][] CarteCaisseCom()
        {
            string[][] tab = new string[5][];

            tab[0] = new string[] { "Exces de vitesse. Payez 10", "10" };

            tab[1] = new string[] { "Essence, vous devez faire le plein . Payez 20", "20" };

            tab[2] = new string[] { "Votre assurance n'est pas en règle. Payez 30", "30" };

            tab[3] = new string[] { "Corruption, votre banquier veut etre payé. Payez 40", "40" };

            tab[4] = new string[] { "Déclaration d'impots. Payez 100", "100" };


            return tab;
        }

        // CarteAlea : Méthode permettant tirer aléatoirement une carte chance ou une carte caisse communautaire
        public static string[] CarteAlea(string[][] tab)
        {
            var generator = new Random(Guid.NewGuid().GetHashCode());
            int nb = generator.Next(0, 5);

            return tab[nb];

        }


    }

}
