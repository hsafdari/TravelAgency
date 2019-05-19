using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ParvazPardaz.Common.Utility
{
    public static class GenerateRandomKey
    {
        #region CallRandom
        public static string CallRandom()
        {
            string cusip = LongRandom(10000000000000, 99999999999999, new Random()).ToString(); //enter 8 digit cusip here
            return cusip.ToString();
            //string checkDigit = GenerateCheckDigit(cusip);
            //return checkDigit;
        } 
        #endregion

        #region LongRandom
        private static long LongRandom(long min, long max, Random rand)
        {
            byte[] buf = new byte[8];
            rand.NextBytes(buf);
            long longRand = BitConverter.ToInt64(buf, 0);
            return (Math.Abs(longRand % (max - min)) + min);
        }
        #endregion

        #region GenerateCheckDigit
        private static string GenerateCheckDigit(string cusip)
        {
            int sum = 9;
            char[] digits = cusip.ToUpper().ToCharArray();
            string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ*@#";

            for (int i = 0; i < digits.Length; i++)
            {
                int val;
                if (!int.TryParse(digits[i].ToString(), out val))
                    val = alphabet.IndexOf(digits[i]) + 15;

                if ((i % 2) != 0)
                    val *= 2;

                val = (val % 18) + (val / 18);

                sum += val;
            }

            int check = (17 - (sum % 17)) % 17;

            return check.ToString();
        } 
        #endregion
    }
}