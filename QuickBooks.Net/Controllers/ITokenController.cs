using System.Threading.Tasks;
using QuickBooks.Net.Payments.Data.Models;

namespace QuickBooks.Net.Controllers
{
    public interface ITokenController
    {
        Task<Token> CreateTokenAsync(Card card);

        Task<Token> CreateTokenAsync(BankAccount bankAccount);
    }
}
