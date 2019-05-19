using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.Common.Utility
{
    public static class CodeGenerator
    {
        public static long LongRandom(long min, long max, Random rand)
        {
            byte[] buf = new byte[8];
            rand.NextBytes(buf);
            long longRand = BitConverter.ToInt64(buf, 0);

            return (Math.Abs(longRand % (max - min)) + min);
        }

        public static string GenerateCheckDigit(string cusip)
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

    

        const int BITCOUNT = 20;
        const int BITMASK = (1 << BITCOUNT / 2) - 1;
        public static uint RoundFunction(uint number)
        {
            return (((number ^ 47894) + 25) << 1) & BITMASK;
        }

        public static  uint Crypt(uint number)
        {
            uint left = number >> (BITCOUNT / 2);
            uint right = number & BITMASK;
            for (int round = 0; round < 10; ++round)
            {
                left = left ^ RoundFunction(right);
                uint temp = left; left = right; right = temp;
            }
            return left | (right << (BITCOUNT / 2));
        }


        const string ALPHABET = "AG8FOLE2WVTCPY5ZH3NIUDBXSMQK7946";
        public static string CouponCode(uint number)
        {
            StringBuilder b = new StringBuilder();
            for (int i = 0; i < 4; ++i)
            {
                b.Append(ALPHABET[(int)number & ((1 << 5) - 1)]);
                number = number >> 5;
            }
            return b.ToString();
        }

        public static uint CodeFromCoupon(string coupon)
        {
            uint n = 0;
            for (int i = 0; i < 6; ++i)
                n = n | (((uint)ALPHABET.IndexOf(coupon[i])) << (5 * i));
            return n;
        }

    }
}
