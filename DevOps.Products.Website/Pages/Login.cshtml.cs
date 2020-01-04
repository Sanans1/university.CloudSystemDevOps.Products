using System;
using System.Collections.Generic;
using System.Security.Authentication;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DevOps.Products.Website.Pages
{
    [AllowAnonymous]
    public class LoginModel : PageModel
    {
        private const string CLAIM_ROLE_CUSTOMER = "Customer";
        private const string CLAIM_ROLE_STAFF = "Staff";
        private const string STAFF_PASSWORD = "246813579";

        public string ReturnUrl { get; set; }

        public async Task<IActionResult>
            OnGetAsync(string paramUsername, string paramPassword)
        {
            try
            {
                // Clear the existing external cookie
                await HttpContext
                    .SignOutAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme);
            }
            catch
            {
                // ignored
            }

            // Not how we should get claims, however as the login system was not my responsibility this satisfies my needs.
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, paramUsername),
            };
            
            //This is purely for demonstration and testing purposes.
            switch (paramUsername)
            {
                case "Lewis":
                    claims.Add(new Claim(ClaimTypes.Role, CLAIM_ROLE_STAFF));
                    break;
                case "Bill":
                case "Bob":
                    claims.Add(new Claim(ClaimTypes.Role, CLAIM_ROLE_CUSTOMER));
                    break;
                default:
                    throw new InvalidCredentialException();
            }

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme);

            AuthenticationProperties authProperties = new AuthenticationProperties
            {
                IsPersistent = true,
                RedirectUri = Request.Host.Value
            };

            try
            {
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity), authProperties);
            }
            catch (Exception ex)
            {
                string error = ex.Message;
            }

            return LocalRedirect(Url.Content("~/"));
        }
    }
}
