using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace LifeGame
{
    public class Program
    {
        public const int MAX_ABCISSE = 11;
        public const int MAX_ORDONNEE = 11;
        public const int MAX_HISTORY_COUNT = 5;

        public static void Main(string[] args)
        {
            LifeGameTable table = new LifeGameTable(MAX_ABCISSE, MAX_ORDONNEE, MAX_HISTORY_COUNT);
            table.InitializeTable();

            table.GameTable.First(myCase => myCase.X == 3 && myCase.Y == 5).isAlive = true;
            table.GameTable.First(myCase => myCase.X == 4 && myCase.Y == 6).isAlive = true;
            table.GameTable.First(myCase => myCase.X == 4 && myCase.Y == 7).isAlive = true;
            table.GameTable.First(myCase => myCase.X == 3 && myCase.Y == 7).isAlive = true;
            table.GameTable.First(myCase => myCase.X == 2 && myCase.Y == 7).isAlive = true;
            table.GameTable.First(myCase => myCase.X == 5 && myCase.Y == 8).isAlive = true;
            table.GameTable.First(myCase => myCase.X == 5 && myCase.Y == 9).isAlive = true;

            table.DisplayGameTable();

            bool exitVar = false;
            while (!exitVar)
            {
                //Console.ReadKey();
                bool isHistoryTheSame = table.AdvanceGeneration();
                Console.Clear();

                table.DisplayGameTable();
                exitVar = isHistoryTheSame;
                Thread.Sleep(1000);
            }

            Console.WriteLine("SIMULATION ENDED");
            Console.WriteLine("End Generation : " + table.GenerationId);
            Console.ReadKey();
        }
    }
}
