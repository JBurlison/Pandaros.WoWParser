using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pandaros.WoWParser.Parser.Models
{
    public class Server
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string Region { get; set; }
        public string HostID { get; set; }
    }
}
