using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LiberyDBDeliveryService.Models.DB.Table
{
    public partial class WorkShift
    {
        public long? IdWorkShift { get; set; } 
        public long IdTelegramEmploye { get; set; }
        public DateTime Date { get; set; }
        public bool Status { get; set; }

        public WorkShift(long idTelegramEmploye, DateTime date, bool status)
        {
            IdTelegramEmploye = idTelegramEmploye;
            Date = date;
            Status = status;
        }

        public WorkShift(long? idWorkShift, long idTelegramEmploye, DateTime date, bool status)
        {
            IdWorkShift = idWorkShift;
            IdTelegramEmploye = idTelegramEmploye;
            Date = date;
            Status = status;
        }

        public WorkShift(long idTelegramEmploye, DateTime date, bool status, Account idTelegramEmployeNavigation) : this(idTelegramEmploye, date, status)
        {
            IdTelegramEmployeNavigation = idTelegramEmployeNavigation;
        }
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            WorkShift workShift = (WorkShift)obj;
            bool equal = Date.Date == workShift.Date.Date &&
                       workShift.Status == Status &&
                       IdTelegramEmploye == workShift.IdTelegramEmploye;
            return equal;
        }
        public virtual Account IdTelegramEmployeNavigation { get; set; } = null!;
    }
}
