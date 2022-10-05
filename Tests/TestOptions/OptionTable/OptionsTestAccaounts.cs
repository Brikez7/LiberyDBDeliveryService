using ConsoleDatabase.ManagerDB;
using LiberyDBDeliveryService.Models.DB.Context;
using LiberyDBDeliveryService.Models.DB.IManagersTables;
using LiberyDBDeliveryService.Models.DB.Table;
using TestsDeliveryServiceLibery.Tests;
using Xunit;

namespace TestsDeliveryServiceLibery.TestOptions.OptionForTests
{
    public class OptionsTestAccaounts : IDisposable
    {
        internal IDBAccountManager _accountManager { get; private set; }
        internal List<Account> _accounts { get; private set; }
        public OptionsTestAccaounts()
        {
            _accountManager = new DBAccountManager();

            _accounts = CreateRandomAccounts("Dima", 1, "brykez", 100);

            _accountManager.ClearTableAccount();
            AddAccounts(_accounts);
        }
        private void AddAccounts(List<Account> newAccounts)
        {
            using (DeliveryServiceContext db = new DeliveryServiceContext())
            {
                db.Accounts.AddRange(newAccounts);
                db.SaveChanges();
            }
        }
        private List<Account> CreateRandomAccounts(string name, long idTelegram, string loginTelegram, int Count)
        {
            List<Account> accounts = new List<Account>();
            for (int i = 0; i < Count; i++)
            {
                Account newAccont = new Account($"{name}{i}", idTelegram + i, (short)(i % 4), loginTelegram + i, new Random().Next(0, 10) > 5, new Random().Next(0, 10) > 5);
                accounts.Add(newAccont);
            }
            return accounts;
        }

        public void Dispose()
        {
            _accountManager.ClearTableAccount();
        }
    }
}
