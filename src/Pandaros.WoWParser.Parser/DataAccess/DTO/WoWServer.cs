using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pandaros.WoWParser.Parser.DataAccess.DTO
{
    [BsonIgnoreExtraElements]
    internal class WoWServer : IdEquatable<WoWServer>
    {
        [BsonElement("_id")]
        internal string ID { get; set; }
        internal string Name { get; set; }
        internal string Region { get; set; }
        internal string HostID { get; set; }

        public bool EquilIds(WoWServer obj)
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
