using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using QuickBooks.Net.Payments.Data.Models;

namespace QuickBooks.Net.Controllers
{
    public class TokenController : BasePaymentsController, ITokenController
    {
        public TokenController(QuickBooksClient client, string oAuthVersion) : base(client, oAuthVersion)
        {
        }

        public async Task<Token> CreateTokenAsync(Card card)
        {
            return await MakeRequest<Token>("Token", "payments/tokens", HttpMethod.Post, new CardObject { Card = card });
        }

        public async Task<Token> CreateTokenAsync(BankAccount bankAccount)
        {
            return await MakeRequest<Token>("Token", "payments/tokens", HttpMethod.Post, JsonConvert.SerializeObject(bankAccount));
        }
    }
}
