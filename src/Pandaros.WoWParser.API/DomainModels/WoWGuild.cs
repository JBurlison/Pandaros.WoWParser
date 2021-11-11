using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pandaros.WoWParser.API.DomainModels
{
    public class WoWGuild
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string URL { get; set; }
        public string ServerID { get; set; }
        public List<string> Owners { get; set; } = new List<string>();
        public List<string> Ranks { get; set; } = new List<string>();

        // key rank name, value list of player ids
        public Dictionary<string, List<string>> PlayersByRank { get; set; } = new Dictionary<string, List<string>>();

        // key: Instance name, Instance date, instance id
        public Dictionary<string, Dictionary<string, string>> InstacneIds { get; set; } = new Dictionary<string, Dictionary<string, string>>();
    }
}
