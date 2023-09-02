using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpVault.models
{
    internal class NumericGenerator : PasswordGenerator
    {
        private static readonly char CHAR_0 = '0';
        private static readonly char CHAR_9 = '9';

        public override string GetChar()
        {
            return Convert.ToString((char)Helper.RandomChar(CHAR_0, CHAR_9));
        }
    }
}
