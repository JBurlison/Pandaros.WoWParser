using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pandaros.WoWParser.API.DomainModels
{
    public class WoWRaid
    {
        public string RaidId { get; set; }
        public string GuildId { get; set; }
        public string RaidName { get; set; }
        public List<string> CharacterIds { get; set; } = new List<string>();
        public List<string> GuildIds { get; set; } = new List<string>();
    }
}
