using Microsoft.AspNetCore.Authorization;
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
    /// 
    [Authorize]
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
        ///     Creates a new User, or links oauth to existing user
        /// </summary>
        /// <remarks>
        ///     Creates a new User, or links oauth to existing user
        /// </remarks>
        /// <response code="201">New user was successfully created</response>
        [HttpPost, Route("CreateOAuthUser")]
        [MapToApiVersion("1.0")]
        [AllowAnonymous]
        public void CreateOAuthUser(string name, string email, string oAuthToken)
        {

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
        [AllowAnonymous]
        public void CreateUser([FromBody]CreateUserViewV1 user)
        {
            
        }

        /// <summary>
        ///     Gets a existing User
        /// </summary>
        /// <remarks>
        ///     Gets a existing User
        /// </remarks>
        /// <response code="201">Returns the user object</response>
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

        /// <summary>
        ///     Logs a user in
        /// </summary>
        /// <remarks>
        ///     Logs a user in
        /// </remarks>
        /// <response code="201">Returns the user object</response>
        [HttpGet, Route("LogIn")]
        [MapToApiVersion("1.0")]
        [AllowAnonymous]
        public UserViewV1 LogIn(string emailAddress, string password)
        {
            return new UserViewV1()
            {
                EmailAddress = "foo@bar.com",
                Username = "Pandaros",
                WebAdmin = false,
                AuthToken = "4A81B119-3914-4B5E-9CD4-2BACC0763FB2",
                CharacterIDs = new List<string>()
                {
                    "E8C291E6-BCE6-4033-9637-2E6E84045826"
                }
            };
        }

        /// <summary>
        ///     Logs a user out
        /// </summary>
        /// <remarks>
        ///     Logs a user out
        /// </remarks>
        /// <response code="201">Logged out on the server</response>
        [HttpGet, Route("LogOut")]
        [MapToApiVersion("1.0")]
        [AllowAnonymous]
        public void LogOut(string emailAddress)
        {
           
        }
    }
}
