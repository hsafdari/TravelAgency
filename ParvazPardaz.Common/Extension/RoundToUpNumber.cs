using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ParvazPardaz.Infrastructure
{
    public static class RoundToUpNumber
    {
        public static int Round(int x,int y)
        {
            // TODO: Define behaviour for negative numbers
            int remainder;
            int quotient = Math.DivRem(x, y, out remainder);
            return remainder == 0 ? quotient : quotient + 1;
        }
    }
}