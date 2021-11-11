using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pandaros.WoWParser.API.DomainModels
{
    public class WoWCharacter
    {
        public string ID { get; set; }
        public string PlayerID { get; set; }
        public string GuildId { get; set; }

        // key: raid name, value list of raid ids
        public Dictionary<string, List<string>> RaidIds { get; set; } = new Dictionary<string, List<string>>();

    }
}
