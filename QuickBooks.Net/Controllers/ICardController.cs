using System.Collections.Generic;
using System.Threading.Tasks;
using QuickBooks.Net.Data.Models;
using QuickBooks.Net.Payments.Data.Models;

namespace QuickBooks.Net.Controllers
{
    public interface ICardController
    {
        Task<Card> CreateAsync(string customerId, Card Card);

        Task<Card> CreateAsync(Customer customer, Card Card);

        Task<Card> CreateFromTokenAsync(string customerId, Token token);

        Task<Card> CreateFromTokenAsync(Customer customer, Token token);

        Task<Card> DeleteAsync(string customerId, string cardId);

        Task<Card> DeleteAsync(Customer customer, string cardId);

        Task<Card> DeleteAsync(string customerId, Card card);

        Task<Card> DeleteAsync(Customer customer, Card card);

        Task<Card> GetAsync(string customerId, string cardId);

        Task<Card> GetAsync(Customer customer, string cardId);

        Task<List<Card>> GetCustomerCardsAsync(string customerId);

        Task<List<Card>> GetCustomerCardsAsync(Customer customer);
    }
}
