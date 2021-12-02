using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Pandaros.WoWLogParser.Parser.DataAccess.DTO
{
    [BsonIgnoreExtraElements]
    public class User
    {

        public string Username { get; set; }

        [BsonElement("_id")]
        public string EmailAddress { get; set; }

        public bool WebAdmin { get; set; } = false;

        public string AuthToken { get; set; }

        public List<string> CharacterIDs { get; set; } = new List<string>();
    }
}
