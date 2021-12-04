using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Pandaros.WoWParser.Parser.DataAccess.DTO
{
    [BsonIgnoreExtraElements]
    internal class WoWUser : IdEquatable<WoWUser>
    {

        internal string Username { get; set; }

        [BsonElement("_id")]
        internal string EmailAddress { get; set; }

        internal string PasswordHash { get; set; }

        internal bool WebAdmin { get; set; } = false;

        internal string AuthToken { get; set; }

        internal List<string> CharacterIDs { get; set; } = new List<string>();

        public bool EquilIds(WoWUser obj)
        {
            return EmailAddress == obj.EmailAddress;
        }

        public bool EquilIds(string id)
        {
            return EmailAddress == id;
        }

        public string GetId()
        {
            return EmailAddress;
        }
    }
}
