using NUnit.Framework;
using LifeGame;
using System.Collections.Generic;

namespace Tests
{
    [TestFixture]
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Theory]
        [TestCase(10, 10, 5)]
        [TestCase(36, 22, 10)]
        public void InitializeGameTable(int abscisse, int ordonnee, int maxHistoryCount)
        {
            LifeGameTable table = new LifeGameTable(abscisse, ordonnee, maxHistoryCount);
            table.InitializeTable();
            
            Assert.AreEqual(table.GameTable.Count, abscisse * ordonnee);
            Assert.Pass();
        }
    }
}