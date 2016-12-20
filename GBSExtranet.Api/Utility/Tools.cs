using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Reflection;
using GBSExtranet.Api.Models;
using GBSExtranet.Repository;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.ComponentModel;
using System.Data;
namespace GBSExtranet.Api
{
    public class Tools
    {

        #region Filtering & Sorting
        public List<T> FilterData<T>(List<T> data,List<Filter> filters)
        {
            List<T> filteredData = new List<T>();
            if (filters != null && filters.Count > 0)
            {
                foreach (var f in filters)
                {
                    if (f.value != null)
                    {                        
                        if (f.condition == "match")
                        {
                            var v = Convert.ToString(f.value);
                            filteredData = (from n in data
                                            where GetDynamicSortProperty(n, f.model) == v
                                            select n).ToList();
                        }
                        if (f.condition == "like")
                        {
                            var v = Convert.ToString(f.value);
                            filteredData = (from n in data
                                            where (GetDynamicSortProperty(n, f.model) as string).Contains(v)
                                            select n).ToList();
                        }
                        if (f.condition == "less")
                        {
                            var v = Convert.ToInt64(f.value);
                            filteredData = (from n in data
                                            where Convert.ToInt64((GetDynamicSortProperty(n, "ID"))) < v
                                            select n).ToList();
                        }
                        if (f.condition == "more")
                        {
                            var v = Convert.ToInt64(f.value);
                            filteredData = (from n in data
                                            where Convert.ToInt64((GetDynamicSortProperty(n, "ID"))) > v
                                            select n).ToList();
                        }
                        if (f.condition == "equal")
                        {
                            var v = Convert.ToInt64(f.value);
                            filteredData = (from n in data
                                            where Convert.ToInt64((GetDynamicSortProperty(n, "ID"))) == v
                                            select n).ToList();
                        }
                        if (f.condition == "between")
                        {
                            var o = JObject.Parse(f.value.ToString());
                            var m = Convert.ToInt64(o.Property("more").Value.ToString());
                            var l = Convert.ToInt64(o.Property("less").Value.ToString());
                            filteredData = (from n in data
                                            where (Convert.ToInt64((GetDynamicSortProperty(n, "ID"))) > m && Convert.ToInt64((GetDynamicSortProperty(n, "ID"))) < l)
                                            select n).ToList();
                        }

                    }
                }
            }
            return filteredData;
        }
        public static void ClearFolder(string FolderName) 
        {
            DirectoryInfo dir = new DirectoryInfo(FolderName);

            foreach (FileInfo fi in dir.GetFiles())
            {
                fi.Delete();
            }

            foreach (DirectoryInfo di in dir.GetDirectories())
            {
                ClearFolder(di.FullName);
                di.Delete();
            }
        }
        public List<T> Sort_List<T>(string sortDirection, string sortExpression, List<T> data)
        {

            List<T> data_sorted = new List<T>();

            if (sortDirection == "ASC")
            {
                data_sorted = (from n in data
                               orderby GetDynamicSortProperty(n, sortExpression) ascending
                               select n).ToList();
            }
            else if (sortDirection == "DESC")
            {
                data_sorted = (from n in data
                               orderby GetDynamicSortProperty(n, sortExpression) descending
                               select n).ToList();

            }

            return data_sorted;

        }


        public object GetDynamicSortProperty(object item, string propName)
        {
            //Use reflection to get order type
            return item.GetType().GetProperty(propName).GetValue(item, null);
        }

        #endregion

        #region Encryption / Decryption Functions
        public static string EncryptText(string strText)
        {
            if (strText != null)
            {
                return Encrypt(strText, "&#?:*%@,");
            }
            else
            {
                return null;
            }
        }

        public static string DecryptCypher(string strText)
        {
            if (strText != null)
            {
                return Decrypt(strText, "&#?:*%@,");
            }
            else
            {
                return null;
            }
        }

        public static string Encrypt(string strText, string strEncrKey)
        {
            //------------------------------------------------------------------------
            //Encryption algorithm code
            //------------------------------------------------------------------------
            byte[] byKey = {
		
	};
            byte[] IV = {
		0x12,
		0x34,
		0x56,
		0x78,
		0x90,
		0xab,
		0xcd,
		0xef
	};

            try
            {
                byKey = System.Text.Encoding.UTF8.GetBytes(Microsoft.VisualBasic.Strings.Left(strEncrKey, 8));

                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                byte[] inputByteArray = Encoding.UTF8.GetBytes(strText);
                MemoryStream ms = new MemoryStream();
                CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(byKey, IV), CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();
                return Convert.ToBase64String(ms.ToArray());

            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }


        public static string Decrypt(string strText, string sDecrKey)
        {
            //------------------------------------------------------------------------
            //Decryption algorithm code
            //------------------------------------------------------------------------
            byte[] byKey = {
		
	};
            byte[] IV = {
		0x12,
		0x34,
		0x56,
		0x78,
		0x90,
		0xab,
		0xcd,
		0xef
	};
            byte[] inputByteArray = new byte[strText.Length + 1];

            strText = Microsoft.VisualBasic.Strings.Replace(strText, " ", "+");

            try
            {
                byKey = System.Text.Encoding.UTF8.GetBytes(Microsoft.VisualBasic.Strings.Left(sDecrKey, 8));
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                inputByteArray = Convert.FromBase64String(strText);
                MemoryStream ms = new MemoryStream();
                CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(byKey, IV), CryptoStreamMode.Write);

                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();
                System.Text.Encoding encoding = System.Text.Encoding.UTF8;

                return encoding.GetString(ms.ToArray());

            }
            catch (Exception ex)
            {
                return ex.Message;
            }

        }

        #endregion
        public static string MD5(string input)
        {
            string hashed;
            MD5CryptoServiceProvider x = new MD5CryptoServiceProvider();
            byte[] bs = System.Text.Encoding.UTF8.GetBytes(input);
            bs = x.ComputeHash(bs);
            System.Text.StringBuilder s = new System.Text.StringBuilder();
            foreach (byte b in bs)
            {
                s.Append(b.ToString("x2").ToLower());
            }
            hashed = s.ToString();
            return hashed;
        }

        public static string buildQueryString(Dictionary<string, string> queryDictionary)
        {
            var query = HttpUtility.ParseQueryString(string.Empty); 
            foreach (var item in queryDictionary)
            {
                query[item.Key] = item.Value;
            }
            return query.ToString();
        }

        public static string prepareURL(string baseURL, string ActionName, Dictionary<string, string> queryDictionary = null)
        {
            var builder = new UriBuilder(baseURL + ActionName);
            if (queryDictionary != null)
            {
                builder.Query = buildQueryString(queryDictionary);
            }
            return builder.ToString();
        }

        public static string EncrytionStringOld(string input)
        {
            byte[] bs = System.Text.Encoding.UTF8.GetBytes(input);

            string encrptedString = Convert.ToBase64String(bs);

            return encrptedString;
        }

        public static string DecrytionStringOld(string encryptedInput)
        {
            string decrptedString;
            MD5CryptoServiceProvider x = new MD5CryptoServiceProvider();

            byte[] bytes = Convert.FromBase64String(encryptedInput);
            decrptedString = System.Text.Encoding.UTF8.GetString(bytes);

            return decrptedString;
        }


        public static string EncrytionString(string input)
        {
            byte[] saltBytes = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };

            byte[] iputBytes = UTF8Encoding.UTF8.GetBytes(input);

            string encryptedString = string.Empty;
            using (AesManaged aes = new AesManaged())
            {
                InitializeAes(saltBytes, aes);

                using (ICryptoTransform encryptTransform = aes.CreateEncryptor())
                {
                    encryptedString = Convert.ToBase64String(Crypted(iputBytes, encryptTransform));
                }

            }

            return encryptedString;
        }

        public static string DecrytionString(string encryptedInput)
        {
            byte[] saltBytes = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };

            byte[] iputBytes = Convert.FromBase64String(encryptedInput);

            string decryptedString = string.Empty;

            using (AesManaged aes = new AesManaged())
            {
                InitializeAes(saltBytes, aes);

                using (ICryptoTransform decryptTransform = aes.CreateDecryptor())
                {
                    byte[] decryptBytes = Crypted(iputBytes, decryptTransform);
                    decryptedString = UTF8Encoding.UTF8.GetString(decryptBytes, 0, decryptBytes.Length);
                }
            }
            return decryptedString;
        }

        private static byte[] Crypted(byte[] iputBytes, ICryptoTransform encryptTransform)
        {
            using (MemoryStream encryptedStream = new MemoryStream())
            {
                using (CryptoStream encryptor =
                new CryptoStream(encryptedStream, encryptTransform, CryptoStreamMode.Write))
                {
                    encryptor.Write(iputBytes, 0, iputBytes.Length);
                    encryptor.Flush();
                    encryptor.Close();
                    byte[] encryptBytes = encryptedStream.ToArray();
                    return encryptBytes;
                }

            }
        }


        private static void InitializeAes(byte[] saltBytes, AesManaged aes)
        {
            Rfc2898DeriveBytes rfc = new Rfc2898DeriveBytes("1357", saltBytes);
            aes.BlockSize = aes.LegalBlockSizes[0].MaxSize;
            aes.KeySize = aes.LegalKeySizes[0].MaxSize;
            aes.Key = rfc.GetBytes(aes.KeySize / 8);
            aes.IV = rfc.GetBytes(aes.BlockSize / 8);
        }


        public static Guid Int2Guid(int value)
        {
            byte[] bytes = new byte[16];
            BitConverter.GetBytes(value).CopyTo(bytes, 0);
            return new Guid(bytes);
        }

        public static int Guid2Int(Guid value)
        {
            byte[] b = value.ToByteArray();
            int bint = BitConverter.ToInt32(b, 0);
            return bint;
        }
    }
}
