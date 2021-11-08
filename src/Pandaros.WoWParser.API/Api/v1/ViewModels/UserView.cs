using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Pandaros.WoWParser.API.Api.v1.ViewModels
{
    public class UserViewV1
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string EmailAddress { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        [Required]
        public bool WebAdmin { get; set; } = false;

        public List<string> CharacterIDs { get; set; } = new List<string>();
    }
}
