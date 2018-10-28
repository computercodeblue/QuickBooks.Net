using System.Threading.Tasks;
using QuickBooks.Net.Payments.Data.Models;

namespace QuickBooks.Net.Controllers
{
    public interface IECheckController
    {
        Task<ECheck> CreateAsync(ECheck eCheck);

        Task<ECheck> GetAsync(string id);
    }
}
