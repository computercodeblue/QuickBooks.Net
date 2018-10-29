using System;
using System.Collections.Generic;
using System.IO;

namespace QuickBooks.Net.Tests
{
    public class TestCustomer
    {
        public string FirstName { get; set; }
        public string MiddleInitial { get; set; }
        public string LastName { get; set; }
        public string FullName
        {
            get
            {
                return FirstName + (string.IsNullOrWhiteSpace(MiddleInitial) ? string.Empty : (" " + MiddleInitial + ". ")) + LastName;
            }
        }
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public string CCType { get; set; }
        public string CCNumber { get; set; }
        public string Cvv { get; set; }
        public DateTime Expiration { get; set; }
    }

    public class TestCustomers
    {
        public List<TestCustomer> Customers { get; set; }

        public TestCustomers()
        {
            Customers = this.LoadCustomers();
        }

        protected List<TestCustomer> LoadCustomers()
        {
            string fileName = @"test_names.csv";
            List<TestCustomer> customers = new List<TestCustomer>();
            char[] delimterChars = { ',' };
            int lineCount = 0;

            var location = System.Reflection.Assembly.GetEntryAssembly().Location;
            var directory = Path.GetDirectoryName(location);


            try
            {
                foreach (string line in File.ReadLines(directory + @"\" + fileName))
                {
                    string[] fields = line.Split(delimterChars);
                    if (lineCount > 0)
                    {
                        string[] dateString = fields[11].Split('/');

                        TestCustomer customer = new TestCustomer
                        {
                            FirstName = StripQuotes(fields[0]),
                            MiddleInitial = StripQuotes(fields[1]),
                            LastName = StripQuotes(fields[2]),
                            StreetAddress = StripQuotes(fields[3]),
                            City = StripQuotes(fields[4]),
                            State = StripQuotes(fields[5]),
                            PostalCode = StripQuotes(fields[6]),
                            Country = StripQuotes(fields[7]),
                            CCType = StripQuotes(fields[8]),
                            CCNumber = StripQuotes(fields[9]),
                            Cvv = StripQuotes(fields[10]),
                            Expiration = new DateTime(Convert.ToInt32(dateString[1]), Convert.ToInt32(dateString[0]), 1)
                        };

                        customers.Add(customer);
                    }
                    lineCount++;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Load Test Customers " + ex.Message);
            }

            return customers;
        }

        public static string StripQuotes(string inStr)
        {
            string outStr = inStr;
            int startIndex = inStr.IndexOf('"') + 1;
            int endIndex = inStr.LastIndexOf('"') - 1;
            int length = endIndex - startIndex + 1;
            if (length > 0)
            {
                outStr = inStr.Substring(startIndex, length);
            }
            return outStr;
        }
    }
}
