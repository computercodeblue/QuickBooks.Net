using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using QuickBooks.Net.Data.Models.Fields;

namespace QuickBooks.Net.Data.Models
{
    public class Refund : QuickBooksBaseModelString
    {
        public DateTime Created { get; set; }

        public decimal Amount { get; set; }
        
        public PaymentContext Context { get; set; }

        public string Description { get; set; }

        internal override QuickBooksBaseModelString CreateReturnObject()
        {
            return this;
        }

        internal override QuickBooksBaseModelString UpdateReturnObject()
        {
            return this;
        }

        internal override QuickBooksBaseModelString DeleteReturnObject()
        {
            return this;
        }
    }
}
