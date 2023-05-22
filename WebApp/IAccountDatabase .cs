using System.Threading.Tasks;
using WebApp.Model;

namespace WebApp
{
    public interface IAccountDatabase
    {
        Task<Account> GetOrCreateAccountAsync(string id);

        Task<Account> GetOrCreateAccountAsync(long id);

        Task<Account> FindByUserNameAsync(string userName);

        Task ResetAsync();
    }
}