using System;
using System.Collections.Generic;
using System.Linq;

namespace LifeGame
{
    class Program
    {
        public static List<Case> GameTable { get; set; }

        public const int MAX_ABCISSE = 10;
        public const int MAX_ORDONNEE = 10;

        static void Main(string[] args)
        {

            InitializeTable();

            GameTable.First(myCase => myCase.X == 3 && myCase.Y == 5).isAlive = true;
            GameTable.First(myCase => myCase.X == 4 && myCase.Y == 5).isAlive = true;
            GameTable.First(myCase => myCase.X == 5 && myCase.Y == 5).isAlive = true;
            GameTable.First(myCase => myCase.X == 6 && myCase.Y == 6).isAlive = true;
            GameTable.First(myCase => myCase.X == 6 && myCase.Y == 7).isAlive = true;
            GameTable.First(myCase => myCase.X == 6 && myCase.Y == 4).isAlive = true;
            GameTable.First(myCase => myCase.X == 5 && myCase.Y == 4).isAlive = true;

            //GameTable.First(myCase => myCase.X == 5 && myCase.Y == 6).isAlive = true;
            //GameTable.First(myCase => myCase.X == 6 && myCase.Y == 6).isAlive = true;
            //GameTable.First(myCase => myCase.X == 4 && myCase.Y == 6).isAlive = true;
            //GameTable.First(myCase => myCase.X == 3 && myCase.Y == 6).isAlive = true;



            DisplayGameTable();

            while(true)
            {
                AdvanceGeneration();
                DisplayGameTable();
                Console.ReadKey();
                Console.Clear();
                
            }

        }

        public static void InitializeTable()
        {
            GameTable = new List<Case>();
            for (int i = 0; i <= MAX_ABCISSE + 1; i++)
            {
                for (int j = 0; j <= MAX_ORDONNEE + 1; j++)
                {
                    GameTable.Add(new Case() { X = i, Y = j, isAlive = false });
                }   
            }
        }

        public static void DisplayGameTable()
        {
            Console.Write("|   |");
            for(int ord = 0; ord <= MAX_ORDONNEE + 1; ord++)
            {
                Console.Write(" "+ord + "|");
            }
            Console.WriteLine();

            for (int i = 0; i <= MAX_ABCISSE + 1; i++)
            {
                Console.Write("| " + i + " |");
                for (int j = 0; j <= MAX_ORDONNEE + 1; j++)
                {

                    Case currentCase = GameTable.First(myCase => myCase.X == j && myCase.Y == i);

                    if(currentCase.isAlive)
                    {
                        Console.Write(" * ");
                    } else
                    {
                        Console.Write(" . ");
                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine("_____________________________");
        }

        public static void AdvanceGeneration()
        {
            for (int i = 0; i <= 8; i++)
            {
                for (int j = 0; j <= 8; j++)
                {
                    Case currentCase = GameTable.First(myCase => myCase.X == i && myCase.Y == j);
                    List<Case> neighbours = GetCaseNeighbours(currentCase);

                    if (currentCase.isAlive)
                    {
                        //Underpopulation
                        if (neighbours.Where(myCase => myCase.isAlive == false).Count() < 2)
                            currentCase.isAlive = false;
                        //Next generation
                        else if(neighbours.Where(myCase => myCase.isAlive == true).Count() == 2 || 
                            neighbours.Where(myCase => myCase.isAlive == true).Count() == 3)
                        {
                            currentCase.isAlive = true;
                        }
                        //Overpopulation
                        else if(neighbours.Where(myCase => myCase.isAlive == false).Count() > 3)
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


        public static List<Case> GetCaseNeighbours(Case currentCase)
        {
            List<Case> neighbours = new List<Case>();

            if(currentCase.X > 0 && currentCase.X < MAX_ABCISSE)
            {
                //Case gauche
                neighbours.Add(GameTable.First(myCase => myCase.X == currentCase.X - 1 && myCase.Y == currentCase.Y));
                //Case droite
                neighbours.Add(GameTable.First(myCase => myCase.X == currentCase.X + 1 && myCase.Y == currentCase.Y));

                if (currentCase.Y > 0 && currentCase.Y < MAX_ORDONNEE)
                {
                    //Case haut gauche
                    neighbours.Add(GameTable.First(myCase => myCase.X == currentCase.X - 1 && myCase.Y == currentCase.Y - 1));
                    //Case haut droite
                    neighbours.Add(GameTable.First(myCase => myCase.X == currentCase.X + 1 && myCase.Y == currentCase.Y - 1));
                    //Case bas gauche
                    neighbours.Add(GameTable.First(myCase => myCase.X == currentCase.X - 1 && myCase.Y == currentCase.Y + 1));
                    //Case bas droite
                    neighbours.Add(GameTable.First(myCase => myCase.X == currentCase.X + 1 && myCase.Y == currentCase.Y + 1));

                    //Case haut
                    neighbours.Add(GameTable.First(myCase => myCase.X == currentCase.X && myCase.Y == currentCase.Y - 1));
                    //Case bas
                    neighbours.Add(GameTable.First(myCase => myCase.X == currentCase.X && myCase.Y == currentCase.Y + 1));
                } else if(currentCase.Y == 0)
                {
                    //Case bas
                    neighbours.Add(GameTable.First(myCase => myCase.X == currentCase.X && myCase.Y == currentCase.Y + 1));
                    //Case bas gauche
                    neighbours.Add(GameTable.First(myCase => myCase.X == currentCase.X - 1 && myCase.Y == currentCase.Y + 1));
                    //Case bas droite
                    neighbours.Add(GameTable.First(myCase => myCase.X == currentCase.X + 1 && myCase.Y == currentCase.Y + 1));
                } else if(currentCase.Y == MAX_ORDONNEE)
                {
                    //Case haut
                    neighbours.Add(GameTable.First(myCase => myCase.X == currentCase.X && myCase.Y == currentCase.Y - 1));
                    //Case haut gauche
                    neighbours.Add(GameTable.First(myCase => myCase.X == currentCase.X - 1 && myCase.Y == currentCase.Y - 1));
                    //Case haut droite
                    neighbours.Add(GameTable.First(myCase => myCase.X == currentCase.X + 1 && myCase.Y == currentCase.Y - 1));
                }
            } else if(currentCase.X == 0)
            {
                //Case droite
                neighbours.Add(GameTable.First(myCase => myCase.X == currentCase.X + 1 && myCase.Y == currentCase.Y));
                if (currentCase.Y > 0 && currentCase.Y < MAX_ORDONNEE)
                {
                    //Case haut droite
                    neighbours.Add(GameTable.First(myCase => myCase.X == currentCase.X + 1 && myCase.Y == currentCase.Y - 1));
                    //Case bas droite
                    neighbours.Add(GameTable.First(myCase => myCase.X == currentCase.X + 1 && myCase.Y == currentCase.Y + 1));

                    //Case haut
                    neighbours.Add(GameTable.First(myCase => myCase.X == currentCase.X && myCase.Y == currentCase.Y - 1));
                    //Case bas
                    neighbours.Add(GameTable.First(myCase => myCase.X == currentCase.X && myCase.Y == currentCase.Y + 1));
                }
                else if (currentCase.Y == 0)
                {
                    //Case bas droite
                    neighbours.Add(GameTable.First(myCase => myCase.X == currentCase.X + 1 && myCase.Y == currentCase.Y + 1));
                    //Case bas
                    neighbours.Add(GameTable.First(myCase => myCase.X == currentCase.X && myCase.Y == currentCase.Y + 1));
                }
                else if (currentCase.Y == MAX_ORDONNEE)
                {
                    //Case haut
                    neighbours.Add(GameTable.First(myCase => myCase.X == currentCase.X && myCase.Y == currentCase.Y - 1));
                    //Case haut droite
                    neighbours.Add(GameTable.First(myCase => myCase.X == currentCase.X + 1 && myCase.Y == currentCase.Y - 1));

                }
            } else if(currentCase.X == MAX_ABCISSE)
            {
                //Case gauche
                neighbours.Add(GameTable.First(myCase => myCase.X == currentCase.X - 1 && myCase.Y == currentCase.Y));
                if (currentCase.Y > 0 && currentCase.Y < MAX_ORDONNEE)
                {
                    //Case bas
                    neighbours.Add(GameTable.First(myCase => myCase.X == currentCase.X && myCase.Y == currentCase.Y + 1));
                    //Case haut
                    neighbours.Add(GameTable.First(myCase => myCase.X == currentCase.X && myCase.Y == currentCase.Y - 1));
                    //Case bas gauche
                    neighbours.Add(GameTable.First(myCase => myCase.X == currentCase.X - 1 && myCase.Y == currentCase.Y + 1));
                    //Case haut gauche
                    neighbours.Add(GameTable.First(myCase => myCase.X == currentCase.X - 1 && myCase.Y == currentCase.Y - 1));
                }
                else if (currentCase.Y == 0)
                {
                    //Case bas gauche
                    neighbours.Add(GameTable.First(myCase => myCase.X == currentCase.X - 1 && myCase.Y == currentCase.Y + 1));
                    //Case bas
                    neighbours.Add(GameTable.First(myCase => myCase.X == currentCase.X && myCase.Y == currentCase.Y + 1));
                }
                else if (currentCase.Y == MAX_ORDONNEE)
                {
                    //Case haut gauche
                    neighbours.Add(GameTable.First(myCase => myCase.X == currentCase.X - 1 && myCase.Y == currentCase.Y - 1));
                    //Case haut
                    neighbours.Add(GameTable.First(myCase => myCase.X == currentCase.X && myCase.Y == currentCase.Y - 1));
                }
            }

            return neighbours;
        }
    }
}
