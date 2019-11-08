using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LifeGameOnWeb.Models;
using LifeGame;

namespace LifeGameOnWeb.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            LifeGameTable table = new LifeGameTable(10, 10);
            List<Case> gameTable = table.InitializeTable();

            gameTable.First(myCase => myCase.X == 3 && myCase.Y == 5).isAlive = true;
            gameTable.First(myCase => myCase.X == 4 && myCase.Y == 5).isAlive = true;
            gameTable.First(myCase => myCase.X == 5 && myCase.Y == 5).isAlive = true;
            gameTable.First(myCase => myCase.X == 6 && myCase.Y == 6).isAlive = true;
            gameTable.First(myCase => myCase.X == 6 && myCase.Y == 7).isAlive = true;
            gameTable.First(myCase => myCase.X == 6 && myCase.Y == 4).isAlive = true;
            gameTable.First(myCase => myCase.X == 5 && myCase.Y == 4).isAlive = true;

            table.DisplayGameTable(gameTable);

            int count = 0;
            while (count <= 20)
            {
                table.AdvanceGeneration(gameTable);
                table.DisplayGameTable(gameTable);
                //Console.ReadKey();
                //Console.Clear();
                count++;
            }

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
