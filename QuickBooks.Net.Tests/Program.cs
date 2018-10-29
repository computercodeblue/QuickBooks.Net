using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.Extensions.Configuration;

using QuickBooks.Net.Exceptions;
using QuickBooks.Net.Data.Models;
using QuickBooks.Net.Payments.Data.Models;
using QuickBooks.Net.Payments.Data.Models.Fields;

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
            var builder = new ConfigurationBuilder().AddJsonFile("appsettings.json");
            var configuration = builder.Build();

            TestCustomers testCustomers = new TestCustomers();
            Random rand = new Random();

            Console.WriteLine("{0} random credit card numbers and names read.", testCustomers.Customers.Count);
            Console.WriteLine();

            QuickBooksClient qb = new QuickBooksClient
            {
                ConsumerKey = configuration.GetSection("QuickBooksOptions")["ConsumerKey"],
                ConsumerSecret = configuration.GetSection("QuickBooksOptions")["ConsumerSecret"],
                AccessToken = configuration.GetSection("QuickBooksOptions")["AccessToken"],
                AccessTokenSecret = configuration.GetSection("QuickBooksOptions")["AccessTokenSecret"],
                CallbackUrl = configuration.GetSection("QuickBooksOptions")["CallbackUrl"],
                RealmId = configuration.GetSection("QuickBooksOptions")["RealmId"],
                SandboxMode = Convert.ToBoolean(configuration.GetSection("QuickBooksOptions")["SandboxMode"])
            };

            Console.WriteLine("Consumer Key: {0}", qb.ConsumerKey);
            Console.WriteLine("Consumer Secret: {0}", qb.ConsumerSecret);
            Console.WriteLine("Access Token: {0}", qb.AccessToken);
            Console.WriteLine("Access Token Secret: {0}", qb.AccessTokenSecret);
            Console.WriteLine("Callback Url: {0}", qb.CallbackUrl);
            Console.WriteLine("Realm Id: {0}", qb.RealmId);
            Console.WriteLine("Sandbox Mode: {0}", qb.SandboxMode);
            Console.WriteLine();

            List<Customer> customers = new List<Customer>(await qb.Customers.GetCustomersAsync());

            Customer qbCustomer = customers[rand.Next(0, customers.Count)];

            TestCustomer testCustomer = testCustomers.Customers[rand.Next(0, 2999)];

            Card card = new Card()
            {
                Address = new CardholderAddress()
                {
                    City = qbCustomer.BillingAddress.City,
                    Country = qbCustomer.BillingAddress.Country,
                    PostalCode = qbCustomer.BillingAddress.PostalCode,
                    StreetAddress = qbCustomer.BillingAddress.Line1,
                    Region = qbCustomer.BillingAddress.CountrySubDivisionCode
                },
                ExpMonth = testCustomer.Expiration.Month.ToString("D2"),
                ExpYear = testCustomer.Expiration.Year.ToString("D4"),
                Name = qbCustomer.GivenName + " " + qbCustomer.FamilyName,
                Cvc = testCustomer.Cvv,
                Number = testCustomer.CCNumber
            };

            Token token = new Token
            {
                Value = string.Empty
            };

            try
            {
                token = await qb.Tokens.CreateTokenAsync(card);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + Environment.NewLine + ex.StackTrace);
            }

            Charge charge = new Charge
            {
                Amount = (Convert.ToDecimal(rand.Next(0,99999)) + Convert.ToDecimal(rand.Next(0,99)) / 100),
                Token = token.Value,
                Currency = "USD",
                Capture = true,
                Context = new PaymentContext
                {
                    Tax = 0.00M,
                    Mobile = false,
                    IsEcommerce = true,
                    Recurring = false
                }
            };

            try
            {
                charge = await qb.Charges.CreateChargeAsync(charge);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + Environment.NewLine + ex.StackTrace);
            }

            Console.WriteLine("{0} QuickBooks customers for this company.", customers.Count);

            if (token != null)
                Console.WriteLine("Token Value: {0}", token.Value);

            if (charge != null)
            {
                Console.WriteLine("Customer Name: {0}", qbCustomer.DisplayName);
                Console.WriteLine("Customer Card Number: {0}", testCustomer.CCNumber);
                Console.WriteLine("Charge Amount: {0}", charge.Amount);
                Console.WriteLine("Charge Status: {0}", charge.Status);
                Console.WriteLine("Charge AuthCode: {0}", charge.AuthCode);
            }

            SalesReceipt receipt = new SalesReceipt
            {
                CustomerRef = new Data.Models.Fields.Ref
                {
                    Name = qbCustomer.DisplayName,
                    Value = qbCustomer.Id.ToString()
                },
                Lines = new List<Data.Models.Fields.Line_Items.Invoice_Line.InvoiceLine>(),
                CreditCardPayment = new Data.Models.Fields.Credit_Card.CreditCardPayment
                {
                    CreditChargeResponse = new Data.Models.Fields.Credit_Card.CreditChargeResponse
                    {
                        AuthCode = charge.AuthCode,
                        TransactionAuthorizationTime = charge.Created,
                        CcTransId = charge.Id,
                        Status = Data.Models.Fields.Credit_Card.CcPaymentStatus.Completed
                    },
                    CreditChargeInfo = new Data.Models.Fields.Credit_Card.CreditChargeInfo
                    {
                        Amount = charge.Amount,
                        CcExpiryMonth = testCustomer.Expiration.Month,
                        CcExpiryYear = testCustomer.Expiration.Year,
                        BillingAddressStreet = charge.Card.Address.StreetAddress,
                        NameOnAccount = charge.Card.Name,
                        PostalCode = charge.Card.Address.PostalCode,
                        ProcessPayment = true,
                        Type = charge.Card.CardType
                    }
                },
            };

            receipt.Lines.Add(new Data.Models.Fields.Line_Items.Invoice_Line.InvoiceLine
            {
                Amount = charge.Amount,
                Description = "Test payment",
                DetailType = Data.Models.Fields.Line_Items.Invoice_Line.LineDetailType.SalesItemLineDetail,
                LineDetails = new Data.Models.Fields.Line_Items.Invoice_Line.Line_Details.SalesItemLineDetail
                {
                    Quantity = 1,
                    UnitPrice = charge.Amount
                }
            });

            try
            {
                receipt = await qb.SalesReceipts.CreateSalesReceiptAsync(receipt);
            }
            catch (QuickBooksException ex)
            {
                Console.WriteLine(ex.Message);
                foreach (Data.Error_Responses.ErrorDetailResponse error in ex.Response.Fault.Errors)
                {
                    Console.WriteLine("Code: {0}", error.Code);
                    Console.WriteLine("Detail: {0}", error.Detail);
                    Console.WriteLine("Element: {0}", error.Element);
                    Console.WriteLine("Message: {0}", error.Message);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + Environment.NewLine + ex.StackTrace);
            }

            Console.WriteLine("Receipt {0} created for customer {1}", receipt.Id, qbCustomer.Id);

            TestCustomer randomCustomer = testCustomers.Customers[rand.Next(0, 2999)];

            Customer newCustomer = new Customer
            {
                GivenName = randomCustomer.FirstName,
                MiddleName = randomCustomer.MiddleInitial,
                FamilyName = randomCustomer.LastName,
                DisplayName = randomCustomer.FullName,
                BillingAddress = new Data.Models.Fields.Address
                {
                    Line1 = randomCustomer.StreetAddress,
                    City = randomCustomer.City,
                    Country = randomCustomer.Country,
                    CountrySubDivisionCode = randomCustomer.State,
                    PostalCode = randomCustomer.PostalCode
                }
            };

            try
            {
                newCustomer = await qb.Customers.CreateCustomerAsync(newCustomer);
            }
            catch (QuickBooksException ex)
            {
                Console.WriteLine(ex.Message);
                foreach (Data.Error_Responses.ErrorDetailResponse error in ex.Response.Fault.Errors)
                {
                    Console.WriteLine("Code: {0}", error.Code);
                    Console.WriteLine("Detail: {0}", error.Detail);
                    Console.WriteLine("Element: {0}", error.Element);
                    Console.WriteLine("Message: {0}", error.Message);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + Environment.NewLine + ex.StackTrace);
            }

            Console.WriteLine("New customer {0} created with ID {1}.", newCustomer.DisplayName, newCustomer.Id.ToString());

            Card newCard = new Card()
            {
                Address = new CardholderAddress()
                {
                    City = newCustomer.BillingAddress.City,
                    Country = newCustomer.BillingAddress.Country,
                    PostalCode = newCustomer.BillingAddress.PostalCode,
                    StreetAddress = newCustomer.BillingAddress.Line1,
                    Region = newCustomer.BillingAddress.CountrySubDivisionCode
                },
                ExpMonth = randomCustomer.Expiration.Month.ToString("D2"),
                ExpYear = randomCustomer.Expiration.Year.ToString("D4"),
                Name = newCustomer.GivenName + " " + qbCustomer.FamilyName,
                Cvc = randomCustomer.Cvv,
                Number = randomCustomer.CCNumber
            };

            List<Card> customerCards = new List<Card>();

            try
            {
                customerCards = await qb.Cards.GetCustomerCardsAsync(newCustomer);
            }
            catch (QuickBooksException ex)
            {
                Console.WriteLine(ex.Message);
                if (ex.Response != null)
                    if (ex.Response.Fault != null)
                        if (ex.Response.Fault.Errors != null)
                            foreach (Data.Error_Responses.ErrorDetailResponse error in ex.Response.Fault.Errors)
                            {
                                Console.WriteLine("Code: {0}", error.Code);
                                Console.WriteLine("Detail: {0}", error.Detail);
                                Console.WriteLine("Element: {0}", error.Element);
                                Console.WriteLine("Message: {0}", error.Message);
                            }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + Environment.NewLine + ex.StackTrace);
            }
            
            int cardCount = customerCards.Count;

            try
            {
                newCard = await qb.Cards.CreateAsync(newCustomer, newCard);
            }
            catch (QuickBooksException ex)
            {
                Console.WriteLine(ex.Message);
                foreach (Data.Error_Responses.ErrorDetailResponse error in ex.Response.Fault.Errors)
                {
                    Console.WriteLine("Code: {0}", error.Code);
                    Console.WriteLine("Detail: {0}", error.Detail);
                    Console.WriteLine("Element: {0}", error.Element);
                    Console.WriteLine("Message: {0}", error.Message);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + Environment.NewLine + ex.StackTrace);
            }

            try
            {
                customerCards = await qb.Cards.GetCustomerCardsAsync(newCustomer);
            }
            catch (QuickBooksException ex)
            {
                Console.WriteLine(ex.Message);
                foreach (Data.Error_Responses.ErrorDetailResponse error in ex.Response.Fault.Errors)
                {
                    Console.WriteLine("Code: {0}", error.Code);
                    Console.WriteLine("Detail: {0}", error.Detail);
                    Console.WriteLine("Element: {0}", error.Element);
                    Console.WriteLine("Message: {0}", error.Message);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + Environment.NewLine + ex.StackTrace);
            }

            Console.WriteLine("Card {0} created for customer {1}. Old count was {2}, new count is {3}.", newCard.Id, newCustomer.DisplayName, cardCount, customerCards.Count);

            Charge newCharge = new Charge
            {
                Amount = (Convert.ToDecimal(rand.Next(0, 99999)) + Convert.ToDecimal(rand.Next(0, 99)) / 100),
                CardOnFile = newCard.Id,
                Currency = "USD",
                Capture = true,
                Context = new PaymentContext
                {
                    Tax = 0.00M,
                    Mobile = false,
                    IsEcommerce = true,
                    Recurring = false
                }
            };

            try
            {
                newCharge = await qb.Charges.CreateChargeAsync(newCharge);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + Environment.NewLine + ex.StackTrace);
            }

            if (newCharge != null)
            {
                Console.WriteLine("Customer Name: {0}", newCustomer.DisplayName);
                Console.WriteLine("Charge Amount: {0}", newCharge.Amount);
                Console.WriteLine("Charge Status: {0}", newCharge.Status);
                Console.WriteLine("Charge AuthCode: {0}", newCharge.AuthCode);
            }

            SalesReceipt newReceipt = new SalesReceipt
            {
                CustomerRef = new Data.Models.Fields.Ref
                {
                    Name = newCustomer.DisplayName,
                    Value = newCustomer.Id.ToString()
                },
                Lines = new List<Data.Models.Fields.Line_Items.Invoice_Line.InvoiceLine>(),
                CreditCardPayment = new Data.Models.Fields.Credit_Card.CreditCardPayment
                {
                    CreditChargeResponse = new Data.Models.Fields.Credit_Card.CreditChargeResponse
                    {
                        AuthCode = newCharge.AuthCode,
                        TransactionAuthorizationTime = newCharge.Created,
                        CcTransId = newCharge.Id,
                        Status = Data.Models.Fields.Credit_Card.CcPaymentStatus.Completed
                    },
                    CreditChargeInfo = new Data.Models.Fields.Credit_Card.CreditChargeInfo
                    {
                        Amount = newCharge.Amount,
                        CcExpiryMonth = Convert.ToInt32(newCharge.Card.ExpMonth),
                        CcExpiryYear = Convert.ToInt32(newCharge.Card.ExpYear),
                        BillingAddressStreet = newCharge.Card.Address.StreetAddress,
                        NameOnAccount = newCharge.Card.Name,
                        PostalCode = newCharge.Card.Address.PostalCode,
                        ProcessPayment = true,
                        Type = newCharge.Card.CardType
                    }
                },
            };

            newReceipt.Lines.Add(new Data.Models.Fields.Line_Items.Invoice_Line.InvoiceLine
            {
                Amount = newCharge.Amount,
                Description = "Second test payment",
                DetailType = Data.Models.Fields.Line_Items.Invoice_Line.LineDetailType.SalesItemLineDetail,
                LineDetails = new Data.Models.Fields.Line_Items.Invoice_Line.Line_Details.SalesItemLineDetail
                {
                    Quantity = 1,
                    UnitPrice = newCharge.Amount
                }
            });

            newReceipt.Lines.Add(new Data.Models.Fields.Line_Items.Invoice_Line.InvoiceLine
            {
                Description = "Test discount percent",
                DetailType = Data.Models.Fields.Line_Items.Invoice_Line.LineDetailType.SalesItemLineDetail,
                LineDetails = new Data.Models.Fields.Line_Items.Invoice_Line.Line_Details.DiscountLineDetail
                {
                    PercentBased = true,
                    DiscountPercent = 25M
                }
            });

            try
            {
                newReceipt = await qb.SalesReceipts.CreateSalesReceiptAsync(newReceipt);
            }
            catch (QuickBooksException ex)
            {
                Console.WriteLine(ex.Message);
                foreach (Data.Error_Responses.ErrorDetailResponse error in ex.Response.Fault.Errors)
                {
                    Console.WriteLine("Code: {0}", error.Code);
                    Console.WriteLine("Detail: {0}", error.Detail);
                    Console.WriteLine("Element: {0}", error.Element);
                    Console.WriteLine("Message: {0}", error.Message);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + Environment.NewLine + ex.StackTrace);
            }

            Console.WriteLine("Receipt {0} created for customer {1}", newReceipt.Id, newCustomer.Id);

            Console.ReadLine();
        }
    }
}
