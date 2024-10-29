using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace HRCommon.Utils
{
    public class CypherAndHashManager
    {
        private const string EncryptionKey = "hrdbmanagerxyd8492[;'=]-&3fju35%";

        private const string IV = "hrdbmanager04uf*";

        private const string ByteToHexKey = "x2";

        private static int CHUNK_SIZE = 1024 * 1024 * 5;

        public static string Encrypt(string strNormalString)
        {
            if (string.IsNullOrWhiteSpace(strNormalString))
            {
                return strNormalString;
            }

            try
            {
                byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(strNormalString);
                Aes aes = Aes.Create();
                aes.KeySize = 256;
                aes.BlockSize = 128;
                aes.Key = Encoding.UTF8.GetBytes(EncryptionKey);
                aes.IV = Encoding.UTF8.GetBytes(IV);

                byte[] btResult;
                using (ICryptoTransform crytoTransForm = aes.CreateEncryptor())
                {
                    btResult = crytoTransForm.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
                }

                return Convert.ToBase64String(btResult, 0, btResult.Length);
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public static string Decrypt(string strEncryptedString)
        {
            if (string.IsNullOrWhiteSpace(strEncryptedString))
            {
                return strEncryptedString;
            }

            try
            {
                byte[] btDecryptedArray = Convert.FromBase64String(strEncryptedString);

                Aes aes = Aes.Create();
                aes.KeySize = 256;
                aes.BlockSize = 128;
                aes.Key = Encoding.UTF8.GetBytes(EncryptionKey);
                aes.IV = Encoding.UTF8.GetBytes(IV);

                byte[] btResult;
                using (ICryptoTransform crytoTransForm = aes.CreateDecryptor())
                {
                    btResult = crytoTransForm.TransformFinalBlock(btDecryptedArray, 0, btDecryptedArray.Length);
                }

                return UTF8Encoding.UTF8.GetString(btResult, 0, btResult.Length);
            }
            catch (System.Exception)
            {
                throw;
            }
        }
    }
}
