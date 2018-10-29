using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using QuickBooks.Net.Payments.Data.Models;


namespace QuickBooks.Net.Controllers
{
    public class ChargeController : BasePaymentsController, IChargeController
    {
        public ChargeController(QuickBooksClient client, string oAuthVersion) : base(client, oAuthVersion)
        {
        }

        public async Task<Charge> CaptureChargeAsync(string chargeId, CaptureCharge capture)
        {
            return await MakeRequest<Charge>("Charge", "payments/charges/" + chargeId + "/capture", HttpMethod.Post, capture, false, false);
        }

        public async Task<Charge> CreateChargeAsync(Charge charge)
        {
            return await MakeRequest<Charge>("Charge", "payments/charges", HttpMethod.Post, charge, false, false);
        }

        public async Task<Charge> GetChargeAsync(string chargeId)
        {
            return await MakeRequest<Charge>("Charge", "payments/charges/" + chargeId, HttpMethod.Get);
        }

        public async Task<Refund> GetRefundById(string chargeId, string refundId)
        {
            return await MakeRequest<Refund>("Refund", "payments/charges/" + chargeId + "/refunds/" + refundId, HttpMethod.Get);
        }

        public async Task<Refund> GetRefundById(Charge charge, string refundId)
        {
            return await GetRefundById(charge.Id, refundId);
        }

        public async Task<Refund> RefundChargeAsync(string chargeId, Refund refund)
        {
            return await MakeRequest<Refund>("Refund", "payments/charges/" + chargeId + "/refunds", HttpMethod.Post, refund);
        }

        public async Task<Refund> RefundChargeAsync(Charge charge, Refund refund)
        {
            return await RefundChargeAsync(charge.Id, refund);
        }
    }
}
