using LiberyDBDeliveryService.Models.DB.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiberyDBDeliveryService.Models.DB.IManagersTables
{
    public interface IDBOrderManager
    {
        public long Add(Order newOrder);
        public void DeleteByIdOrder(long id);
        public void UpdateOrderByIdOrder(Order changeOrder);
        public void UpdateStatusByIdOrder(long idOrder, short status);
        public void AddIdTelegramDeliverByIdOrder(long idOrder, long IdDeliver);
        public List<Order> GetListOrderByDeliverId(long idTelegramDeliver);
        public List<Order> GetListOrderByDate(string changeDate);
        public void ClearTableOrder();
    }
}
