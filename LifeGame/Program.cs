using System;
using System.Collections.Generic;
using System.Linq;

namespace LifeGame
{
    public class Program
    {
        public const int MAX_ABCISSE = 10;
        public const int MAX_ORDONNEE = 32;
        public const int MAX_HISTORY_COUNT = 5;

        public static void Main(string[] args)
        {
            LifeGameTable table = new LifeGameTable(MAX_ABCISSE, MAX_ORDONNEE, MAX_HISTORY_COUNT);
            table.InitializeTable();

            table.GameTable.First(myCase => myCase.X == 3 && myCase.Y == 5).isAlive = true;
            table.GameTable.First(myCase => myCase.X == 4 && myCase.Y == 5).isAlive = true;
            table.GameTable.First(myCase => myCase.X == 5 && myCase.Y == 5).isAlive = true;
            table.GameTable.First(myCase => myCase.X == 6 && myCase.Y == 6).isAlive = true;
            table.GameTable.First(myCase => myCase.X == 6 && myCase.Y == 7).isAlive = true;
            table.GameTable.First(myCase => myCase.X == 6 && myCase.Y == 4).isAlive = true;
            table.GameTable.First(myCase => myCase.X == 5 && myCase.Y == 4).isAlive = true;

            //table.GameTable.First(myCase => myCase.X = )

            table.DisplayGameTable();

            bool exitVar = false;
            while (!exitVar)
            {
                bool isHistoryTheSame = table.AdvanceGeneration();
                Console.Clear();

                table.DisplayGameTable();
                exitVar = isHistoryTheSame;      
            }

            Console.WriteLine("SIMULATION ENDED");
            Console.WriteLine("End Generation : " + table.GenerationId);
            Console.ReadKey();
        }
    }
}
