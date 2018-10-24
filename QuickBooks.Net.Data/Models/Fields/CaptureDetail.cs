using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickBooks.Net.Data.Models.Fields
{
    public class CaptureDetail
    {
        public DateTime Created { get; set; }

        public decimal Amount { get; set; }

        public PaymentContext Context { get; set; }

        public string Description { get; set; }
    }
}
