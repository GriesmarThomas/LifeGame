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
        private int MaxHistoryCount { get; set; }

        public List<Case> GameTable { get; set; }

        private Queue<Generation> GenerationsHistory { get; set; }
        public int GenerationId { get; set; }


        public LifeGameTable(int abcisse, int ordonnee, int maxHistoryCount)
        {
            Abscisse = abcisse;
            Ordonnee = ordonnee;
            GenerationId = 1;
            GenerationsHistory = new Queue<Generation>();
            MaxHistoryCount = maxHistoryCount;
        }

        public void InitializeTable()
        {
            List<Case> gameTable = new List<Case>();
            for (int i = 0; i < Abscisse; i++)
            {
                for (int j = 0; j < Ordonnee; j++)
                {
                    gameTable.Add(new Case() { X = i, Y = j, isAlive = false });
                }
            }
            this.GameTable = gameTable;
        }

        public void DisplayGameTable()
        {
            Console.Write("|   |");
            for (int ord = 0; ord < Ordonnee; ord++)
            {
                Console.Write(" " + ord + "|");
            }
            Console.WriteLine();

            for (int i = 0; i < Abscisse; i++)
            {
                Console.Write("| " + i + " |");
                for (int j = 0; j < Ordonnee; j++)
                {
                    Case currentCase = GameTable.First(myCase => myCase.X == i && myCase.Y == j);

                    if (currentCase.isAlive)
                        Console.Write(" * ");
                    else
                        Console.Write(" . ");
                }
                Console.WriteLine();
            }
            Console.WriteLine("_____________________________");
            Console.WriteLine("Generation : " + GenerationId);
        }

        public bool AdvanceGeneration()
        {
            GenerationId++;
            var currentGameTable = GameTable.ConvertAll(x => x.DeepCopy());

            for (int i = 0; i < Abscisse; i++)
            {
                for (int j = 0; j < Ordonnee; j++)
                {

                    Case currentCase = currentGameTable.First(myCase => myCase.X == i && myCase.Y == j);
                    List<Case> neighbours = GetCaseNeighbours(currentGameTable, currentCase);

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

            this.GameTable = currentGameTable;

            AddToHistory(currentGameTable);

            return CheckIfGenerationStucked(MaxHistoryCount);
        }

        public void AddToHistory(List<Case> currentGameTable)
        {
            Generation newGenInHistory = new Generation() { Id = GenerationId, GameTable = currentGameTable };
            GenerationsHistory.Enqueue(newGenInHistory);

            if (GenerationsHistory.Count == MaxHistoryCount + 1)
            {
                GenerationsHistory.Dequeue();
            }
        }

        public bool CheckIfGenerationStucked(int maxHistoryCount)
        {
            int isSameCounter = 0;
            if (GenerationsHistory.Count == maxHistoryCount)
            {
                int index = 0;
                List<Case> previousGameTable = null;

                foreach (List<Case> currentGameTable in GenerationsHistory.Select(x => x.GameTable).ToList())
                {
                    if (previousGameTable != null && currentGameTable.SequenceEqual(previousGameTable, new CaseComparer()))
                    {
                        isSameCounter++;
                    }

                    index++;
                    previousGameTable = new List<Case>(currentGameTable);
                }
            }

            return isSameCounter == maxHistoryCount - 1;
        }

        public List<Case> GetCaseNeighbours(List<Case> gameTable, Case currentCase)
        {
            List<Case> neighbours = new List<Case>();

            if (currentCase.X > 0 && currentCase.X < Abscisse - 1)
            {
                //Case gauche
                neighbours.Add(gameTable.First(myCase => myCase.X == currentCase.X - 1 && myCase.Y == currentCase.Y));
                //Case droite
                neighbours.Add(gameTable.First(myCase => myCase.X == currentCase.X + 1 && myCase.Y == currentCase.Y));

                if (currentCase.Y > 0 && currentCase.Y < Ordonnee - 1)
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
                if (currentCase.Y > 0 && currentCase.Y < Ordonnee - 1)
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
                else if (currentCase.Y == Ordonnee - 1)
                {
                    //Case haut
                    neighbours.Add(gameTable.First(myCase => myCase.X == currentCase.X && myCase.Y == currentCase.Y - 1));
                    //Case haut droite
                    neighbours.Add(gameTable.First(myCase => myCase.X == currentCase.X + 1 && myCase.Y == currentCase.Y - 1));

                }
            }
            else if (currentCase.X == Abscisse - 1)
            {
                //Case gauche
                neighbours.Add(gameTable.First(myCase => myCase.X == currentCase.X - 1 && myCase.Y == currentCase.Y));
                if (currentCase.Y > 0 && currentCase.Y < Ordonnee - 1)
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
                else if (currentCase.Y == Ordonnee - 1)
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
