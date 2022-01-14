using System;
using System.Security.Cryptography;
using System.Text;
using System.IO;
namespace CounsellingServer.BusinessLayer
{
    /// <summary>
    /// Summary description for Utils.
    /// </summary>
    public class Secure
    {

        /* IV is used to encrypt the first block of the string to hide and avoid repetative pattern*/
        string KEY_192 = null; 
        
        public Secure()
        {
            string aSecureKey = "^%&*()TAXtg43@!~$9lLKo)(";
            //string aSecureIV = "(*&^y54$#TAXd3@!0(8Mk)(*";
            Key(aSecureKey);
        }

        public void Key(string aSecureKey)
        {  
            KEY_192 = aSecureKey; 
           
        }

        //TRIPLE DES encryption
         
        public string EncryptTripleDES(string clearText)
        {
            try
            {
                byte[] MyEncryptedArray = UTF8Encoding.UTF8.GetBytes(clearText);

                MD5CryptoServiceProvider MyMD5CryptoService = new MD5CryptoServiceProvider();

                byte[] MysecurityKeyArray = MyMD5CryptoService.ComputeHash (UTF8Encoding.UTF8.GetBytes(KEY_192));

                MyMD5CryptoService.Clear();

                var MyTripleDESCryptoService = new  TripleDESCryptoServiceProvider();

                MyTripleDESCryptoService.Key = MysecurityKeyArray;

                MyTripleDESCryptoService.Mode = CipherMode.ECB;

                MyTripleDESCryptoService.Padding = PaddingMode.PKCS7;

                var MyCrytpoTransform = MyTripleDESCryptoService.CreateEncryptor();

                byte[] MyresultArray = MyCrytpoTransform.TransformFinalBlock(MyEncryptedArray, 0,MyEncryptedArray.Length);

                MyTripleDESCryptoService.Clear();

                clearText = Convert.ToBase64String(MyresultArray, 0,MyresultArray.Length);

                //byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
                //using (Aes encryptor = Aes.Create())
                //{
                //    Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(KEY_192, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                //    encryptor.Key = pdb.GetBytes(32);
                //    encryptor.IV = pdb.GetBytes(16);
                //    using (MemoryStream ms = new MemoryStream())
                //    {
                //        using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                //        {
                //            cs.Write(clearBytes, 0, clearBytes.Length);
                //            cs.Close();
                //        }
                //        clearText = Convert.ToBase64String(ms.ToArray());
                //    }
                //}
            }
            catch
            {
                //do nothing
            }
            return Hex(clearText);
        }


        //TRIPLE DES decryption 
        public string DecryptTripleDES(string cipherText)
        {
            try
            {

                byte[] MyDecryptArray = Convert.FromBase64String(DeHex(cipherText));

                MD5CryptoServiceProvider MyMD5CryptoService = new MD5CryptoServiceProvider();

                byte[] MysecurityKeyArray = MyMD5CryptoService.ComputeHash(UTF8Encoding.UTF8.GetBytes(KEY_192));

                MyMD5CryptoService.Clear();

                var MyTripleDESCryptoService = new TripleDESCryptoServiceProvider();

                MyTripleDESCryptoService.Key = MysecurityKeyArray;

                MyTripleDESCryptoService.Mode = CipherMode.ECB;

                MyTripleDESCryptoService.Padding = PaddingMode.PKCS7;

                var MyCrytpoTransform = MyTripleDESCryptoService.CreateDecryptor();

                byte[] MyresultArray = MyCrytpoTransform.TransformFinalBlock(MyDecryptArray, 0,MyDecryptArray.Length);

                MyTripleDESCryptoService.Clear();

                cipherText = UTF8Encoding.UTF8.GetString(MyresultArray);
                //cipherText = cipherText.Replace(" ", "+");
                //byte[] cipherBytes = Convert.FromBase64String(DeHex(cipherText));
                //using (Aes encryptor = Aes.Create())
                //{
                //    Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(KEY_192, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                //    encryptor.Key = pdb.GetBytes(32);
                //    encryptor.IV = pdb.GetBytes(16);
                //    using (MemoryStream ms = new MemoryStream())
                //    {
                //        using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                //        {
                //            cs.Write(cipherBytes, 0, cipherBytes.Length);
                //            cs.Close();
                //        }
                //        cipherText = Encoding.Unicode.GetString(ms.ToArray());
                //    }
                //}
            }
            catch
            {
                //do nothing
            }
            return cipherText;

        }

        //TRIPLE DES encryption
        public string EncryptTripleWithOutDES(string clearText)
        {
            try
            {

                byte[] MyEncryptedArray = UTF8Encoding.UTF8.GetBytes(clearText);

                MD5CryptoServiceProvider MyMD5CryptoService = new MD5CryptoServiceProvider();

                byte[] MysecurityKeyArray = MyMD5CryptoService.ComputeHash(UTF8Encoding.UTF8.GetBytes(KEY_192));

                MyMD5CryptoService.Clear();

                var MyTripleDESCryptoService = new TripleDESCryptoServiceProvider();

                MyTripleDESCryptoService.Key = MysecurityKeyArray;

                MyTripleDESCryptoService.Mode = CipherMode.ECB;

                MyTripleDESCryptoService.Padding = PaddingMode.PKCS7;

                var MyCrytpoTransform = MyTripleDESCryptoService.CreateEncryptor();

                byte[] MyresultArray = MyCrytpoTransform.TransformFinalBlock(MyEncryptedArray, 0, MyEncryptedArray.Length);

                MyTripleDESCryptoService.Clear();

                clearText = Convert.ToBase64String(MyresultArray, 0, MyresultArray.Length); 
            }
            catch
            {
                //do nothing
            }
            return  clearText ;
        }
        //TRIPLE DES decryption

        public string DecryptTripleDESWitoutHex(string cipherText)
        {
            try
            {
                byte[] MyDecryptArray = Convert.FromBase64String(cipherText);

                MD5CryptoServiceProvider MyMD5CryptoService = new MD5CryptoServiceProvider();

                byte[] MysecurityKeyArray = MyMD5CryptoService.ComputeHash(UTF8Encoding.UTF8.GetBytes(KEY_192));

                MyMD5CryptoService.Clear();

                var MyTripleDESCryptoService = new TripleDESCryptoServiceProvider();

                MyTripleDESCryptoService.Key = MysecurityKeyArray;

                MyTripleDESCryptoService.Mode = CipherMode.ECB;

                MyTripleDESCryptoService.Padding = PaddingMode.PKCS7;

                var MyCrytpoTransform = MyTripleDESCryptoService.CreateDecryptor();

                byte[] MyresultArray = MyCrytpoTransform.TransformFinalBlock(MyDecryptArray, 0, MyDecryptArray.Length);

                MyTripleDESCryptoService.Clear();

                cipherText = UTF8Encoding.UTF8.GetString(MyresultArray); 
               
            }
            catch
            {
                //do nothing
            }
            return cipherText;

        } 

        private string DeHex(string hexstring)
        {
            StringBuilder sb = new StringBuilder(hexstring.Length / 2);
            for (int i = 0; i <= hexstring.Length - 1; i = i + 2)
            {
                sb.Append((char)int.Parse(hexstring.Substring(i, 2), System.Globalization.NumberStyles.HexNumber));
            }
            return sb.ToString();
        }

        private string Hex(string sData)
        {
            string temp = String.Empty;
            StringBuilder sb = new StringBuilder(sData.Length * 2);
            for (int i = 0; i < sData.Length; i++)
            {
                if ((sData.Length - (i + 1)) > 0)
                {
                    temp = sData.Substring(i, 2);
                    if (temp == @"\n") sb.Append("0A");
                    else if (temp == @"\b") sb.Append("20");
                    else if (temp == @"\r") sb.Append("0D");
                    else if (temp == @"\c") sb.Append("2C");
                    else if (temp == @"\\") sb.Append("5C");
                    else if (temp == @"\0") sb.Append("00");
                    else if (temp == @"\t") sb.Append("07");
                    else
                    {
                        sb.Append(String.Format("{0:X2}", (int)(sData.ToCharArray())[i]));
                        i--;
                    }
                }
                else
                {
                    sb.Append(String.Format("{0:X2}", (int)(sData.ToCharArray())[i]));
                }
                i++;
            }
            return sb.ToString();
        }



    }
}
