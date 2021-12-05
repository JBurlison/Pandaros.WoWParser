using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pandaros.WoWParser.Parser.DataAccess.DTO
{
    [BsonIgnoreExtraElements]
    public class WoWServer : BaseDto
    {
        [BsonElement]
        internal string Name { get; set; }
        [BsonElement]
        internal string Region { get; set; }
        [BsonElement]
        internal string HostID { get; set; }
    }
}
