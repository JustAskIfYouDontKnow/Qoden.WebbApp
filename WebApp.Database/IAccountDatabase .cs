using System.Threading.Tasks;
using WebApp.Database.Model;

namespace WebApp.Database
{
    public interface IAccountDatabase
    {
        Task<Account> GetOrCreateAccountAsync(string id);

        Task<Account> GetOrCreateAccountAsync(long id);

        Task<Account> FindByUserNameAsync(string userName);

        Task ResetAsync();
    }
}