using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Web.Http;

namespace BLL
{
    public class Encrypt
    {

        public void CrearLlaves()
        {
            // Variables
            CspParameters cspParams = null;
            RSACryptoServiceProvider rsaProvider = null;
            StreamWriter publicKeyFile = null;
            StreamWriter privateKeyFile = null;
            string publicKey = "";
            string privateKey = "";


            // Create a new key pair on target CSP
            cspParams = new CspParameters();
            cspParams.ProviderType = 1; // PROV_RSA_FULL 
            //cspParams.ProviderName; // CSP name
            cspParams.Flags = CspProviderFlags.UseArchivableKey;
            cspParams.KeyNumber = (int)KeyNumber.Exchange;
            rsaProvider = new RSACryptoServiceProvider(cspParams);

            // Export public key
            publicKey = rsaProvider.ToXmlString(false);

            // Write public key to file
            publicKeyFile = File.CreateText("llave_publica.xml");
            publicKeyFile.Write(publicKey);
            publicKeyFile.Close();


            // Export private/public key pair 
            privateKey = rsaProvider.ToXmlString(true);

            // Write private/public key pair to file
            privateKeyFile = File.CreateText("llave_Privada_Publica.xml");
            privateKeyFile.Write(privateKey);
            privateKeyFile.Close();


        }

        public string Encriptar(string pCadena)
        {
            // Variables
            CspParameters cspParams = null;
            RSACryptoServiceProvider rsaProvider = null;
            StreamReader publicKeyFile = null;

            FileStream encryptedFile = null;
            string publicKeyText = "";

            byte[] plainBytes = null;
            byte[] encryptedBytes = null;

            try
            {
                // Select target CSP
                cspParams = new CspParameters();
                cspParams.ProviderType = 1; // PROV_RSA_FULL 
                //cspParams.ProviderName; // CSP name
                rsaProvider = new RSACryptoServiceProvider(cspParams);

                // Read public key from file
                string appPath = AppDomain.CurrentDomain.BaseDirectory + "Sources\\llave_privada.xml";
                publicKeyFile = File.OpenText(appPath);
                publicKeyText = publicKeyFile.ReadToEnd();

                // Import public key
                rsaProvider.FromXmlString(publicKeyText);

                // Encrypt plain text
                plainBytes = Encoding.Unicode.GetBytes(pCadena);
                encryptedBytes = rsaProvider.Encrypt(plainBytes, false);
                return Convert.ToBase64String(encryptedBytes); ;

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                // Do some clean up if needed
                if (publicKeyFile != null)
                {
                    publicKeyFile.Close();
                }

                if (encryptedFile != null)
                {
                    encryptedFile.Close();
                }
            }

        }

        public string Desencriptar(string pCadena)
        {
            // Variables
            CspParameters cspParams = null;
            RSACryptoServiceProvider rsaProvider = null;
            StreamReader privateKeyFile = null;
            FileStream encryptedFile = null;
            StreamWriter plainFile = null;
            string privateKeyText = "";
            byte[] plainBytes = null;

            try
            {

                // Select target CSP
                cspParams = new CspParameters();
                cspParams.ProviderType = 1; // PROV_RSA_FULL 
                //cspParams.ProviderName; // CSP name
                rsaProvider = new RSACryptoServiceProvider(cspParams);

                plainBytes = Convert.FromBase64String(pCadena);
                string appPath = AppDomain.CurrentDomain.BaseDirectory + "Sources\\llave_Privada_Publica.xml";

                privateKeyFile = File.OpenText(appPath);
                privateKeyText = privateKeyFile.ReadToEnd();
                rsaProvider.FromXmlString(privateKeyText);

                byte[] decrypt = rsaProvider.Decrypt(plainBytes, false);

                return Encoding.Unicode.GetString(decrypt);

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                // Do some clean up if needed
                if (privateKeyFile != null)
                {
                    privateKeyFile.Close();
                }
                if (encryptedFile != null)
                {
                    encryptedFile.Close();
                }
                if (plainFile != null)
                {
                    plainFile.Close();
                }
            }
        }

    }
}
