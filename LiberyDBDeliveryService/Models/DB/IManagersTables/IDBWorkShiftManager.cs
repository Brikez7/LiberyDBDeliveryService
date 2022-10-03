using LiberyDBDeliveryService.Models.DB.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiberyDBDeliveryService.Models.DB.IManagersTables
{
    public interface IDBWorkShiftManager
    {
        public void AddShiftWork(WorkShift workShift);
        public List<WorkShift> GetShifWorkByDate(DateTime changeDate);
        public void ClearTableWorkShift();
        public List<WorkShift> GetWorkShifts();
    }
}
