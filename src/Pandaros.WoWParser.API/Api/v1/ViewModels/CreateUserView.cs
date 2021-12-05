using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Pandaros.WoWParser.API.Api.v1.ViewModels
{
    public class CreateUserViewV1
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string EmailAddress { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Timezone { get; set; }

        public bool WebAdmin { get; set; } = false;
    }
}
