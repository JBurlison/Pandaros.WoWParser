using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Pandaros.WoWParser.API.Api.v1.ViewModels;
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
    public class UserAccountController
    {
        private readonly ILogger<UserAccountController> _logger;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public UserAccountController(ILogger<UserAccountController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        ///     Creates a new User
        /// </summary>
        /// <remarks>
        ///     Creates a new User
        /// </remarks>
        /// <response code="201">New user was successfully created</response>
        [HttpPost, Route("CreateUser")]
        [MapToApiVersion("1.0")]
        public void CreateUser([FromBody]UserViewV1 user)
        {
            
        }

        /// <summary>
        ///     Creates a new User
        /// </summary>
        /// <remarks>
        ///     Creates a new User
        /// </remarks>
        /// <response code="201">New user was successfully created</response>
        [HttpGet, Route("GetUser")]
        [MapToApiVersion("1.0")]
        public UserViewV1 GetUser(string emailAddress)
        {
            return new UserViewV1()
            {
                EmailAddress = "foo@bar.com",
                Username = "Pandaros",
                WebAdmin = false,
                CharacterIDs = new List<string>()
                {
                    "E8C291E6-BCE6-4033-9637-2E6E84045826"
                }
            };
        }
    }
}
