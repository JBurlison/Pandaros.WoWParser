using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pandaros.WoWParser.Parser.DataAccess.DTO
{
    [BsonIgnoreExtraElements]
    public class WoWInstanceInfo : BaseDto
    {
        [BsonElement]
        internal string Location { get; set; }
        [BsonElement]
        internal Dictionary<string, List<string>> BossFights { get; set; }
    }
}
