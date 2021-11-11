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
    public class InstanceController
    {
        private readonly ILogger<UserAccountController> _logger;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public InstanceController(ILogger<UserAccountController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        ///     Gets a instance by Id
        /// </summary>
        /// <remarks>
        ///     Gets a instance by Id
        /// </remarks>
        /// <response code="201">The Character information</response>
        [HttpPost, Route("GetInstance")]
        [MapToApiVersion("1.0")]
        public WoWInstance GetInstance(string id)
        {
            return new WoWInstance()
            {
                InstanceId = "92183C73-0112-41AC-9441-928EDDFE1E18",
                CharacterIds = new List<string>() { "E8C291E6-BCE6-4033-9637-2E6E84045826" },
                StartTime = DateTime.UtcNow - TimeSpan.FromHours(2),
                EndTime = DateTime.UtcNow,
                InstanceName = "Serpentshrine Cavern",
                FightIds = new List<string>() { "CF2EC121-83D2-4DBA-92CD-F42E7AA4B81B" }
            };
        }
    }
}
