using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    public abstract class Crypting
    {
        public abstract string Name { get; }
        public abstract string Description { get; }

        public abstract string Encyrypt(string content, int? shift, string? key);

        public abstract string Decyrypt(string content, int? shift, string? key);

    }
}
