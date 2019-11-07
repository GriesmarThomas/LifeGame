using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LifeGame
{
    public class LifeGameTable
    {
        private int Abscisse { get; set; }
        private int Ordonnee { get; set; }

        public LifeGameTable(int abcisse, int ordonnee)
        {
            Abscisse = abcisse;
            Ordonnee = ordonnee;
        }

        public List<Case> InitializeTable()
        {
            List<Case> gameTable = new List<Case>();
            for (int i = 0; i < Abscisse ; i++)
            {
                for (int j = 0; j < Ordonnee; j++)
                {
                    gameTable.Add(new Case() { X = i, Y = j, isAlive = false });
                }
            }
            return gameTable;
        }

        public void DisplayGameTable(List<Case> gameTable)
        {
            Console.Write("|   |");
            for (int ord = 0; ord < Ordonnee; ord++)
            {
                Console.Write(" " + ord + "|");
            }
            Console.WriteLine();

            for (int i = 0; i < Abscisse ; i++)
            {
                Console.Write("| " + i + " |");
                for (int j = 0; j < Ordonnee; j++)
                {

                    Case currentCase = gameTable.First(myCase => myCase.X == i && myCase.Y == j);

                    if (currentCase.isAlive)
                    {
                        Console.Write(" * ");
                    }
                    else
                    {
                        Console.Write(" . ");
                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine("_____________________________");
        }

        public void AdvanceGeneration(List<Case> gameTable)
        {
            for (int i = 0; i <= 8; i++)
            {
                for (int j = 0; j <= 8; j++)
                {
                    Case currentCase = gameTable.First(myCase => myCase.X == i && myCase.Y == j);
                    List<Case> neighbours = GetCaseNeighbours(gameTable, currentCase);

                    if (currentCase.isAlive)
                    {
                        //Underpopulation
                        if (neighbours.Where(myCase => myCase.isAlive == false).Count() < 2)
                            currentCase.isAlive = false;
                        //Next generation
                        else if (neighbours.Where(myCase => myCase.isAlive == true).Count() == 2 ||
                            neighbours.Where(myCase => myCase.isAlive == true).Count() == 3)
                        {
                            currentCase.isAlive = true;
                        }
                        //Overpopulation
                        else if (neighbours.Where(myCase => myCase.isAlive == false).Count() > 3)
                        {
                            currentCase.isAlive = false;
                        }

                    }
                    else
                    {
                        //Reproduction
                        if (neighbours.Where(myCase => myCase.isAlive == true).Count() == 3)
                            currentCase.isAlive = true;
                    }
                }
            }
        }


        public List<Case> GetCaseNeighbours(List<Case> gameTable, Case currentCase)
        {
            List<Case> neighbours = new List<Case>();

            if (currentCase.X > 0 && currentCase.X < Abscisse)
            {
                //Case gauche
                neighbours.Add(gameTable.First(myCase => myCase.X == currentCase.X - 1 && myCase.Y == currentCase.Y));
                //Case droite
                neighbours.Add(gameTable.First(myCase => myCase.X == currentCase.X + 1 && myCase.Y == currentCase.Y));

                if (currentCase.Y > 0 && currentCase.Y < Ordonnee)
                {
                    //Case haut gauche
                    neighbours.Add(gameTable.First(myCase => myCase.X == currentCase.X - 1 && myCase.Y == currentCase.Y - 1));
                    //Case haut droite
                    neighbours.Add(gameTable.First(myCase => myCase.X == currentCase.X + 1 && myCase.Y == currentCase.Y - 1));
                    //Case bas gauche
                    neighbours.Add(gameTable.First(myCase => myCase.X == currentCase.X - 1 && myCase.Y == currentCase.Y + 1));
                    //Case bas droite
                    neighbours.Add(gameTable.First(myCase => myCase.X == currentCase.X + 1 && myCase.Y == currentCase.Y + 1));

                    //Case haut
                    neighbours.Add(gameTable.First(myCase => myCase.X == currentCase.X && myCase.Y == currentCase.Y - 1));
                    //Case bas
                    neighbours.Add(gameTable.First(myCase => myCase.X == currentCase.X && myCase.Y == currentCase.Y + 1));
                }
                else if (currentCase.Y == 0)
                {
                    //Case bas
                    neighbours.Add(gameTable.First(myCase => myCase.X == currentCase.X && myCase.Y == currentCase.Y + 1));
                    //Case bas gauche
                    neighbours.Add(gameTable.First(myCase => myCase.X == currentCase.X - 1 && myCase.Y == currentCase.Y + 1));
                    //Case bas droite
                    neighbours.Add(gameTable.First(myCase => myCase.X == currentCase.X + 1 && myCase.Y == currentCase.Y + 1));
                }
                else if (currentCase.Y == Ordonnee)
                {
                    //Case haut
                    neighbours.Add(gameTable.First(myCase => myCase.X == currentCase.X && myCase.Y == currentCase.Y - 1));
                    //Case haut gauche
                    neighbours.Add(gameTable.First(myCase => myCase.X == currentCase.X - 1 && myCase.Y == currentCase.Y - 1));
                    //Case haut droite
                    neighbours.Add(gameTable.First(myCase => myCase.X == currentCase.X + 1 && myCase.Y == currentCase.Y - 1));
                }
            }
            else if (currentCase.X == 0)
            {
                //Case droite
                neighbours.Add(gameTable.First(myCase => myCase.X == currentCase.X + 1 && myCase.Y == currentCase.Y));
                if (currentCase.Y > 0 && currentCase.Y < Ordonnee)
                {
                    //Case haut droite
                    neighbours.Add(gameTable.First(myCase => myCase.X == currentCase.X + 1 && myCase.Y == currentCase.Y - 1));
                    //Case bas droite
                    neighbours.Add(gameTable.First(myCase => myCase.X == currentCase.X + 1 && myCase.Y == currentCase.Y + 1));

                    //Case haut
                    neighbours.Add(gameTable.First(myCase => myCase.X == currentCase.X && myCase.Y == currentCase.Y - 1));
                    //Case bas
                    neighbours.Add(gameTable.First(myCase => myCase.X == currentCase.X && myCase.Y == currentCase.Y + 1));
                }
                else if (currentCase.Y == 0)
                {
                    //Case bas droite
                    neighbours.Add(gameTable.First(myCase => myCase.X == currentCase.X + 1 && myCase.Y == currentCase.Y + 1));
                    //Case bas
                    neighbours.Add(gameTable.First(myCase => myCase.X == currentCase.X && myCase.Y == currentCase.Y + 1));
                }
                else if (currentCase.Y == Ordonnee)
                {
                    //Case haut
                    neighbours.Add(gameTable.First(myCase => myCase.X == currentCase.X && myCase.Y == currentCase.Y - 1));
                    //Case haut droite
                    neighbours.Add(gameTable.First(myCase => myCase.X == currentCase.X + 1 && myCase.Y == currentCase.Y - 1));

                }
            }
            else if (currentCase.X == Abscisse)
            {
                //Case gauche
                neighbours.Add(gameTable.First(myCase => myCase.X == currentCase.X - 1 && myCase.Y == currentCase.Y));
                if (currentCase.Y > 0 && currentCase.Y < Ordonnee)
                {
                    //Case bas
                    neighbours.Add(gameTable.First(myCase => myCase.X == currentCase.X && myCase.Y == currentCase.Y + 1));
                    //Case haut
                    neighbours.Add(gameTable.First(myCase => myCase.X == currentCase.X && myCase.Y == currentCase.Y - 1));
                    //Case bas gauche
                    neighbours.Add(gameTable.First(myCase => myCase.X == currentCase.X - 1 && myCase.Y == currentCase.Y + 1));
                    //Case haut gauche
                    neighbours.Add(gameTable.First(myCase => myCase.X == currentCase.X - 1 && myCase.Y == currentCase.Y - 1));
                }
                else if (currentCase.Y == 0)
                {
                    //Case bas gauche
                    neighbours.Add(gameTable.First(myCase => myCase.X == currentCase.X - 1 && myCase.Y == currentCase.Y + 1));
                    //Case bas
                    neighbours.Add(gameTable.First(myCase => myCase.X == currentCase.X && myCase.Y == currentCase.Y + 1));
                }
                else if (currentCase.Y == Ordonnee)
                {
                    //Case haut gauche
                    neighbours.Add(gameTable.First(myCase => myCase.X == currentCase.X - 1 && myCase.Y == currentCase.Y - 1));
                    //Case haut
                    neighbours.Add(gameTable.First(myCase => myCase.X == currentCase.X && myCase.Y == currentCase.Y - 1));
                }
            }

            return neighbours;
        }
    }
}
