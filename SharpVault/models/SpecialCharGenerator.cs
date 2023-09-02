using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpVault.models
{
    internal class SpecialCharGenerator : PasswordGenerator
    {
        private static readonly char[] SPECIAL_CHAR_ARRAY = "!@#$%^&*".ToCharArray();

        public override string GetChar()
        {
            int randomIndex = Helper.RandomChar(0, SPECIAL_CHAR_ARRAY.Length - 1);
            return Convert.ToString(SPECIAL_CHAR_ARRAY[randomIndex]);
        }
    }

}
