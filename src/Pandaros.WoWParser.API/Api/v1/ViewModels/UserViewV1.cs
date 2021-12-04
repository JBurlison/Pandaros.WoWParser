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
        [Editable(true)]
        public string Username { get; set; }

        [Required]
        [Editable(false)]
        public string EmailAddress { get; set; }

        [Required]
        [Editable(true)]
        public bool WebAdmin { get; set; } = false;

        [Editable(false)]
        public string AuthToken { get; set; }

        [Editable(false)]
        public List<string> CharacterIDs { get; set; } = new List<string>();
    }
}
