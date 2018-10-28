using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using QuickBooks.Net.Data.Models;
using QuickBooks.Net.Payments.Data.Models;

namespace QuickBooks.Net.Tests
{
    public class Program
    {
        static void Main(string[] args)
        {
            MainAsync(args).GetAwaiter().GetResult();
        }

        public async static Task MainAsync(string[] args)
        {
            QuickBooksClient qb = new QuickBooksClient
            {
                ConsumerKey = "lvprdnjlbJTTRHqnPQ4sEmBRqobhgB",
                ConsumerSecret = "FaauGpvwswMKSpassnaWr7J43CEPZFJg1WPXTBwo",
                AccessToken = "lvprdpQ7c9eTvtlHt6g9oRaEFljN8hPZelsuRoWdNuJyMo9w",
                AccessTokenSecret = "64hkS6iOaGRl68L15ifPamarMKImuMqsz3fEsTo3",
                CallbackUrl = "",
                RealmId = "123145773458229",
                SandboxMode = true
            };

            List<Customer> customers = new List<Customer>(await qb.Customers.GetCustomersAsync());

            Card card = new Card()
            {
                Address = new Payments.Data.Models.Fields.CardholderAddress()
                {
                    City = "Sunnyvale",
                    Country = "US",
                    PostalCode = "94086",
                    StreetAddress = "1130 Kifer Rd.",
                    Region = "CA"
                },
                ExpMonth = "02",
                ExpYear = "2020",
                Name = "Gonzo Perez",
                Cvc = "123",
                Number = "4111111111111111"
            };

            Token token = null;

            try
            {
                token = await qb.Tokens.CreateTokenAsync(card);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.WriteLine("Hello World!");
            Console.WriteLine("Customers: {0}", customers.Count);

            if (token != null)
                Console.WriteLine("Token Value: {0}", token.Value);

            Console.ReadLine();
        }
    }
}
