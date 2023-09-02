using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SharpVault.Utils
{
    internal class HashCheck
    {
        public static bool CompareFileSizes(string vault, string vaultBackup)
        {
            if (!string.IsNullOrEmpty(vault) && !string.IsNullOrEmpty(vaultBackup))
            {
                bool fileSizeEqual = true;

                FileInfo fileInfo1 = new FileInfo(vault);
                FileInfo fileInfo2 = new FileInfo(vaultBackup);

                if (fileInfo1.Length != fileInfo2.Length)
                {
                    fileSizeEqual = false;
                }

                return fileSizeEqual;
            }

            return false;
        }

        public static bool CompareFileHashes(string vault, string vaultBackup)
        {
            if (CompareFileSizes(vault, vaultBackup))
            {
                HashAlgorithm hash = HashAlgorithm.Create();

                byte[] fileHash1;
                byte[] fileHash2;

                using (FileStream fileStream1 = new FileStream(vault, FileMode.Open),
                                  fileStream2 = new FileStream(vaultBackup, FileMode.Open))
                {
                    fileHash1 = hash.ComputeHash(fileStream1);
                    fileHash2 = hash.ComputeHash(fileStream2);
                }

                return BitConverter.ToString(fileHash1) == BitConverter.ToString(fileHash2);
            }
            
            return false;
        }
    }
}
