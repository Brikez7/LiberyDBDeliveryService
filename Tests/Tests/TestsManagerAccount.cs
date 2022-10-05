using ConsoleDatabase.ManagerDB;
using LiberyDBDeliveryService.Models.DB.Context;
using LiberyDBDeliveryService.Models.DB.IManagersTables;
using LiberyDBDeliveryService.Models.DB.Table;
using Microsoft.EntityFrameworkCore;
using TestsDeliveryServiceLibery.TestOptions.Connection;

namespace TestsDeliveryServiceLibery.Tests
{

    [Collection("Option connection")]
    public class TestsManagerAccount :  IDisposable
    {
        private readonly IDBAccountManager _accountManager;
        public OptionConnectionDatabase _optionConnectionDatabase;
        public TestsManagerAccount(OptionConnectionDatabase optionConnectionDatabase)
        {
            _optionConnectionDatabase = optionConnectionDatabase;
            _accountManager = new DBAccountManager();
        }

        [Theory]
        [InlineData("Dima", 1, 1, "Brykez7", true,true)]
        [InlineData("Egor", 11, 2, "Joycasino", true, true)]
        [InlineData("Vlad", 21, 3, "DotNeter", true, true)]
        public void TestsAdd(string name, long idTelegram, short post, string loginTelegram, bool life, bool work)
        {
            List<Account> newAccounts = CreateAccounts(name, idTelegram, post, loginTelegram, life, work);

            AddAccountsFromDatabase(newAccounts);

            CheckAddAccounts(newAccounts);
        }
        private List<Account> CreateAccounts(string name, long idTelegram, short post, string loginTelegram, bool life,bool work, int Count = 5)
        {
            List<Account> accounts = new List<Account>();
            for (int i = 0; i < Count; i++)
            {
                Account newAccont = new Account($"{name}{i}", idTelegram + i, (short)(i % 4), loginTelegram + i, life, work);
                accounts.Add(newAccont);
            }
            return accounts;
        }
        private void AddAccountsFromDatabase(List<Account> newAccounts)
        {
            for (int i = 0; i < newAccounts.Count; i++)
            {
                _accountManager.Add(newAccounts[i]);
            }
        }
        private void CheckAddAccounts(List<Account> addAccounts)
        {
            List<Account> accounts = new List<Account>();
            using (DeliveryServiceContext db = new DeliveryServiceContext())
            {
                accounts = db.Accounts.AsNoTracking().AsQueryable().OrderBy(x => x.IdTelegram).ToList();
            }
            for (int i = 0; i < addAccounts.Count; i++)
            {
                Assert.Equal(accounts[i], addAccounts[i]);
            }
        }
        [Theory]
        [InlineData("Dima", 1, 1, "Brykez7", true, true)]
        [InlineData("Egor", 11, 2, "Joycasino", true, true)]
        [InlineData("Vlad", 21, 3, "DotNeter", true, true)]
        public void TestUpdateLifeByIdAccaount(string name, long idTelegram, short post, string loginTelegram, bool life, bool work)
        {
            List<Account> newAccounts = CreateAccounts(name, idTelegram, post, loginTelegram, life, work);
            AddAccounts(newAccounts);

            Account randomAccount = RandomAccount(newAccounts);

            bool randomLife = new Random().Next(0, 10) > 5;
            _accountManager.UpdateLifeByIdAccaountAndPost(randomAccount.IdTelegram, randomAccount.Post, randomLife);
            randomAccount.Life = randomLife;

            CheckLifeAccount(randomAccount);
        }
        private Account RandomAccount(List<Account> newAccounts)
            => newAccounts[new Random().Next(0, newAccounts.Count - 1)];
        private void CheckLifeAccount(Account account)
        {
            bool lifeAccount = false;
            using (DeliveryServiceContext db = new DeliveryServiceContext())
            {
                lifeAccount = db.Accounts.AsNoTracking().AsQueryable().Select(x => new { x.IdTelegram, x.Post, x.Life }).First(x => x.IdTelegram == account.IdTelegram && x.Post == account.Post).Life;
            }
            Assert.Equal(lifeAccount, account.Life);
        }
        private static void AddAccounts(List<Account> newAccounts)
        {
            using (DeliveryServiceContext db = new DeliveryServiceContext())
            {
                db.Accounts.AddRange(newAccounts);
                db.SaveChanges();
            }
        }

        [Theory]
        [InlineData("Dima", 1, 1, "Brykez7", true,true)]
        [InlineData("Egor", 11, 2, "Joycasino", true, true)]
        [InlineData("Vlad", 21, 3, "DotNeter", true, true)]
        public void TestGetAccounts(string name, long idTelegram, short post, string loginTelegram, bool life,bool work)
        {
            List<Account> newAccounts = CreateAccounts(name, idTelegram, post, loginTelegram, life, work);

            AddAccounts(newAccounts);

            List<Account> accounts = _accountManager.GetAccounts();

            Assert.Equal(newAccounts, accounts);
        }

        [Theory]
        [InlineData("Dima", 1, 1, "Brykez7", true,true, 10)]
        [InlineData("Egor", 11, 2, "Joycasino", true, true, 30)]
        [InlineData("Vlad", 21, 3, "DotNeter", true, true, 20)]
        public void TestGetAccountByIdTelegramAndPost(string name, long idTelegram, short post, string loginTelegram, bool life,bool work, int count)
        {
            List<Account> newAccounts = CreateAccounts(name, idTelegram, post, loginTelegram, life, work, count);
            AddAccounts(newAccounts);

            Account changeAccount = RandomAccount(count, newAccounts);
            Account accountDB = _accountManager.GetAccountByIdTelegramAndPost(changeAccount.IdTelegram, changeAccount.Post);
            Assert.Equal(changeAccount, accountDB);
        }

        [Theory]
        [InlineData("Dima", 1, 1, "Brykez7", true, true, 10)]
        [InlineData("Egor", 11, 2, "Joycasino", true, true, 30)]
        [InlineData("Vlad", 21, 3, "DotNeter", true, true, 20)]
        public void TestCheckAccountExistByIdTelegram(string name, long idTelegram, short post, string loginTelegram, bool life,bool work, int count)
        {
            List<Account> newAccounts = CreateAccounts(name, idTelegram, post, loginTelegram, life,work ,count);
            AddAccounts(newAccounts);

            Account changeAccount = RandomAccount(count, newAccounts);

            Assert.True(_accountManager.CheckAccountExistByIdTelegram(changeAccount.IdTelegram));

            long maxId = newAccounts.Max(x => x.IdTelegram);
            Assert.False(_accountManager.CheckAccountExistByIdTelegram(++maxId));
        }

        private static Account RandomAccount(int count, List<Account> newAccounts)
        {
            int randomIndex = new Random().Next(0, count - 1);
            Account changeAccount = newAccounts[randomIndex];
            return changeAccount;
        }

        [Theory]
        [InlineData("Dima", 1, 1, "Brykez7", true, 30)]
        [InlineData("Egor", 11, 2, "Joycasino", false, 30)]
        [InlineData("Vlad", 21, 3, "DotNeter", true, 20)]
        public void TestGetLivesAccountByPost(string name, long idTelegram, short post, string loginTelegram, bool life, int count)
        {
            List<Account> newAccounts = CreateRandomAccounts(name, idTelegram, loginTelegram, count);
            AddAccounts(newAccounts);

            int randomIndex = new Random().Next(0, count - 1);
            Account randomAccount = newAccounts[randomIndex];

            List<Account> accounts1 = _accountManager.GetLivesAccountByPost(randomAccount.Post, randomAccount.Life);
            List<Account> accounts2 = newAccounts.Where(x => x.Post == randomAccount.Post && randomAccount.Life == x.Life).ToList();
            
            Assert.Equal(accounts2, accounts1);
        }
        private List<Account> CreateRandomAccounts(string name, long idTelegram,string loginTelegram, int Count)
        {
            List<Account> accounts = new List<Account>();
            for (int i = 0; i < Count; i++)
            {
                Account newAccont = new Account($"{name}{i}", idTelegram + i, (short)(i % 4), loginTelegram + i, RandomBool(), RandomBool());
                accounts.Add(newAccont);
            }
            return accounts;
        }

        private static bool RandomBool()
        {
            return new Random().Next(0, 10) > 5;
        }

        [Theory]
        [InlineData("Dima", 1, "Brykez7",  30)]
        [InlineData("Egor", 11, "Joycasino", 30)]
        [InlineData("Vlad", 21, "DotNeter",  20)]
        public void TestCheckLifeAccountByIdTelegramAndPost(string name, long idTelegram, string loginTelegram, int count)
        {
            List<Account> newAccounts = CreateRandomAccounts(name, idTelegram, loginTelegram, count);
            AddAccounts(newAccounts);

            Account randomAccount = RandomAccount(count, newAccounts);

            bool randomlifeAccountDB = _accountManager.CheckLifeAccountByIdTelegramAndPost(randomAccount.IdTelegram,randomAccount.Post);
            bool lifeAccountList= newAccounts.Where(x => x.IdTelegram == randomAccount.IdTelegram && x.Post == randomAccount.Post).First().Life;

            Assert.Equal(randomlifeAccountDB, lifeAccountList);
        }

        [Theory]
        [InlineData("Dima", 1, "Brykez7", 30)]
        [InlineData("Egor", 11, "Joycasino", 30)]
        [InlineData("Vlad", 21, "DotNeter", 20)]
        public void TestClearTableAccount(string name, long idTelegram, string loginTelegram, int count)
        {
            List<Account> newAccounts = CreateRandomAccounts(name, idTelegram, loginTelegram, count);
            AddAccounts(newAccounts);

            _accountManager.ClearTableAccount();

            List<Account> accounts = new List<Account>();
            using (DeliveryServiceContext db = new DeliveryServiceContext()) 
            {
                accounts = db.Accounts.AsNoTracking().AsQueryable().ToList();
            }
            Assert.Empty(accounts);
        }

        [Theory]
        [InlineData("Dima", 1, "Brykez7", 30)]
        [InlineData("Egor", 11, "Joycasino", 30)]
        [InlineData("Vlad", 21, "DotNeter", 20)]
        public void TestCheckExistAccountByIdTelegramAndPost(string name, long idTelegram, string loginTelegram, int count)
        {
            List<Account> newAccounts = CreateRandomAccounts(name, idTelegram, loginTelegram, count);
            AddAccounts(newAccounts);

            Account randomAccount = RandomAccount(count,newAccounts);

            bool exist = _accountManager.CheckExistAccountByIdTelegramAndPost(randomAccount.Post, randomAccount.IdTelegram);
            bool notExist = _accountManager.CheckExistAccountByIdTelegramAndPost(4, randomAccount.IdTelegram);

            long maxId = newAccounts.Max(x => x.IdTelegram);
            short randomPost = (short)new Random().Next(0, 3);
            bool notExist1 = _accountManager.CheckExistAccountByIdTelegramAndPost(randomPost, maxId + 1);

            Assert.True(exist);
            Assert.False(notExist);
            Assert.False(notExist1);
        }

        public void Dispose()
        {
            _accountManager.ClearTableAccount();
        }
    }
}