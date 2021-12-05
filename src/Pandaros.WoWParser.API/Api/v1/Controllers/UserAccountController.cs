using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Pandaros.WoWParser.API.Api.v1.ViewModels;
using Pandaros.WoWParser.Parser.Models;
using Pandaros.WoWParser.Parser.Repositories;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
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
    public class UserAccountController : Controller
    {
        private readonly ILogger<UserAccountController> _logger;
        UserRepo _userRepo;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="userRepo"></param>
        public UserAccountController(ILogger<UserAccountController> logger, UserRepo userRepo)
        {
            _logger = logger;
            _userRepo = userRepo;
        }

        /// <summary>
        ///     Creates a new User, returns the auth token. can populate auth token with oauth token for oauth account creation.
        /// </summary>
        /// <remarks>
        ///     Creates a new User, returns the auth token. can populate auth token with oauth token for oauth account creation.
        /// </remarks>
        /// <response code="200">New user was successfully created</response>
        /// <response code="409">User already exists. Email in use.</response>
        /// <response code="400">Missing reuired field.</response>
        [HttpPost, Route("CreateUser")]
        [MapToApiVersion("1.0")]
        [AllowAnonymous]
        public async Task<string> CreateUser([FromBody]CreateUserViewV1 user)
        {
            if (string.IsNullOrEmpty(user.EmailAddress) || string.IsNullOrEmpty(user.Password) || string.IsNullOrEmpty(user.Username) || string.IsNullOrEmpty(user.Timezone))
            {
                Response.StatusCode = StatusCodes.Status400BadRequest;
                return string.Empty;
            }

            var existingUser = await _userRepo.GetAsync(user.EmailAddress);

            if (existingUser != null)
            {
                Response.StatusCode = StatusCodes.Status409Conflict;
                return string.Empty;
            }

            var pwHash = Hash(user.Password);

            var newUser = new User()
            {
                EmailAddress = user.EmailAddress,
                Username = user.Username,
                Timezone = user.Timezone,
                WebAdmin = user.WebAdmin,
                PasswordHash = pwHash,
                AuthToken = GenerateJSONWebToken(pwHash)
            };

            await _userRepo.UpsertAsync(newUser);

            return newUser.AuthToken;
        }

        /// <summary>
        ///     Links a user to oAuth, once done, the oauth token will replace the password for logging in.
        /// </summary>
        /// <remarks>
        ///     Links a user to oAuth, once done, the oauth token will replace the password for logging in.
        /// </remarks>
        /// <response code="201">New user was successfully created</response>
        /// <response code="400">Missing reuired field.</response>
        /// <response code="404">User not found.</response>
        [HttpPost, Route("LinkUserToOauth")]
        [MapToApiVersion("1.0")]
        public async Task<string> LinkUserToOauth(string emailAddress, string oAuthToken)
        {
            if (string.IsNullOrEmpty(emailAddress) || string.IsNullOrEmpty(oAuthToken))
            {
                Response.StatusCode = StatusCodes.Status400BadRequest;
                return string.Empty;
            }

            var existingUser = await _userRepo.GetAsync(emailAddress);

            if (existingUser == null)
            {
                Response.StatusCode = StatusCodes.Status404NotFound;
                return string.Empty;
            }

            existingUser.PasswordHash = Hash(oAuthToken);
            existingUser.AuthToken = GenerateJSONWebToken(existingUser.PasswordHash);

            await _userRepo.UpsertAsync(existingUser);

            return existingUser.AuthToken;
        }

        /// <summary>
        ///     Gets a existing User, for viewing profiles
        /// </summary>
        /// <remarks>
        ///     Gets a existing User, for viewing profiles
        /// </remarks>
        /// <response code="200">Returns the user object</response>
        /// <response code="400">Missing reuired field.</response>
        /// <response code="404">User not found.</response>
        [HttpGet, Route("GetUser")]
        [MapToApiVersion("1.0")]
        public async Task<UserViewV1> GetUser(string emailAddress)
        {
            if(string.IsNullOrEmpty(emailAddress))
            {
                Response.StatusCode = StatusCodes.Status400BadRequest;
                return null;
            }

            var user = await _userRepo.GetAsync(emailAddress);

            if (user == null)
            {
                Response.StatusCode = StatusCodes.Status404NotFound;
                return null;
            }

            return new UserViewV1()
            {
                EmailAddress = user.EmailAddress,
                CharacterIDs = user.CharacterIDs,
                Username = user.Username,
                WebAdmin = user.WebAdmin
            };
        }

        /// <summary>
        ///     Logs a user in
        /// </summary>
        /// <remarks>
        ///     Logs a user in
        /// </remarks>
        /// <response code="200">Returns the user object</response>
        /// <response code="400">Missing reuired field.</response>
        /// <response code="401">Unautherized.</response>
        /// <response code="404">User not found.</response>
        [HttpGet, Route("LogIn")]
        [MapToApiVersion("1.0")]
        [AllowAnonymous]
        public async Task<UserViewV1> LogIn(string emailAddress, string password)
        {
            if (string.IsNullOrEmpty(emailAddress))
            {
                Response.StatusCode = StatusCodes.Status400BadRequest;
                return null;
            }

            var user = await _userRepo.GetAsync(emailAddress);

            if (user == null)
            {
                Response.StatusCode = StatusCodes.Status404NotFound;
                return null;
            }

            var pwHash = Hash(password);

            if (!pwHash.Equals(user.PasswordHash))
            {
                Response.StatusCode = StatusCodes.Status401Unauthorized;
                return null;
            }

            user.AuthToken = GenerateJSONWebToken(user.PasswordHash);

            await _userRepo.UpsertAsync(user);

            return new UserViewV1()
            {
                EmailAddress = user.EmailAddress,
                CharacterIDs = user.CharacterIDs,
                Username = user.Username,
                AuthToken = user.AuthToken,
                WebAdmin = user.WebAdmin
            };
        }

        /// <summary>
        ///     Logs a user out
        /// </summary>
        /// <remarks>
        ///     Logs a user out
        /// </remarks>
        /// <response code="201">Logged out on the server</response>
        /// <response code="400">Missing reuired field.</response>
        /// <response code="404">User not found.</response>
        [HttpGet, Route("LogOut")]
        [MapToApiVersion("1.0")]
        public async Task LogOut(string emailAddress)
        {
            if (string.IsNullOrEmpty(emailAddress))
            {
                Response.StatusCode = StatusCodes.Status400BadRequest;
                return;
            }

            var user = await _userRepo.GetAsync(emailAddress);

            if (user == null)
            {
                Response.StatusCode = StatusCodes.Status404NotFound;
                return;
            }

            user.AuthToken = string.Empty;

            await _userRepo.UpsertAsync(user);
        }

        private string GenerateJSONWebToken(string hash)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(hash));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "PandarosWowParser",
                audience: "PandarosWowParser",
                expires: DateTime.Now.AddHours(3),
                signingCredentials: credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private string Hash(string password)
        {
            // generate a 128-bit salt using a cryptographically strong random sequence of nonzero values
            // Placeholder salt for github
            byte[] salt = Encoding.UTF8.GetBytes("PandarosWoWParserPlaceholderSalt");
            using (var rngCsp = new RNGCryptoServiceProvider())
            {
                rngCsp.GetNonZeroBytes(salt);
            }

            // derive a 256-bit subkey (use HMACSHA256 with 100,000 iterations)
            return Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 100000,
                numBytesRequested: 256 / 8));
        }
    }
}
