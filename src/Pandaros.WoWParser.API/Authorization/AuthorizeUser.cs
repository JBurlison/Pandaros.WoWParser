using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Pandaros.WoWParser.Parser.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;

namespace Pandaros.WoWParser.API.Authorization
{
    public class AuthorizeUser : IAuthenticationService
    {
        UserRepo _userRepo;

        public AuthorizeUser(UserRepo userRepo)
        {
            _userRepo = userRepo;
        }

        public async Task<AuthenticateResult> AuthenticateAsync(HttpContext context, string scheme)
        {
            if (context.Request.Headers.TryGetValue("panda-user", out var email))
            {
                var existing = await _userRepo.GetAsync(email);

                if (existing == null)
                    return AuthenticateResult.Fail("Unknown message.");

                if (context.Request.Headers.TryGetValue("panda-token", out var accessToken) && existing.AuthToken.Equals(accessToken))
                {
                    return AuthenticateResult.Success(new AuthenticationTicket(new ClaimsPrincipal(new UserId(existing.EmailAddress, true)), "PandaAuth"));
                }
            }

            return AuthenticateResult.Fail("Unknown message.");
        }

        public async Task ChallengeAsync(HttpContext context, string scheme, AuthenticationProperties properties)
        {
            if (context.Request.Headers.TryGetValue("panda-user", out var email))
            {
                var existing = await _userRepo.GetAsync(email);

                if (existing == null || !context.Request.Headers.TryGetValue("panda-token", out var accessToken) || !existing.AuthToken.Equals(accessToken))
                {
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    return;
                }
            }

            return;
        }

        public Task ForbidAsync(HttpContext context, string scheme, AuthenticationProperties properties)
        {
            return Task.CompletedTask;
        }

        public Task SignInAsync(HttpContext context, string scheme, ClaimsPrincipal principal, AuthenticationProperties properties)
        {
            return Task.CompletedTask;
        }

        public Task SignOutAsync(HttpContext context, string scheme, AuthenticationProperties properties)
        {
            return Task.CompletedTask;
        }
    }

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
}
