using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pandaros.WoWParser.Parser.Models
{
    public class Instance
    {
        public string InstanceId { get; set; }
        public string InstanceName { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public List<string> CharacterIds { get; set; } = new List<string>();
        public List<string> GuildIds { get; set; } = new List<string>();
        /// <summary>
        ///  In order of fights that happend.
        /// </summary>
        public List<string> FightIds { get; set; } = new List<string>();
    }
}
