
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace Rally.Lib.Utility.Encryption
{
    /// <summary>
    /// 提供哈希值创建相关的工具方法
    /// </summary>
    public static class HashUtility {
        private const string charRange = @"abcdefghijklmnopqrstuvwxyz0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ~!@#$";
        private static readonly Encoding defaultEncoding = Encoding.UTF8;

        /// <summary>
        /// 生成随机数字符串
        /// </summary>
        /// <param name="length">随机数字符数组长度</param>
        /// <returns>随机数字符串</returns>
        public static string GenerateRandomString(int length) {
            char[] chars = new char[length];
            for (int i = 0; i < length; i++) {
                chars[i] = charRange[RollDice(charRange.Length)];
            }
            return new string(chars);
        }

        /// <summary>
        /// 给定哈希算法提供程序类型计算给定数据（二进制数组）的哈希值
        /// </summary>
        /// <typeparam name="T">哈希算法提供程序类型模板参数</typeparam>
        /// <param name="data">数据（二进制数组）</param>
        /// <returns>哈希值字符串</returns>
        public static string CreateHash<T>(byte[] data) where T : HashAlgorithm {
            if (data == null)
                throw new ArgumentNullException("没有可以计算哈希值的数据！");

            using (HashAlgorithm hashProvider = Activator.CreateInstance<T>()) {
                return ToHexString(hashProvider.ComputeHash(data));
            }
        }

        /// <summary>
        ///  给定哈希算法提供程序类型计算给定字符串的哈希值
        /// </summary>
        /// <typeparam name="T">哈希算法提供程序类型模板参数</typeparam>
        /// <param name="str">原始字符</param>
        /// <returns>哈希值字符串</returns>
        public static string CreateHash<T>(string str) where T : HashAlgorithm {
            return CreateHash<T>(str, defaultEncoding);
        }

        /// <summary>
        /// 给定实现哈希的消息验证代码 (HMAC)的提供程序类型计算给定数据（二进制数组）的哈希值
        /// </summary>
        /// <typeparam name="T">HMAC提供程序类型模板参数</typeparam>
        /// <param name="key">实现哈希的消息验证代码 (HMAC)的算法名称</param>
        /// <param name="data">数据（二进制数组）</param>
        /// <returns>哈希值字符串</returns>
        public static string CreateHmac<T>(string key, byte[] data) where T : HMAC {
            if (data == null)
                throw new ArgumentNullException("没有可以计算哈希值的数据！");

            using (HMAC hmac = (HMAC)Activator.CreateInstance(typeof(T), defaultEncoding.GetBytes(key))) {
                return ToHexString(hmac.ComputeHash(data));
            }
        }

        private static string CreateHash<T>(string str, Encoding encoding) where T : HashAlgorithm {
            return CreateHash<T>(encoding.GetBytes(str));
        }

        private static string ToHexString(byte[] bytes) {
            StringBuilder sb = new StringBuilder();
            foreach (var b in bytes) {
                sb.Append(b.ToString("x2"));
            }
            return sb.ToString();
        }

        private static int RollDice(int length) {
            if (length > byte.MaxValue)
                throw new NotImplementedException();
            using (RNGCryptoServiceProvider rngCsp = new RNGCryptoServiceProvider()) {
                byte[] randomNumberBuffer = new byte[1];
                do {
                    rngCsp.GetBytes(randomNumberBuffer);
                }
                while (randomNumberBuffer[0] >= ((byte.MaxValue / length) * length));
                return randomNumberBuffer[0] % length;
            }
        }
    }
}
