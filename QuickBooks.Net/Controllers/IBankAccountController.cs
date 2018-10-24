using System.Collections.Generic;
using System.Threading.Tasks;
using QuickBooks.Net.Data.Models;
using QuickBooks.Net.Payments.Data.Models;

namespace QuickBooks.Net.Controllers
{
    public interface IBankAccountController
    {
        Task<BankAccount> CreateAsync(BankAccount bankAccount);

        Task<BankAccount> CreateFromTokenAsync(string token);

        Task<BankAccount> DeleteAsync(string id);

        Task<BankAccount> DeleteAsync(BankAccount bankAccount);

        Task<BankAccount> GetAsync(string id);

        Task<IEnumerable<BankAccount>> GetCustomerBankAccounts(string customerId);

        Task<IEnumerable<BankAccount>> GetCustomerBankAccounts(Customer customer);
    }
}
