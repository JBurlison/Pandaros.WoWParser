using AutoMapper.Configuration.Conventions;
using Pandaros.WoWParser.Parser.DataAccess.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pandaros.WoWParser.Parser.Models
{
    public class User
    {
        public User() { }
        public User(WoWUser user)
        {
            EmailAddress = user.Id;
            Username = user.Username;
            WebAdmin = user.WebAdmin;
            AuthToken = user.AuthToken;
            CharacterIDs = user.CharacterIDs;
            PasswordHash = user.PasswordHash;
            Timezone = user.Timezone;
        }

        public string Username { get; set; }

        public string EmailAddress { get; set; }

        public string PasswordHash { get; set; }
        public string Timezone { get; set; }
        public string AuthToken { get; set; }
        public bool WebAdmin { get; set; } = false;

        public List<string> CharacterIDs { get; set; } = new List<string>();
    }
}
