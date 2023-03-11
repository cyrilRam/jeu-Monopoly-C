using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Projet
{
    // %%%%%%%%%%%%%%%%%%%%%%%%%% Classe Pion %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
    public class Pion
    {

        public int posX { get; set; }  
        public int posY { get; set; }
        public Char Carcater { get; set; }   
        public ConsoleColor Color {get;set;} 
        public int pas { get; set; }         // Pas parcouru par le pion dans le plateau de jeu
        public string Name { get; set; }     // Nom du propriétaire du pion
        public int isPrison { get; set; }    // Attribut du pion :
                                             // si isPrison > 0 alors le pion est dans la case prison
                                             // si isPrison = 0 alors le pion est ailleurs 
        
        // Constructeur
        public Pion(int posX, int posY, char carcater, ConsoleColor color,string Nom)
        {
            this.posX = posX;
            this.posY = posY;
            this.Carcater = carcater;
            this.Color = color;
            this.pas = 0;
            this.Name = Nom;
            this.isPrison = 0;
        }

        // DrawPion : Méthode qui affiche à la console le pion à son emplacement
        public void DrawPion()
        {
            Console.CursorVisible = false;
            Console.SetCursorPosition(posY, posX);
            Console.ForegroundColor = Color;
            Console.Write(Carcater);
            Console.ResetColor();
        }

        // MoveLeft : méthode qui déplace le pion à gauche d'une case
        public  void MoveLeft()
        {
            Console.MoveBufferArea(posY, posX, 1, 1, posY-Console.LargestWindowWidth / 11, posX);
            posY = posY -Console.LargestWindowWidth / 11;
            pas += 1;
        }

        // MoveRight : méthode qui déplace le pion à droite d'une case
        public void MoveRight()
        {
            Console.MoveBufferArea( posY, posX, 1, 1, posY + Console.LargestWindowWidth / 11, posX);
            posY = posY + Console.LargestWindowWidth / 11;
            pas += 1;
        }

        // MoveUp : méthode qui déplace le pion vers le haut d'une case
        public void MoveUp()
        {
            Console.MoveBufferArea(posY, posX, 1, 1, posY , posX-10);
            posX = posX - 10;
            pas += 1;
        }

        // MoveLeft : méthode qui déplace le pion vers le bas d'une case
        public void MoveDown()
        {
            Console.MoveBufferArea(posY, posX, 1, 1, posY, posX + 10);
            posX = posX +10;
            pas += 1;
        }

        // GetTypeofMove : méthode permet de changer la direction du deplacement du pion en fonction
        // de l'int typeMove
        public void GetTypeofMove(int typeMove)
        {
            switch (typeMove)
            {
                case 0:
                    MoveRight();
                    break;
                case 1:
                    MoveDown();
                    break;
                case 2:
                    MoveLeft();
                    break;
                case 3:
                    MoveUp();
                    break;
                default:
                    break;
            }
        }

        // GlobalMove : méthode qui permet de déplacer le pion dans le jeu en fonction du résulat du lancé de dés
        public void GlobalMove(int pasDé)
        {
            
            for(int i=0; i < pasDé; i++)
            {
                Thread.Sleep(500);
                if (pas < 10)
                {
                    GetTypeofMove(0);
                }
                else if (pas < 20)
                {
                    GetTypeofMove(1);
                }
                else if (pas < 30)
                {
                    GetTypeofMove(2);
                }
                else if (pas < 40)
                {
                    GetTypeofMove(3);
                }

                if (pas >= 40) { pas = pas - 40; }
            }

            Thread.Sleep(1000);
        }

    }

}