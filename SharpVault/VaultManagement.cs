using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Collections.ObjectModel;
using System.Windows;
using Ookii.Dialogs.Wpf;
using SharpVault.Utils;

namespace SharpVault
{
    public class VaultManagement
    {
        private string vaultName;
        private string masterKey;

        public VaultManagement(string vaultName, string masterKey)
        {
            this.vaultName = vaultName;
            this.masterKey = masterKey;
        }

        public VaultManagement() { }

        public void SetVaultName(string vaultName) { this.vaultName= vaultName; }
        public string GetVaultName() { return vaultName; }
        public void SetMasterKey(string masterKey1) { this.masterKey= masterKey1; }
        public string GetMasterKey() { return masterKey; }


        public void NewVault(string vaultPath)
        {
            AES aes = new AES();

            byte[] encryptedVault = aes.Encrypt(string.Empty, masterKey);

            File.WriteAllBytes(vaultPath, encryptedVault);


        }

        public string DecryptVault(string vaultPath, string masterKey)
        {
            AES aes = new AES();

            byte[] encryptedVault = File.ReadAllBytes(vaultPath);

            string decryptedVault = aes.Decrypt(encryptedVault, masterKey);

            return decryptedVault;
        }

        public ObservableCollection<EntryModel> InitializeVault(string decryptedvault)
        {
            ObservableCollection<EntryModel> vault = new ObservableCollection<EntryModel>();
            if (decryptedvault == string.Empty)
            {
                return vault;
            } 

            try
            {
                vault = JsonSerializer.Deserialize<ObservableCollection<EntryModel>>(decryptedvault);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return vault;
        }

        public void UpdateVault(ObservableCollection<EntryModel> updatedVault, string masterKey, string vaultPath)
        {
            
            AES aes = new AES();

            string updatedVaultJSON = JsonSerializer.Serialize(updatedVault);

            byte[] encryptedUpdatedVault = aes.Encrypt(updatedVaultJSON, masterKey);

            File.WriteAllBytes($@"{vaultPath}", encryptedUpdatedVault);

        }

        public bool BackupVault(string vaultPath)
        {
            string backupVaultPath = vaultPath + ".old";

            try
            {
                File.Copy(vaultPath, backupVaultPath, true);
                if (!HashCheck.CompareFileHashes(vaultPath, backupVaultPath)) return false;
            }
            catch (IOException iox)
            {
                Console.Write(iox.Message);
                return false;
            }

            return true;
        }
    }


}
