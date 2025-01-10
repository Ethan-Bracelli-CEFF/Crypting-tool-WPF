using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    public class CaesarCipher : Crypting
    {
        public override string Name => "CaesarCipher";

        public override string Description => "The simple and very well known Caesar Cipher use an aditionnal number to shift each letter in the alphabet.";

        public override string Encyrypt(string content, int? shift)
        {
            int shiftValue = shift.Value;

            return new string(content.Select(c =>
            {
                if (char.IsLetter(c))
                {
                    char offset = char.IsUpper(c) ? 'A' : 'a';
                    return (char)((c + shiftValue - offset) % 26 + offset);
                }
                return c;
            }).ToArray());
        }

        public override string Decyrypt(string content, int? shift)
        {
            int shiftValue = shift.Value;

            return new string(content.Select(c =>
            {
                if (char.IsLetter(c))
                {
                    char offset = char.IsUpper(c) ? 'A' : 'a';
                    return (char)((c - shiftValue - offset + 26) % 26 + offset);
                }
                return c;
            }).ToArray());
        }
    }
}
