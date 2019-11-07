using System;
using System.Collections.Generic;
using System.Linq;

namespace LifeGame
{
    public class Program
    {
        public const int MAX_ABCISSE = 10;
        public const int MAX_ORDONNEE = 36;

        public static void Main(string[] args)
        {
            LifeGameTable table = new LifeGameTable(MAX_ABCISSE, MAX_ORDONNEE);
            List<Case> gameTable = table.InitializeTable();

            gameTable.First(myCase => myCase.X == 3 && myCase.Y == 5).isAlive = true;
            gameTable.First(myCase => myCase.X == 4 && myCase.Y == 5).isAlive = true;
            gameTable.First(myCase => myCase.X == 5 && myCase.Y == 5).isAlive = true;
            gameTable.First(myCase => myCase.X == 6 && myCase.Y == 6).isAlive = true;
            gameTable.First(myCase => myCase.X == 6 && myCase.Y == 7).isAlive = true;
            gameTable.First(myCase => myCase.X == 6 && myCase.Y == 4).isAlive = true;
            gameTable.First(myCase => myCase.X == 5 && myCase.Y == 4).isAlive = true;
            
            table.DisplayGameTable(gameTable);

            while(true)
            {
                table.AdvanceGeneration(gameTable);
                table.DisplayGameTable(gameTable);
                Console.ReadKey();
                Console.Clear();       
            }
        }
    }
}
