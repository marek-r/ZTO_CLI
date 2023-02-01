using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ZTO_CLI
{
    /// <summary>
    /// Zbiór funkcji haszowania.
    /// </summary>
    internal class Krypto
    {
        /// <summary>
        /// Hasło przekonwertowane na tablicę bajtów
        /// </summary>
        private byte[]? TempHaslo
        {
            get; set;
        }

        /// <summary>
        /// Tablica bajtów zawierająca hasz.
        /// </summary>
        private byte[]? Hasz
        {
            get; set;
        }

        /// <summary>
        /// Generuje hasz MD5
        /// </summary>
        /// <param name="haslo">Ciąg znaków</param>
        /// <returns>Hasz w postaci tablicy bajtów.</returns>
        public string GenerujMD5(string haslo)
        {
            try
            {
                TempHaslo = ASCIIEncoding.ASCII.GetBytes(haslo);
                Hasz = MD5.HashData(TempHaslo);
                return ByteArrayToString(Hasz);
            }
            catch (Exception error)
            {
                Console.WriteLine(error.Message.ToString());
                throw;
            }
        }

        /// <summary>
        /// Konwertuje tablicę bajtów na string
        /// </summary>
        /// <param name="byteArray">Tablica bajtów</param>
        /// <returns></returns>
        static string ByteArrayToString(byte[] byteArray)
        {
            int i;
            StringBuilder pass = new StringBuilder(byteArray.Length);
            for (i = 0; i < byteArray.Length; i++)
            {
                pass.Append(byteArray[i].ToString("X2"));
            }
            return pass.ToString();
        }

    }
}
