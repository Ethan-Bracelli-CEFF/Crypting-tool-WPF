using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    public class EthanCrypt : Crypting
    {
        public override string Name => "Ethan Crypt";

        public override string Description => "A little algorithm that I made which convert each letter ascii code into an hexa value and put them after each other.";

        public override string Encyrypt(string content, int? shift, string? key)
        {
            StringBuilder hexResult = new StringBuilder();

            foreach (char c in content)
            {
                int asciiValue = (int)c;

                string hexValue = asciiValue.ToString("X2");

                hexResult.Append(hexValue);
            }

            return hexResult.ToString();
        }

        public override string Decyrypt(string content, int? shift, string? key)
        {
            if (string.IsNullOrEmpty(content) || content.Length % 2 != 0)
            {
                throw new ArgumentException("This text is not in the correct format to be decrypted by this algorithm.");
            }

            StringBuilder result = new StringBuilder();

            try
            {
                for (int i = 0; i < content.Length; i += 2)
                {
                    string hexPair = content.Substring(i, 2);

                    int asciiValue = Convert.ToInt32(hexPair, 16);

                    result.Append((char)asciiValue);
                }
            }
            catch (FormatException)
            {
                throw new ArgumentException("This text is not in the correct format to be decrypted by this algorithm.");
            }

            return result.ToString();
        }
    }
}
