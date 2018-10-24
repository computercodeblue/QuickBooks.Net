using System.Threading.Tasks;
using QuickBooks.Net.Payments.Data.Models;

namespace QuickBooks.Net.Controllers
{
    public interface ITokenController
    {
        Task<Token> Create(Card card);

        Task<Token> Create(BankAccount bankAccount);
    }
}
