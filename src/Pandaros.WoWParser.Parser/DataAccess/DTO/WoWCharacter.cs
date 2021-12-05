using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pandaros.WoWParser.Parser.DataAccess.DTO
{
    [BsonIgnoreExtraElements]
    public class WoWCharacter : BaseDto
    {
        internal string PlayerID { get; set; }
        internal string CharacterName { get; set; }
        internal string GuildId { get; set; }
        internal string Class { get; set; }
        internal string Role { get; set; }
        // key: Instance name, Date of instance, with the instance id.
        internal Dictionary<string, Dictionary<string, string>> InstanceIds { get; set; } = new Dictionary<string, Dictionary<string, string>>();
    }
}
