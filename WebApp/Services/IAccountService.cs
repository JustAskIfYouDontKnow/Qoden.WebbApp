using System.Threading.Tasks;
using WebApp.Model;

namespace WebApp.Services
{
    public interface IAccountService
    {
        /// <summary>
        /// Get account from cache or return null of account is not in the cache.
        /// </summary>
        ValueTask<Account> GetFromCache(long id);
        
        /// <summary>
        /// Get account from cache or load it from db. 
        /// </summary>
        ValueTask<Account> LoadOrCreateAsync(string id);
        ValueTask<bool> UpdateAsync(Account account);
        ValueTask<Account> LoadOrCreateAsync(long id);
    }
}