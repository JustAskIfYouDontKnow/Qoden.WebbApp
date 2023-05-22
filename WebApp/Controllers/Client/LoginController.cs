using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp.Model;

namespace WebApp.Controllers.Client
{
    public class LoginController : AbstractClientController
    {
        private readonly IAccountDatabase _db;

        public LoginController(IAccountDatabase db)
        {
            _db = db;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginAccount loginAccount)
        {
            var account = await _db.FindByUserNameAsync(loginAccount.UserName);

            if (account is null)
            {
                return NotFound();
            }

            var claims = new[]
            {
                new Claim("externalId", account.ExternalId),
                new Claim(ClaimTypes.Name, account.UserName),
                new Claim(ClaimTypes.Role, account.Role)
            };
        
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);
        
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
                
            return Ok($"Success login, your internal id {account.InternalId}");
        }
    }
}