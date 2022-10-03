using LiberyDBDeliveryService.Models.DB.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiberyDBDeliveryService.Models.DB.IManagersTables
{
    public interface IDBAccountManager
    {
        public void Add(Account account);
        public void UpdateLifeByIdAccaountAndPost(long idAccount, short post, bool life);
        public List<Account> GetAccounts();
        public Account GetAccountByIdTelegramAndPost(long idtelegram, short post);
        public List<Account> GetLivesAccountByPost(int post,bool life = true);
        public bool CheckLifeAccountByIdTelegramAndPost(long idTelegram, int post);
        public void ClearTableAccount();
        public bool CheckExistAccountByIdTelegramAndPost(short post, long idTelegram);
        public bool CheckAccountExistByIdTelegram(long idTelegram);
    }
}
