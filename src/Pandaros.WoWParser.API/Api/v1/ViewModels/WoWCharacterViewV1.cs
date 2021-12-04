using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pandaros.WoWParser.API.Api.v1.ViewModels
{
    public class WoWCharacterViewV1
    {
        public string ID { get; set; }
        public string CharacterName { get; set; }
        public string PlayerID { get; set; }
        public string GuildId { get; set; }
        public string Class { get; set; }
        public string Role { get; set; }

        // key: Instance name, Date of instance, with the instance id.
        public Dictionary<string, Dictionary<string, string>> InstanceIds { get; set; } = new Dictionary<string, Dictionary<string, string>>();

    }
}
