using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pandaros.WoWParser.API.DomainModels
{
    public class ParserUser
    {
        public string Username { get; set; }

        public string EmailAddress { get; set; }

        public string PasswordHash { get; set; }
        public string Timezone { get; set; }

        public bool WebAdmin { get; set; } = false;

        public List<string> CharacterIDs { get; set; } = new List<string>();
    }
}
