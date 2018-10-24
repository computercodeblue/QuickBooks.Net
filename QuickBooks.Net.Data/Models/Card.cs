using System;

using QuickBooks.Net.Data.Models.Fields;

namespace QuickBooks.Net.Data.Models
{
    public enum CardTypes
    {
        VISA,
        MC,
        AMEX,
        DISC,
        DINERS,
        JCB
    }

    public class Card : QuickBooksBaseModelString
    {
        public DateTime Created { get; set; }

        public DateTime Updated { get; set; }

        public string Number { get; set; }

        public string ExpYear { get; set; }

        public string Cvc { get; set; }

        public string Name { get; set; }

        public CardholderAddress Address { get; set; }

        public bool Default { get; set; }

        public string CommercialCardCode { get; set; }

        public string CvcVerification { get; set; }

        public bool IsBusiness { get; set; }

        public CardTypes CardType { get; set; }

        public bool Capture { get; set; }

        public string AuthCode { get; set; }

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
