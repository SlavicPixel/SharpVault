using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpVault.models
{
    internal abstract class PasswordGenerator
    {
        private static List<PasswordGenerator> generators;
        private static readonly Random random = new Random();

        public static void Clear()
        {
            if (generators != null) generators.Clear();
            else generators = new List<PasswordGenerator>();
        }

        public static bool IsEmpty()
        {
            return generators == null || generators.Count == 0;
        }

        public static void Add(PasswordGenerator pwdg)
        {
            if (generators == null) generators = new List<PasswordGenerator>();
            generators.Add(pwdg);
        }

        public abstract string GetChar();

        private static PasswordGenerator GetRandomPassGen()
        {
            if (generators == null)
            {
                generators = new List<PasswordGenerator>();
                Add(new LowerCaseGenerator());
            }

            if (generators.Count == 1) return generators[0];
            int randomIndex = random.Next(generators.Count);
            return generators[randomIndex];
        }

        public static string GeneratePassword(int sizeOfPassword)
        {
            if (sizeOfPassword <= 0 || IsEmpty())
            {
                throw new InvalidOperationException("Invalid size or no password generators available.");
            }

            string password = "";

            while (sizeOfPassword != 0)
            {
                password += GetRandomPassGen().GetChar();
                sizeOfPassword--;
            }

            return password;
        }
    }
}
