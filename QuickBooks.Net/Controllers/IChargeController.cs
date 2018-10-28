using System.Threading.Tasks;
using QuickBooks.Net.Payments.Data.Models;

namespace QuickBooks.Net.Controllers
{
    public interface IChargeController
    {
        Task<Charge> GetChargeAsync(string chargeId);

        Task<Charge> CreateChargeAsync(Charge charge);

        Task<Charge> CaptureChargeAsync(string chargeId, CaptureCharge capture);

        Task<Refund> RefundChargeAsync(string chargeId, Refund refund);

        Task<Refund> RefundChargeAsync(Charge charge, Refund refund);

        Task<Refund> GetRefundById(string chargeId, string refundId);

        Task<Refund> GetRefundById(Charge charge, string refundId);
    }
}
