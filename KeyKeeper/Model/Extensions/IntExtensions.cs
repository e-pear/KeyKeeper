using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyKeeper
{
    public static class IntExtensions
    {
        /// <summary>
        /// Converts an associated integer value to four digit string code. 
        /// </summary>
        /// <param name="number">This int value object.</param>
        /// <returns>Four digit string object for integers from 0 to 9999 value range. Null reference for others.</returns>
        public static string ConvertToFourDigitStringCode(this int number)
        {
            if (number > 9999 || number < 0) return null;

            StringBuilder code = new StringBuilder(number.ToString(), 4);
            
            while(code.Length < 4)
            {
                code.Insert(0, "0");
            }

            return code.ToString();

        }
    }
}
