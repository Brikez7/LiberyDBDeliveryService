using LiberyDBDeliveryService.Models.DB.Context;
using LiberyDBDeliveryService.Models.DB.IManagersTables;
using LiberyDBDeliveryService.Models.DB.Table;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace ConsoleDatabase.ManagerDB
{
    public class DBWorkShiftManager : IDBWorkShiftManager
    {
        public DBWorkShiftManager()
        {
        }

        public void AddShiftWork(WorkShift workShift)
        {
            using (DeliveryServiceContext db = new DeliveryServiceContext())
            {
                var commandText = "INSERT INTO WorkShift(idTelegramEmploye,date,status) VALUES (@PidTelegramEmploye,@Pdate,@Pstatus)";

                var parameterIdtelegram = new SqlParameter("@PidTelegramEmploye", workShift.IdTelegramEmploye);
                var parameterDate = new SqlParameter("@Pdate", workShift.Date);
                var parameterStatus = new SqlParameter("@Pstatus", workShift.Status);

                db.Database.ExecuteSqlRaw(commandText, parameterIdtelegram, parameterDate, parameterStatus);

                db.SaveChanges();
            }
        }
        public List<WorkShift> GetWorkShifts() 
        {
            List<WorkShift> workShifts = new List<WorkShift>();
            using (DeliveryServiceContext db = new DeliveryServiceContext())
            {
                workShifts = db.WorkShifts.AsNoTracking()
                                          .AsQueryable()
                                          .ToList();
            }
            return workShifts;
        }
        public List<WorkShift> GetShifWorkByDate(DateTime changeDate) 
        {
            List<WorkShift> workShiftsForChangeDate = new List<WorkShift>();   
            using (DeliveryServiceContext db = new DeliveryServiceContext()) 
            {
                workShiftsForChangeDate = db.WorkShifts.AsNoTracking()
                                                       .AsQueryable()
                                                       .Where(x => x.Date == changeDate)
                                                       .ToList();
            }
            return workShiftsForChangeDate;
        }
        public void ClearTableWorkShift() 
        {
            using (DeliveryServiceContext db = new DeliveryServiceContext())
            {
                if (db.WorkShifts.Any())
                {
                    db.Database.ExecuteSqlRaw($"DELETE FROM workShift");
                }
            }
        }
    }
}