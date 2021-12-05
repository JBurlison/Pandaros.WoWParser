using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pandaros.WoWParser.Parser.DataAccess.DTO
{
    public abstract class BaseDto
    {
        [BsonElement("_id")]
        internal virtual string Id { get; set; }
    }
}
