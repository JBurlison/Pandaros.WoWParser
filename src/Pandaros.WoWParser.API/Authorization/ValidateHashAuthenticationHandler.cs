using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Pandaros.WoWParser.Parser.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace Pandaros.WoWParser.API.Authorization
{
    public class UserId : IIdentity
    {
        public UserId(string email, bool auth)
        {
            IsAuthenticated = auth;
            Name = email;
        }

        public string AuthenticationType { get; set; } = "User";

        public bool IsAuthenticated { get; set; }

        public string Name { get; set; }
    }

    public class ValidateHashAuthenticationSchemeOptions : AuthenticationSchemeOptions
    {

    }

    public class ValidateHashAuthenticationHandler : AuthenticationHandler<ValidateHashAuthenticationSchemeOptions>
    {

        UserRepo _userRepo;
        public ValidateHashAuthenticationHandler(
            IOptionsMonitor<ValidateHashAuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock,
            UserRepo userRepo)
            : base(options, logger, encoder, clock)
        {
            _userRepo = userRepo;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (Request.Headers.TryGetValue("panda-user", out var email))
            {
                var existing = await _userRepo.GetAsync(email);

                if (existing == null)
                    return AuthenticateResult.Fail("Unknown message.");

                if (Request.Headers.TryGetValue("panda-token", out var accessToken) && existing.AuthToken.Equals(accessToken))
                {
                    return AuthenticateResult.Success(new AuthenticationTicket(new ClaimsPrincipal(new UserId(existing.EmailAddress, true)), "PandaAuth"));
                }
            }

            return AuthenticateResult.Fail("Unknown message.");
        }

    }
}
