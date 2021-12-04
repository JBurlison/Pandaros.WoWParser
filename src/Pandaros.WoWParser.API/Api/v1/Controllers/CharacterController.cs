using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Pandaros.WoWParser.API.Api.v1.ViewModels;
using Pandaros.WoWParser.API.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pandaros.WoWParser.API.Api.v1.Controllers
{
    /// <summary>
    ///     
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Route("[controller]")]
    public class CharacterController
    {
        private readonly ILogger<UserAccountController> _logger;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public CharacterController(ILogger<UserAccountController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        ///     Gets a character by Id
        /// </summary>
        /// <remarks>
        ///     Gets a character by Id
        /// </remarks>
        /// <response code="201">The Character information</response>
        [HttpGet, Route("GetCharacter")]
        [MapToApiVersion("1.0")]
        public WoWCharacterViewV1 GetCharacter(string id)
        {
            return new WoWCharacterViewV1()
            {
                PlayerID = "Pandaros",
                ID = "E8C291E6-BCE6-4033-9637-2E6E84045826",
                CharacterName = "Pandaros",
                GuildId = "",
                InstanceIds = new Dictionary<string, Dictionary<string, string>>()
                {
                    { "Serpentshrine Cavern", new Dictionary<string, string>() { { DateTime.UtcNow.ToString("O"), "92183C73-0112-41AC-9441-928EDDFE1E18" } } }
                }
            };
        }
    }
}
