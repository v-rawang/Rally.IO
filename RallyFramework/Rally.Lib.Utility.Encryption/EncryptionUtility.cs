using System;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Configuration;
using System.IO;
using System.Text;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Collections.Generic;

namespace Rally.Lib.Utility.Encryption
{
    /// <summary>
    /// 提供可逆加密解密，以及X.509证书操作相关的工具方法
    /// </summary>
    public static class EncryptionUtility
    {
        /// <summary>
        /// 使用AES算法加密数据
        /// </summary>
        /// <param name="data">数据（二进制数组）</param>
        /// <param name="key">对称密钥</param>
        /// <param name="iv">初始化向量</param>
        /// <returns>加密结果（二进制数组）</returns>
        public static byte[] AesEncrypt(byte[] data, out byte[] key, out byte[] iv)
        {
            if (data == null)
                throw new ArgumentNullException("没有数据可加密！");

            using (AesCryptoServiceProvider provider = new AesCryptoServiceProvider())
            {
                provider.GenerateKey();
                provider.GenerateIV();
                key = provider.Key;
                iv = provider.IV;
                return provider.CreateEncryptor().TransformFinalBlock(data, 0, data.Length);
            }
        }

        /// <summary>
        /// 使用AES算法加密数据
        /// </summary>
        /// <param name="data">数据（二进制数组）</param>
        /// <param name="key">对称密钥</param>
        /// <param name="iv">初始化向量</param>
        /// <returns>加密结果（二进制数组）</returns>
        public static byte[] AesEncrypt(byte[] data, byte[] key, byte[] iv)
        {
            if (data == null)
                throw new ArgumentNullException("没有数据可加密！");

            using (AesCryptoServiceProvider provider = new AesCryptoServiceProvider())
            {
                //provider.GenerateKey();
                //provider.GenerateIV();
                //key = provider.Key;
                //iv = provider.IV;
                provider.Key = key;
                provider.IV = iv;
                return provider.CreateEncryptor().TransformFinalBlock(data, 0, data.Length);
            }
        }

        /// <summary>
        /// 使用AES算法解密数据
        /// </summary>
        /// <param name="data">数据（二进制数组）</param>
        /// <param name="key">对称密钥</param>
        /// <param name="iv">初始化向量</param>
        /// <returns>解密结果（二进制数组）</returns>
        public static byte[] AesDecrypt(byte[] data, byte[] key, byte[] iv)
        {
            if (data == null)
                throw new ArgumentNullException("没有数据可解密！");

            using (AesCryptoServiceProvider provider = new AesCryptoServiceProvider())
            {
                provider.Key = key;
                provider.IV = iv;
                return provider.CreateDecryptor().TransformFinalBlock(data, 0, data.Length);
            }
        }

        /// <summary>
        /// TripleDES加密
        /// </summary>
        /// <param name="data">待加密的数据</param>
        /// <param name="key">对称密钥</param>
        /// <param name="iv">初始化矢量（只有在CBC解密模式下才适用）</param>
        /// <param name="mode">用于加密的块密码模式</param>
        /// <returns>加密结果（二进制数组）</returns>
        public static byte[] TripleDesEncrypt(byte[] data, out byte[] key, out byte[] iv, CipherMode mode = CipherMode.ECB)
        {
            if (data == null)
                throw new ArgumentNullException("没有数据可加密！");

            using (TripleDESCryptoServiceProvider provider = new TripleDESCryptoServiceProvider() { Mode = mode })
            {
                provider.GenerateKey();
                key = provider.Key;

                if (mode == CipherMode.CBC)
                {
                    provider.GenerateIV();
                }

                iv = provider.IV;

                return provider.CreateEncryptor().TransformFinalBlock(data, 0, data.Length);
            }
        }

        /// <summary>
        /// TripleDES加密
        /// </summary>
        /// <param name="data">待加密的数据</param>
        /// <param name="key">对称密钥</param>
        /// <param name="iv">初始化矢量（只有在CBC解密模式下才适用）</param>
        /// <param name="mode">用于加密的块密码模式</param>
        /// <returns>加密结果（二进制数组）</returns>
        public static byte[] TripleDesEncrypt(byte[] data, byte[] key, byte[] iv, CipherMode mode = CipherMode.ECB)
        {
            if (data == null)
                throw new ArgumentNullException("没有数据可加密！");

            using (TripleDESCryptoServiceProvider provider = new TripleDESCryptoServiceProvider() { Mode = mode })
            {
                //provider.GenerateKey();
                //key = provider.Key;
                provider.Key = key;

                if (mode == CipherMode.CBC)
                {
                    //provider.GenerateIV();
                    provider.IV = iv;
                }

                //iv = provider.IV;

                return provider.CreateEncryptor().TransformFinalBlock(data, 0, data.Length);
            }
        }

        /// <summary>
        /// TripleDES解密
        /// </summary>
        /// <param name="data">待解密的字符串</param>
        /// <param name="key">对称密钥</param>
        /// <param name="iv">初始化矢量（只有在CBC解密模式下才适用）</param>
        /// <param name="mode">用于加密的块密码模式</param>
        /// <returns>解密结果（二进制数组）</returns>
        public static byte[] TripleDesDecrypt(byte[] data, byte[] key, byte[] iv, CipherMode mode = CipherMode.ECB)
        {
            if (data == null)
                throw new ArgumentNullException("没有数据可解密！");

            using (TripleDESCryptoServiceProvider provider = new TripleDESCryptoServiceProvider() { Mode = mode, Key = key, IV = iv })
            {
                return provider.CreateDecryptor().TransformFinalBlock(data, 0, data.Length);
            }
        }

        /// <summary>
        /// 使用RSA算法加密数据
        /// </summary>
        /// <param name="data">数据（二进制数组）</param>
        /// <param name="provider">RSA提供程序实例</param>
        /// <returns>加密结果（二进制数组）</returns>
        public static byte[] RsaEncrypt(byte[] data, RSACryptoServiceProvider provider)
        {
            if (data == null)
                throw new ArgumentNullException("没有数据可加密！");

            return provider.Encrypt(data, false);
        }

        /// <summary>
        /// 使用RSA算法解密数据
        /// </summary>
        /// <param name="data">数据（二进制数组）</param>
        /// <param name="provider">RSA提供程序实例</param>
        /// <returns>解密结果（二进制数组）</returns>
        public static byte[] RsaDecrypt(byte[] data, RSACryptoServiceProvider provider)
        {
            if (data == null)
                throw new ArgumentNullException("没有数据可解密！");

            return provider.Decrypt(data, false);
        }

        /// <summary>
        /// 给定条件查询Windows系统所管理的X.509证书
        /// </summary>
        /// <param name="subject">主体名称</param>
        /// <param name="storeLocation">证书存储位置</param>
        /// <param name="storeName">证书存储区名称（默认StoreName.My，个人）</param>
        /// <returns>X.509证书对象集合</returns>
        public static X509Certificate2 GetCertificate(string subject, StoreLocation storeLocation, StoreName storeName = StoreName.My)
        {
            return GetCertificate(storeLocation, storeName, X509FindType.FindBySubjectDistinguishedName, subject);
        }

        /// <summary>
        /// 给定条件查询Windows系统所管理的X.509证书
        /// </summary>
        /// <param name="storeLocation">证书存储位置</param>
        /// <param name="storeName">证书存储区名称（默认StoreName.My，个人）</param>
        /// <returns>X.509证书对象集合</returns>
        public static X509Certificate2Collection GetCertificates(StoreLocation storeLocation, StoreName storeName = StoreName.My)
        {
            X509Store store = new X509Store(StoreName.My, storeLocation);
            store.Open(OpenFlags.ReadOnly);
            return store.Certificates;
        }

        /// <summary>
        /// 给定条件查询Windows系统所管理的X.509证书
        /// </summary>
        /// <param name="storeLocation">证书存储位置</param>
        /// <param name="storeName">证书存储区名称</param>
        /// <param name="findType">搜索值的类型</param>
        /// <param name="findValue">搜索条件值</param>
        /// <returns>X.509证书对象</returns>
        public static X509Certificate2 GetCertificate(StoreLocation storeLocation, StoreName storeName, X509FindType findType, string findValue)
        {
            X509Store store = new X509Store(storeName, storeLocation);
            store.Open(OpenFlags.ReadOnly);
            X509Certificate2Collection certs = store.Certificates.Find(findType, findValue, false);
            if (certs.Count == 0)
                throw new FileNotFoundException("没有找到符合条件的证书！");
            else
                return certs[0];
        }
    }
}
