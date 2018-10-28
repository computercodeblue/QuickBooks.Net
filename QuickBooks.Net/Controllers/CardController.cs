using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using QuickBooks.Net.Data.Models;
using QuickBooks.Net.Payments.Data.Models;

namespace QuickBooks.Net.Controllers
{
    public class CardController : BasePaymentsController, ICardController
    {
        public CardController(QuickBooksClient client, string oAuthVersion) : base(client, oAuthVersion)
        {
        }

        public async Task<Card> CreateAsync(string customerId, Card Card)
        {
            return await MakeRequest<Card>("Card", "customers/" + customerId + "/bank-accounts", HttpMethod.Post, JsonConvert.SerializeObject(Card));
        }

        public async Task<Card> CreateAsync(Customer customer, Card Card)
        {
            return await CreateAsync(customer.Id.ToString(), Card);
        }

        public async Task<Card> CreateFromTokenAsync(string customerId, Token token)
        {
            return await MakeRequest<Card>("Card", "customers/" + customerId + "/bank-accounts", HttpMethod.Post, JsonConvert.SerializeObject(token));
        }

        public async Task<Card> CreateFromTokenAsync(Customer customer, Token token)
        {
            return await CreateFromTokenAsync(customer.Id.ToString(), token);
        }

        public async Task<Card> DeleteAsync(string customerId, string CardId)
        {
            return await MakeRequest<Card>("Card", "customers/" + customerId + "/bank-accounts/" + CardId, HttpMethod.Delete, null, false, true);
        }

        public async Task<Card> DeleteAsync(Customer customer, string CardId)
        {
            return await DeleteAsync(customer.Id.ToString(), CardId);
        }

        public async Task<Card> DeleteAsync(string customerId, Card Card)
        {
            return await DeleteAsync(customerId, Card.Id);
        }

        public async Task<Card> DeleteAsync(Customer customer, Card Card)
        {
            return await DeleteAsync(customer.Id.ToString(), Card.Id);
        }

        public async Task<Card> GetAsync(string customerId, string CardId)
        {
            return await MakeRequest<Card>("Card", "customers/" + customerId + "/bank-accounts/" + CardId, HttpMethod.Get);
        }

        public async Task<Card> GetAsync(Customer customer, string CardId)
        {
            return await GetAsync(customer.Id.ToString(), CardId);
        }

        public async Task<List<Card>> GetCustomerCardsAsync(string customerId)
        {
            return await Task.FromResult(new List<Card>(await MakeRequest<Card[]>("System.Array", "/customers/" + customerId + "/bank-accounts", HttpMethod.Get)));
        }

        public async Task<List<Card>> GetCustomerCardsAsync(Customer customer)
        {
            return await GetCustomerCardsAsync(customer.Id.ToString());
        }
    }
}
