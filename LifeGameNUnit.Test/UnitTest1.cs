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
        [TestCase(10, 10)]
        [TestCase(36, 22)]
        public void InitializeGameTable(int abscisse, int ordonnee)
        {
            LifeGameTable table = new LifeGameTable(abscisse, ordonnee);
            List<Case> gameTable = table.InitializeTable();
            Assert.AreEqual(gameTable.Count, abscisse * ordonnee);
            Assert.Pass();
        }
    }
}