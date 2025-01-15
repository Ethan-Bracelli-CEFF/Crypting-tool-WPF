using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    internal class Hex64 : Crypting
    {
        public override string Name => "Hex64";
        public override string Description => "Apply a Caesar Cipher to each letter but using the order of the letter in the alphabet of the letter in the key with the same index.";
        public override string Encyrypt(string content, int? shift, string? key)
        {
            if (string.IsNullOrEmpty(content) || string.IsNullOrEmpty(key))
                throw new ArgumentException("Input or key cannot be null or empty.");

            List<int> keyShifts = GenerateShiftsFromKey(key);

            StringBuilder encryptedText = new StringBuilder();
            int keyLength = keyShifts.Count;

            for (int i = 0; i < content.Length; i++)
            {
                char c = content[i];
                shift = keyShifts[i % keyLength];

                if (char.IsLetter(c))
                {
                    char offset = char.IsUpper(c) ? 'A' : 'a';
                    char encryptedChar = (char)((((c - offset) + shift) % 26) + offset);
                    encryptedText.Append(encryptedChar);
                }
                else
                {
                    encryptedText.Append(c);
                }
            }

            return encryptedText.ToString();
        }
        public override string Decyrypt(string content, int? shift, string? key)
        {
            if (string.IsNullOrEmpty(content) || string.IsNullOrEmpty(key))
                throw new ArgumentException("Input or key cannot be null or empty.");

            List<int> keyShifts = GenerateShiftsFromKey(key);

            StringBuilder decryptedText = new StringBuilder();
            int keyLength = keyShifts.Count;

            for (int i = 0; i < content.Length; i++)
            {
                char c = content[i];
                shift = keyShifts[i % keyLength];

                if (char.IsLetter(c))
                {
                    char offset = char.IsUpper(c) ? 'A' : 'a';
                    char decryptedChar = (char)((((c - offset) - shift + 26) % 26) + offset);
                    decryptedText.Append(decryptedChar);
                }
                else
                {
                    decryptedText.Append(c);
                }
            }

            return decryptedText.ToString();
        }

        private List<int> GenerateShiftsFromKey(string key)
        {
            List<int> shifts = new List<int>();

            for (int i = 0; i < key.Length; i += 2)
            {
                string hexPair = key.Substring(i, 2);
                int asciiValue = Convert.ToInt32(hexPair, 16);
                char character = (char)asciiValue;

                if (char.IsLetter(character))
                {
                    int position = char.ToUpper(character) - 'A' + 1;
                    shifts.Add(position);
                }
            }

            return shifts;
        }

        public string GenerateKey()
        {
            const string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/";
            Random random = new Random();
            StringBuilder keyBuilder = new StringBuilder();

            for (int i = 0; i < 64; i++)
            {
                char randomChar = alphabet[random.Next(alphabet.Length)];
                keyBuilder.Append(randomChar);
            }

            string key = keyBuilder.ToString();
            StringBuilder hexKeyBuilder = new StringBuilder();
            foreach (char c in key)
            {
                hexKeyBuilder.Append(((int)c).ToString("X2"));
            }

            return hexKeyBuilder.ToString();
        }
    }
}
