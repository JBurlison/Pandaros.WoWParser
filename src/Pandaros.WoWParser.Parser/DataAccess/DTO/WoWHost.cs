using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pandaros.WoWParser.Parser.DataAccess.DTO
{
    [BsonIgnoreExtraElements]
    public class WoWHost : BaseDto
    {
        internal string Name { get; set; }
        internal string URL { get; set; }

    }
}
