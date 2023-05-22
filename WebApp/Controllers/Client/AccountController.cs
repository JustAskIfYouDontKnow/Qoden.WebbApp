using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp.Database.Model;
using WebApp.Services;

namespace WebApp.Controllers.Client
{
    public class AccountController : AbstractClientController
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }
        
        [Authorize] 
        [HttpGet]
        public ValueTask<Account> GetAccountFromCookies()
        {
            var userIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "externalId");

            if (userIdClaim == null)
            {
                return default;
            }

            if (long.TryParse(userIdClaim.Value, out var userIdLong))
            {
                return _accountService.LoadOrCreateAsync(userIdLong);
            }
            
            return _accountService.LoadOrCreateAsync(userIdClaim.Value);
        }
        
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task <IActionResult> GetByInternalId(int id)
        {
            var accountInfo = await _accountService.GetFromCache(id);

            if (accountInfo is null)
            {
                return NoContent();
            }
            return Ok(accountInfo);
        }

        [Authorize]
        [HttpPost]
        public async Task UpdateCount()
        {
            var account = await GetAccountFromCookies();
            account.Counter++;
            _accountService.Update(account);
        }
    }
}