using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet
{
    // %%%%%%%%%%%%%%%%%%%%%%%% Classe Caisse %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
    // Elle permet de gérer les caisses des joueurs ainsi que la caisse de la banque
    public class Caisse
    {

        public string Proprio { get; set; }   // Propriétaire de la caisse
        public int Montant { get; set; }      
        public int posX { get; set; }        // position X dans la console ou afficher la caisse
        public int posY { get; set; }        // position Y dans la console ou afficher la caisse

        // Cosntructeur
        public Caisse(string proprio, int montant,int posX, int posY)
        {
            Proprio = proprio;
            Montant = montant;
            this.posX = posX;
            this.posY = posY;
        }

        // Actualize : méthode qui prend en entrée un montant négatif ou positif et l'ajoute au montant disponible
        // dans la caisse
        public void Actualize(int prix)
        {
            Montant += prix;
        }

        // Affiche : méthode d'affichage à la console du montant disponible en caisse 
        public void Affiche()
        {
            Console.SetCursorPosition(posY, posX);
            Console.WriteLine($"Caisse {Proprio} = {Montant}$");
        }
    }
}
