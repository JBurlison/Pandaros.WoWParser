using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pandaros.WoWParser.Parser.DataAccess.DTO
{
    [BsonIgnoreExtraElements]
    internal class WoWHost : IdEquatable<WoWHost>
    {
        [BsonElement("_id")]
        internal string ID { get; set; }
        internal string Name { get; set; }
        internal string URL { get; set; }

        public bool EquilIds(WoWHost obj)
        {
            return ID == obj.ID;
        }

        public bool EquilIds(string id)
        {
            return ID == id;
        }

        public string GetId()
        {
            return ID;
        }
    }
}
