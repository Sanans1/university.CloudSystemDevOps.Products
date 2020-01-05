using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace DevOps.Products.Website.Tests
{
    public static class AuthenticationStateService
    {
        public static ParameterView AuthenticationStateCreator(params Claim[] claims)
        {
            ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity());

            if (claims.Any())
            {
                claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme));
            }

            Task<AuthenticationState> authenticationStateTask = Task.FromResult(new AuthenticationState(claimsPrincipal));
            return ParameterView.FromDictionary(new Dictionary<string, object> { ["AuthenticationState"] = authenticationStateTask });
        }
    }
}
