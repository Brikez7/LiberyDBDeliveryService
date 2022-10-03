using LiberyDBDeliveryService.Models.DB.Context;
using LiberyDBDeliveryService.Models.DB.IManagersTables;
using LiberyDBDeliveryService.Models.DB.Table;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace ConsoleDatabase.ManagerDB
{
    public class DBAccountManager : IDBAccountManager
    {
        public DBAccountManager()
        {
        }

        public void Add(Account newAccount)
        {
            using (DeliveryServiceContext db = new DeliveryServiceContext())
            {
                db.Accounts.Add(newAccount);
                db.SaveChanges();
            }
        }
        public void UpdateLifeByIdAccaountAndPost(long idAccount, short post, bool life)
        {
            using (DeliveryServiceContext db = new DeliveryServiceContext())
            {
                Account? account = db.Accounts.AsQueryable()
                           .Where(x => x.IdTelegram == idAccount && x.Post == post)
                           .FirstOrDefault();
                if (account != null)
                {
                    account.Life = life;
                    db.SaveChanges();
                }    
            }
        }

        public List<Account> GetAccounts() 
        {
            List<Account> accounts = new List<Account>();  
            using (DeliveryServiceContext db = new DeliveryServiceContext()) 
            {
                accounts = db.Accounts.AsNoTracking().ToList();
            }
            return accounts;
        }
        public Account GetAccountByIdTelegramAndPost(long idtelegram, short post)
        {
            Account? searchAccount;
            using (DeliveryServiceContext db = new DeliveryServiceContext())
            {
                searchAccount = db.Accounts.AsNoTracking()
                                           .AsQueryable()
                                           .Where(x => x.IdTelegram == idtelegram && x.Post == post)
                                           .FirstOrDefault();
            }
            if (searchAccount == null)
                searchAccount = new Account("",idtelegram,post);
                            
            return searchAccount;
        }
        public bool CheckAccountExistByIdTelegram(long idTelegram) 
        {
            bool existAccount;
            using (DeliveryServiceContext db = new DeliveryServiceContext())
            {
                existAccount = db.Accounts.AsNoTracking()
                                          .AsQueryable()
                                          .Where(x => x.IdTelegram == idTelegram)
                                          .Any();
            }
            return existAccount;
        }
        public List<Account> GetLivesAccountByPost(int post,bool life = true) 
        {
            List<Account> accounts = new List<Account>();
            using (DeliveryServiceContext db = new DeliveryServiceContext())
            {
                accounts = db.Accounts.AsNoTracking()
                                      .AsQueryable()
                                      .Where(x => x.Post == post && x.Life == life)
                                      .ToList();
            }
            return accounts;
        }
        public bool CheckLifeAccountByIdTelegramAndPost(long idTelegram, int post)
        {
            using (DeliveryServiceContext db = new DeliveryServiceContext())
            {
                var ChangeAccount = db.Accounts.AsNoTracking()
                                               .AsQueryable()
                                               .Select(x => new { x.IdTelegram, x.Post, x.Life })
                                               .Where(x => x.IdTelegram == idTelegram && x.Post == post)
                                               .FirstOrDefault();
                if (ChangeAccount != null)
                {
                    return ChangeAccount.Life;
                }
                return false;
            }
        }
        public void ClearTableAccount()
        {
            using (DeliveryServiceContext db = new DeliveryServiceContext())
            {
                if (db.Accounts.Any())
                {
                    db.Accounts.RemoveRange(db.Accounts);
                    db.SaveChanges();
                }
            }
        }
        public bool CheckExistAccountByIdTelegramAndPost(short post, long idTelegram) 
        {
            using (DeliveryServiceContext db = new DeliveryServiceContext())
            {
                var ChangeAccount = db.Accounts.AsNoTracking()
                                               .AsQueryable()
                                               .Select(x => new { x.IdTelegram, x.Post})
                                               .Where(x => x.IdTelegram == idTelegram && x.Post == post)
                                               .FirstOrDefault();
                return ChangeAccount != null;
            }
        }
    }
}
