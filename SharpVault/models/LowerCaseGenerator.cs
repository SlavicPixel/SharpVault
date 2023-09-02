using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace SharpVault.models
{
    internal class LowerCaseGenerator : PasswordGenerator
    {
        private static readonly char CHAR_A = 'a';
        private static readonly char CHAR_Z = 'z';

        public override string GetChar()
        {
            return Convert.ToString((char)Helper.RandomChar(CHAR_A, CHAR_Z));
        }
    }
}
