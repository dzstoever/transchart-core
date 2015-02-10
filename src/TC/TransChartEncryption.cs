using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// a place to provide implementations of the existing interfaces
// and remove dependancies on any COM Interop assemblies
namespace TransChartEncryption
{
    public enum TransChartEncryptionType
    {
        Rinjindael,
        SHAHash
    }
    

    public interface IEncryptor
    {// to mock TransChartEncryption.Encryptor
        string Decrypt(string encrypted);
        string Encrypt(string plaintext);
        byte[] EncryptBinary(string plaintext);
        bool EncryptAndValidate(string plaintext, string encrypted);
        bool IsEncrypted(string data);

        void Initialize(string appName);
        void InitializeNoAppName(string cnnString);
    }
    public class Encryptor : IEncryptor
    {
        public Encryptor(){}
        public Encryptor(ITransChartSettings tcSettings)
        { }


        public string Decrypt(string encrypted)
        {
            throw new NotImplementedException();
        }
        public string Encrypt(string plaintext)
        {
            throw new NotImplementedException();
        }
        public byte[] EncryptBinary(string plaintext)
        {
            throw new NotImplementedException();
        }
        public bool EncryptAndValidate(string plaintext, string encrypted)
        {
            throw new NotImplementedException();
        }
        public bool IsEncrypted(string data)
        {
            throw new NotImplementedException();
        }


        public void Initialize(string appName)
        {
            throw new NotImplementedException();
        }
        public void InitializeNoAppName(string cnnString)
        {
            throw new NotImplementedException();
        }
    }


    public interface ITransChartSettings
    {// to mock TransChartEncryption.TransChartSettings
        string GetConnectionString();
        string GetConnectionString(string cnnStringName);
    }
    public class TransChartSettings : DictionaryBase, ITransChartSettings
    {
        public TransChartSettings(string appName) { }
        public TransChartSettings(string appName, string cnnStringName) { }

        public string GetConnectionString()
        {
            throw new NotImplementedException();
        }
        public string GetConnectionString(string cnnStringName)
        {
            throw new NotImplementedException();
        }
    }


    public static class Utility
    {
        static string BytesToHexString(byte[] bytes) { throw new NotImplementedException(); }
        static string BytesToString(byte[] bytes) { throw new NotImplementedException(); }
        static byte[] HexStringToBytes(string hex) { throw new NotImplementedException(); }
        static byte[] StringToBytes(string s) { throw new NotImplementedException(); }
    }
    

}
