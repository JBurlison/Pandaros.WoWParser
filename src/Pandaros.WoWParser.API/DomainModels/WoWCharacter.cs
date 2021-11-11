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

        // key: Instance name, Date of instance, with the instance id.
        public Dictionary<string, Dictionary<DateTime, string>> InstanceIds { get; set; } = new Dictionary<string, Dictionary<DateTime, string>>();

    }
}
