using System.Threading.Tasks;
using QuickBooks.Net.Payments.Data.Models;

namespace QuickBooks.Net.Controllers
{
    interface ITokenController
    {
        Task<Token> Create(Card card);

        Task<Token> Create(BankAccount bankAccount);
    }
}
