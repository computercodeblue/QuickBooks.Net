using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using QuickBooks.Net.Payments.Data.Models;

namespace QuickBooks.Net.Controllers
{
    class ECheckController : BasePaymentsController, IECheckController
    {
        public ECheckController(QuickBooksClient client, string oAuthVersion) : base(client, oAuthVersion)
        {
        }

        public async Task<ECheck> CreateAsync(ECheck eCheck)
        {
            return await MakeRequest<ECheck>("ECheck", "payments/echecks", HttpMethod.Post, JsonConvert.SerializeObject(eCheck));
        }

        public async Task<ECheck> GetAsync(string id)
        {
            return await MakeRequest<ECheck>("ECheck", "payments/echecks/" + id, HttpMethod.Get);
        }
    }
}
