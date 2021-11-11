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
    public class FightController
    {
        private readonly ILogger<UserAccountController> _logger;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public FightController(ILogger<UserAccountController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        ///     Gets a fight by Id
        /// </summary>
        /// <remarks>
        ///     Gets a fight by Id
        /// </remarks>
        /// <response code="201">The Character information</response>
        [HttpPost, Route("GetFight")]
        [MapToApiVersion("1.0")]
        public WoWFight GetFight(string id)
        {
            return new WoWFight()
            {
                FightId = "CF2EC121-83D2-4DBA-92CD-F42E7AA4B81B",
                CharacterIds = new List<string>() { "E8C291E6-BCE6-4033-9637-2E6E84045826" },
                StartTime = DateTime.Now - TimeSpan.FromMinutes(7),
                EndTime = DateTime.Now,
                InstanceId = "92183C73-0112-41AC-9441-928EDDFE1E18",
                StatIndexes = new List<string>() { "Healing" }
            };
        }
    }
}
