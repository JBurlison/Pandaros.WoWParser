using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pandaros.WoWParser.API.DomainModels
{
    public class WoWInstance
    {
        public string InstanceId { get; set; }
        public string InstanceName { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public List<string> CharacterIds { get; set; } = new List<string>();
        public List<string> GuildIds { get; set; } = new List<string>();
        public List<string> FightIds { get; set; } = new List<string>();
    }
}
