using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using QuickBooks.Net.Data.Models;
using QuickBooks.Net.Payments.Data.Models;

namespace QuickBooks.Net.Controllers
{
    public class BankAccountController : BasePaymentsController, IBankAccountController
    {
        public BankAccountController(QuickBooksClient client, string oAuthVersion) : base(client, oAuthVersion)
        {
        }

        public async Task<BankAccount> CreateAsync(string customerId, BankAccount bankAccount)
        {
            return await MakeRequest<BankAccount>("BankAccount", "customers/" + customerId + "/bank-accounts", HttpMethod.Post, JsonConvert.SerializeObject(bankAccount));
        }

        public async Task<BankAccount> CreateAsync(Customer customer, BankAccount bankAccount)
        {
            return await CreateAsync(customer.Id.ToString(), bankAccount);
        }

        public async Task<BankAccount> CreateFromTokenAsync(string customerId, Token token)
        {
            return await MakeRequest<BankAccount>("BankAccount", "customers/" + customerId + "/bank-accounts", HttpMethod.Post, JsonConvert.SerializeObject(token));
        }

        public async Task<BankAccount> CreateFromTokenAsync(Customer customer, Token token)
        {
            return await CreateFromTokenAsync(customer.Id.ToString(), token);
        }

        public async Task<BankAccount> DeleteAsync(string customerId, string bankAccountId)
        {
            return await MakeRequest<BankAccount>("BankAccount", "customers/" + customerId + "/bank-accounts/" + bankAccountId, HttpMethod.Delete, null, false, true);
        }

        public async Task<BankAccount> DeleteAsync(Customer customer, string bankAccountId)
        {
            return await DeleteAsync(customer.Id.ToString(), bankAccountId);
        }

        public async Task<BankAccount> DeleteAsync(string customerId, BankAccount bankAccount)
        {
            return await DeleteAsync(customerId, bankAccount.Id);
        }

        public async Task<BankAccount> DeleteAsync(Customer customer, BankAccount bankAccount)
        {
            return await DeleteAsync(customer.Id.ToString(), bankAccount.Id);
        }

        public async Task<BankAccount> GetAsync(string customerId, string bankAccountId)
        {
            return await MakeRequest<BankAccount>("BankAccount", "customers/" + customerId + "/bank-accounts/" + bankAccountId, HttpMethod.Get);
        }

        public async Task<BankAccount> GetAsync(Customer customer, string bankAccountId)
        {
            return await GetAsync(customer.Id.ToString(), bankAccountId);
        }

        public async Task<List<BankAccount>> GetCustomerBankAccountsAsync(string customerId)
        {
            return await Task.FromResult(new List<BankAccount>(await MakeRequest<BankAccount[]>("System.Array", "/customers/" + customerId + "/bank-accounts", HttpMethod.Get)));
        }

        public async Task<List<BankAccount>> GetCustomerBankAccountsAsync(Customer customer)
        {
            return await GetCustomerBankAccountsAsync(customer.Id.ToString());
        }
    }
}
