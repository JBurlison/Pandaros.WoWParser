using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pandaros.WoWParser.Parser.DataAccess.DTO
{
    [BsonIgnoreExtraElements]
    internal class WoWInstanceInfo : IdEquatable<WoWInstanceInfo>
    {
        [BsonElement("_id")]
        internal string Name { get; set; }

        internal string Location { get; set; }

        internal Dictionary<string, List<string>> BossFights { get; set; }
        public bool EquilIds(WoWInstanceInfo obj)
        {
            return Name == obj.Name;
        }

        public bool EquilIds(string id)
        {
            return Name == id;
        }

        public string GetId()
        {
            return Name;
        }

    }
}
