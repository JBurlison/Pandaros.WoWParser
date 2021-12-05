using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper.Configuration.Conventions;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Pandaros.WoWParser.Parser.Models;

namespace Pandaros.WoWParser.Parser.DataAccess.DTO
{
    [BsonIgnoreExtraElements]
    public class WoWUser : BaseDto
    {
        public WoWUser() { }

        public WoWUser(User user)
        {
            Username = user.Username;
            Id = user.EmailAddress;
            WebAdmin = user.WebAdmin;
            AuthToken = user.AuthToken;
            CharacterIDs = user.CharacterIDs;
            PasswordHash = user.PasswordHash;
            Timezone = user.Timezone;
        }

        [BsonElement]
        internal string Username { get; set; }

        [BsonElement]
        internal string PasswordHash { get; set; }

        [BsonElement]
        internal bool WebAdmin { get; set; } = false;

        [BsonElement]
        public string Timezone { get; set; }

        [BsonElement]
        internal string AuthToken { get; set; }

        [BsonElement]
        internal List<string> CharacterIDs { get; set; } = new List<string>();
    }
}
