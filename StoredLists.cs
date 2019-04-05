using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NapierBank
{
    public class StoredLists
    {
        public List<string> mentions { get; set; }
        public List<string> trending { get; set; }
        public List<string> sir { get; set; }
        public List<string> noi { get; set; }

        public List<string> ids { get; set; }

        public StoredLists()
        {
            mentions = new List<string>();
            trending = new List<string>();
            sir = new List<string>();
            noi = new List<string>();
            ids = new List<string>();
        }
    }
}
