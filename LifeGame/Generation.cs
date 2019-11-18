using System;
using System.Collections.Generic;
using System.Text;

namespace LifeGame
{
    public class Generation
    {
        public int Id { get; set; }
        public List<Case> GameTable { get; set; }

        public object DeepCopy()
        {
            Generation myGeneration = (Generation)this.MemberwiseClone();
            myGeneration.Id = this.Id;
            myGeneration.GameTable = new List<Case>(GameTable);
            return myGeneration;
        }
    }
}
