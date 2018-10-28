using System.Collections.Generic;
using System.Threading.Tasks;
using QuickBooks.Net.Data.Models;
using QuickBooks.Net.Payments.Data.Models;

namespace QuickBooks.Net.Controllers
{
    public interface IBankAccountController
    {
        Task<BankAccount> CreateAsync(string customerId, BankAccount bankAccount);

        Task<BankAccount> CreateAsync(Customer customer, BankAccount bankAccount);

        Task<BankAccount> CreateFromTokenAsync(string customerId, Token token);

        Task<BankAccount> CreateFromTokenAsync(Customer customer, Token token);

        Task<BankAccount> DeleteAsync(string customerId, string bankAccountId);

        Task<BankAccount> DeleteAsync(Customer customer, string bankAccountId);

        Task<BankAccount> DeleteAsync(string customerId, BankAccount bankAccount);

        Task<BankAccount> DeleteAsync(Customer customer, BankAccount bankAccount);

        Task<BankAccount> GetAsync(string customerId, string bankAccountId);

        Task<BankAccount> GetAsync(Customer customer, string bankAccountId);

        Task<List<BankAccount>> GetCustomerBankAccountsAsync(string customerId);

        Task<List<BankAccount>> GetCustomerBankAccountsAsync(Customer customer);
    }
}
