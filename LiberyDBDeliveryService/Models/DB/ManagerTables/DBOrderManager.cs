using LiberyDBDeliveryService.Models.DB.Context;
using LiberyDBDeliveryService.Models.DB.IManagersTables;
using LiberyDBDeliveryService.Models.DB.Table;
using Microsoft.EntityFrameworkCore;

namespace ConsoleDatabase.ManagerDB
{
    public class DBOrderManager : IDBOrderManager
    {
        public DBOrderManager()
        {
        }

        public long Add(Order newOrder)
        {
            using (DeliveryServiceContext db = new DeliveryServiceContext())
            {
                db.Orders.Add(newOrder);
                db.SaveChanges();
                long idAddingOrder = Convert.ToInt64(db.Orders.AsNoTracking()
                                                              .AsQueryable()
                                                              .Select(x => new { x.IdOrder })
                                                              .MaxBy(x => x.IdOrder));
                return idAddingOrder;
            }
        }
        public void DeleteByIdOrder(long id)
        {
            using (DeliveryServiceContext db = new DeliveryServiceContext())
            {
                Order deleteOrder = new Order(id);
                db.Orders.Remove(deleteOrder);// 
                db.SaveChanges();
            }
        }
        public void UpdateOrderByIdOrder(Order changeOrder)
        {
            using (DeliveryServiceContext db = new DeliveryServiceContext())
            {
                var orderInDB = db.Orders.AsQueryable()
                                         .Where(x => x.IdOrder == changeOrder.IdOrder)
                                         .First();
                orderInDB = changeOrder;
                db.SaveChanges();
            }
        }
        public void UpdateStatusByIdOrder(long idOrder, short status)
        {
            using (DeliveryServiceContext db = new DeliveryServiceContext())
            {
                db.Orders.AsQueryable()
                         .Where(x => x.IdOrder == idOrder)
                         .First()
                         .StatusOrder = status;
                db.SaveChanges();
            }
        }
        public void AddIdTelegramDeliverByIdOrder(long idOrder, long IdDeliver)
        {
            using (DeliveryServiceContext db = new DeliveryServiceContext())
            {
                db.Orders.AsQueryable()
                         .Where(x => x.IdOrder == idOrder)
                         .First()
                         .IdTelegramDeliver = IdDeliver;
                db.SaveChanges();
            }
        }
        public List<Order> GetListOrderByDeliverId(long idTelegramDeliver)
        {
            using (DeliveryServiceContext db = new DeliveryServiceContext())
            {
                List<Order> ordersChangeDeliver = db.Orders.AsNoTracking()
                                                           .AsQueryable()
                                                           .Where(x => x.IdTelegramDeliver == idTelegramDeliver)
                                                           .ToList();
                return ordersChangeDeliver;
            }
        }
        public List<Order> GetListOrderByDate(string changeDate)
        {
            using (DeliveryServiceContext db = new DeliveryServiceContext())
            {
                List<Order> ordersChangeByDate = db.Orders.AsNoTracking()
                                                          .AsQueryable()
                                                          .Where(x => x.DateOrder == changeDate)
                                                          .ToList();
                return ordersChangeByDate;
            }
        }
        public void ClearTableOrder()
        {
            using (DeliveryServiceContext db = new DeliveryServiceContext())
            {
                if (db.Orders.Any())
                {
                    db.Orders.RemoveRange(db.Orders);
                    db.SaveChanges();
                }
            }
        }
    }
}
