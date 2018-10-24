using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickBooks.Net.Data.Models.Fields
{
    public class PaymentContext
    {
        public decimal Tax { get; set; }

        public DeviceInfo DeviceInfo { get; set; }

        public bool Mobile { get; set; }

        public bool IsEcommerce { get; set; }

        public bool Recurring { get; set; }
    }
}
