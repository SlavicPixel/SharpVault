using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpVault.models
{
    internal class Helper
    {
        private static readonly Random random = new Random();

        public static int RandomVal(int size)
        {
            double rand = random.NextDouble();
            return (int)(rand * size);
        }

        public static int RandomChar(int min, int max)
        {
            return RandomVal(max - min + 1) + min;
        }
    }

}
