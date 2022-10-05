using System;
using System.Collections.Generic;
using System.Drawing;

namespace LiberyDBDeliveryService.Models.DB.Table
{
    public partial class Account
    {
        public string Name { get; set; } = null!;
        public long IdTelegram { get; set; }
        public short Post { get; set; }
        public string LoginTelegram { get; set; } = null!;
        public bool Life { get; set; }
        public bool WorkNow { get; set; }

        public Account(string name, long idTelegram, short post)
        {
            Name = name;
            IdTelegram = idTelegram;
            Post = post;
        }

        public Account(string name, long idTelegram, short post, string loginTelegram, bool life, bool work) : this(name, idTelegram, post)
        {
            LoginTelegram = loginTelegram;
            Life = life;
            WorkNow = work;
        }

        public override bool Equals(object obj)
        {
            if ((obj == null) || !GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                Account p = (Account)obj;
                return (Post == p.Post) &&
                       (IdTelegram == p.IdTelegram) &&
                       (Name == p.Name) &&
                       (LoginTelegram == p.LoginTelegram) &&
                       (Life == p.Life) &&
                       (WorkNow == p.WorkNow);
            }
        }
        public virtual ICollection<Order> OrderIdTelegramDeliverNavigations { get; set; } 
        public virtual ICollection<Order> OrderIdTelegramShopNavigations { get; set; }
    }
}
