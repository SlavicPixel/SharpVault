using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpVault
{
    public class DataContainer
    {
        public int lastID = 0;
        public EntryModel Entry { get; set; }

        public VaultManagement Vault { get; set; }
        public string vaultPath { get; set; }
        public string decryptedVault { get; set; }
        public bool encrypted { get; set; }
    }
}
