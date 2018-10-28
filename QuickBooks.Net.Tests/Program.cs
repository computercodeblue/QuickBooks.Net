using System;

namespace QuickBooks.Net.Tests
{
    public class Program
    {
        public static void Main(string[] args)
        {
            QuickBooksClient qb = new QuickBooksClient
            {
                ConsumerKey = "{consumerKey}",
                ConsumerSecret = "{consumerSecret}",
                AccessToken = "{access token}",
                AccessTokenSecret = "{access token secret}",
                CallbackUrl = "",
                RealmId = "{realmId}",
            };

            Console.WriteLine("Hello World!");
            Console.ReadLine();
        }
    }
}
