using System.Collections.Generic;
using System.Threading.Tasks;
using QuickBooks.Net.Data.Models;
using QuickBooks.Net.Payments.Data.Models;

namespace QuickBooks.Net.Controllers
{
    interface ICardController
    {
        Task<Card> CreateAsync(Card card);

        Task<Card> CreateFromTokenAsync(string token);

        Task<Card> DeleteAsync(string id);

        Task<Card> DeleteAsync(Card card);

        Task<Card> GetAsync(string id);

        Task<IEnumerable<Card>> GetCustomerBankAccounts(string customerId);

        Task<IEnumerable<Card>> GetCustomerBankAccounts(Customer customer);
    }
}
