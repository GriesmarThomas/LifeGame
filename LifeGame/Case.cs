using System;
using System.Collections.Generic;
using System.Text;

namespace LifeGame
{
    public class Case
    {
        public int X { get; set; }
        public int Y { get; set; }

        public bool isAlive { get; set; }

        public Case DeepCopy()
        {
            Case myCase = (Case)this.MemberwiseClone();
            myCase.X = X;
            myCase.Y = Y;
                return myCase;
        }
    }


    public class CaseComparer : IEqualityComparer<Case>
    {
        public bool Equals(Case x, Case y)
        {
            //if (Object.ReferenceEquals(x, y)) return true;

            if (Object.ReferenceEquals(x, null) || Object.ReferenceEquals(y, null))
                return false;

            return x.X == y.X && x.Y == y.Y && x.isAlive == y.isAlive;
        }

        public int GetHashCode(Case myCase)
        {
            if (Object.ReferenceEquals(myCase, null))
                return 0;

            int hashCaseX = myCase.X == 0 ? 0 : myCase.X.GetHashCode();
            int hashCaseY = myCase.Y == 0 ? 0 : myCase.Y.GetHashCode();
            int hashCaseIsAlive = myCase.isAlive.GetHashCode();

            return hashCaseX ^ hashCaseY ^ hashCaseIsAlive;
        }

    }

}
