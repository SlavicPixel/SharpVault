using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpVault.models
{
    internal class UpperCaseGenerator : PasswordGenerator
    {
        private static readonly char CHAR_A = 'A';
        private static readonly char CHAR_Z = 'Z';

        public override string GetChar()
        {
            return Convert.ToString((char)Helper.RandomChar(CHAR_A, CHAR_Z));
        }
    }

}
