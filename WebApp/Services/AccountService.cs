using System.Threading.Tasks;
using WebApp.Model;

namespace WebApp.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountCache _cache;
        private readonly IAccountDatabase _db;

        public AccountService(IAccountCache cache, IAccountDatabase db)
        {
            _cache = cache;
            _db = db;
        }

        public async ValueTask<Account> GetFromCache(long id)
        {
            if (_cache.TryGetValue(id, out var account))
            {
                return account;
            }

            return null;
        }

        public async ValueTask<Account> LoadOrCreateAsync(string id)
        {
            if (_cache.TryGetValue(id, out var account))
            {
                return account;
            }

            account = await _db.GetOrCreateAccountAsync(id);
            _cache.AddOrUpdate(account);

            return account;
        }


        public async ValueTask<bool> UpdateAsync(Account account)
        {
           _cache.AddOrUpdate(account);
           return true;
        }


        public async ValueTask<Account> LoadOrCreateAsync(long id)
        {
            if (_cache.TryGetValue(id, out var account))
            {
                return account;
            }

            account = await _db.GetOrCreateAccountAsync(id);
            _cache.AddOrUpdate(account);

            return account;
        }
    }
}