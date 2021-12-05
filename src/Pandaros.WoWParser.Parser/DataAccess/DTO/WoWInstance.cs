using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pandaros.WoWParser.Parser.DataAccess.DTO
{
    [BsonIgnoreExtraElements]
    public class WoWInstance : BaseDto
    {
        [BsonElement]
        internal string InstanceName { get; set; }
        [BsonElement]
        internal DateTime StartTime { get; set; }
        [BsonElement]
        internal DateTime EndTime { get; set; }
        [BsonElement]
        internal List<string> CharacterIds { get; set; } = new List<string>();
        [BsonElement]
        internal List<string> GuildIds { get; set; } = new List<string>();
        /// <summary>
        ///  In order of fights that happend.
        /// </summary>
        [BsonElement]
        internal List<string> FightIds { get; set; } = new List<string>();

    }
}
