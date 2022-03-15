using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace HLibrary
{
    public class Criptografia
    {
        public static string keySecret = "cx3f2e4ct4d2";
        public static string EnCode(string entrada)
        {
            TripleDESCryptoServiceProvider tripledescryptoserviceprovider = new TripleDESCryptoServiceProvider();
            MD5CryptoServiceProvider md5cryptoserviceprovider = new MD5CryptoServiceProvider();

            try
            {
                if (entrada.Trim() != "")
                {
                    tripledescryptoserviceprovider.Key = md5cryptoserviceprovider.ComputeHash(Encoding.Default.GetBytes(keySecret));
                    tripledescryptoserviceprovider.Mode = CipherMode.ECB;
                    ICryptoTransform desdencrypt = tripledescryptoserviceprovider.CreateEncryptor();
                    byte[] buff = Encoding.Default.GetBytes(entrada);

                    //var d = Convert.ToBase64String(desdencrypt.TransformFinalBlock(buff, 0, buff.Length));

                    return Convert.ToBase64String(desdencrypt.TransformFinalBlock(buff, 0, buff.Length));
                }
                else
                {
                    return "";
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
            finally
            {
                tripledescryptoserviceprovider = null;
                md5cryptoserviceprovider = null;
            }
        }

        public static string DeCode(string entrada)
        {
            TripleDESCryptoServiceProvider tripledescryptoserviceprovider = new TripleDESCryptoServiceProvider();
            MD5CryptoServiceProvider md5cryptoserviceprovider = new MD5CryptoServiceProvider();

            try
            {
                if (entrada.Trim() != "")
                {
                    tripledescryptoserviceprovider.Key = md5cryptoserviceprovider.ComputeHash(Encoding.Default.GetBytes(keySecret));
                    tripledescryptoserviceprovider.Mode = CipherMode.ECB;
                    ICryptoTransform desdencrypt = tripledescryptoserviceprovider.CreateDecryptor();
                    byte[] buff = Convert.FromBase64String(entrada);

                    return Encoding.Default.GetString(desdencrypt.TransformFinalBlock(buff, 0, buff.Length));
                }
                else
                {
                    return "";
                }
            }
            catch (Exception exception)
            {
                return exception.Message;
            }
            finally
            {
                tripledescryptoserviceprovider = null;
                md5cryptoserviceprovider = null;
            }
        }
        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            return Convert.ToBase64String(plainTextBytes);
        }
        public static string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = Convert.FromBase64String(base64EncodedData);
            return Encoding.UTF8.GetString(base64EncodedBytes);
        }
    }
}
