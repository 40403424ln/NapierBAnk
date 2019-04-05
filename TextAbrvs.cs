using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NapierBank
{
    public class TextAbrvs
    {
        public string Abb { get; set; }
        public string Translation { get; set; }

        public TextAbrvs() : this(string.Empty, string.Empty)
        {

        }

        public TextAbrvs(string abb, string translation)
        {
            Abb = abb;
            Translation = translation;
        }
    }
}
