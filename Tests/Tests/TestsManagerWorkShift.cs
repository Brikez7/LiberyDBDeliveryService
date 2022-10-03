using ConsoleDatabase.ManagerDB;
using LiberyDBDeliveryService.Models.DB.Context;
using LiberyDBDeliveryService.Models.DB.IManagersTables;
using LiberyDBDeliveryService.Models.DB.Table;
using Microsoft.EntityFrameworkCore;
using TestsDeliveryServiceLibery.TestOptions.Connection;
using TestsDeliveryServiceLibery.TestOptions.OptionForTests;

namespace TestsDeliveryServiceLibery.Tests
{
    [Collection("Option connection")]
    public class TestsManagerWorkShift : IClassFixture<OptionsTestAccaounts>, IDisposable
    {
        private readonly IDBWorkShiftManager _workShiftManager;
        private readonly OptionConnectionDatabase _optionTestConnectionDB;
        private readonly OptionsTestAccaounts _optionTestAccaounts;
        private static Random _rnd = new Random();
        public TestsManagerWorkShift(OptionConnectionDatabase OptionConnectionDatabase,OptionsTestAccaounts optionTestWorkShift)
        {
            _optionTestConnectionDB = OptionConnectionDatabase;
            _optionTestAccaounts = optionTestWorkShift;

            _workShiftManager = new DBWorkShiftManager();

            _workShiftManager.ClearTableWorkShift();
        }

        [Fact]
        public void TestAddShiftByIdtelegram()
        {
            List<WorkShift> workShifts = CreateRandomShiftWork(_optionTestAccaounts._accounts, 10);

            AddShiftWorks(workShifts);

            CheckAdd(workShifts);
        }
        private List<WorkShift> CreateRandomShiftWork(List<Account> accounts, int count)
        {
            List<WorkShift> newWorkShifts = new List<WorkShift>();
            for (int i = 0; 0 < count; i++, --count)
            {
                if (i >= accounts.Count - 1)
                    i = 0;

                WorkShift workShift = new WorkShift(accounts[i].IdTelegram, RandomDay().Date, new Random().Next(0, 10) > 5);
                newWorkShifts.Add(workShift);
            }
            return newWorkShifts;
        }
        private static void AddShiftWorks(List<WorkShift> workShifts) 
        {
            using (DeliveryServiceContext db = new DeliveryServiceContext()) 
            {
                db.AddRange(workShifts);
                db.SaveChanges();
            }
        }
        private static void CheckAdd(List<WorkShift> workShifts)
        {
            for (int i = 0; i < workShifts.Count; i++)
            {
                int randomIndex = new Random().Next(workShifts.Count - 1);
                WorkShift randomWorkShift = workShifts[randomIndex];

                WorkShift? workshoft;
                using (DeliveryServiceContext db = new DeliveryServiceContext())
                {
                    workshoft = db.WorkShifts.AsNoTracking()
                                             .AsQueryable()
                                             .Where(x => x.Date.Date == randomWorkShift.Date.Date &&
                                                    x.Status == randomWorkShift.Status &&
                                                    x.IdTelegramEmploye == randomWorkShift.IdTelegramEmploye)
                                             .ToList()
                                             .FirstOrDefault();
                }
                Assert.Equal(randomWorkShift, workshoft);
            }
        }

        [Fact]
        public void TestGetWorkShifts() 
        {
            List<WorkShift> workShifts = CreateRandomShiftWork(_optionTestAccaounts._accounts, 10);

            AddShiftWorks(workShifts);

            CheckGetWorkShifts(workShifts);
        }
        private void CheckGetWorkShifts(List<WorkShift> workShifts) 
        {
            List<WorkShift> workShiftsDB = _workShiftManager.GetWorkShifts();

            Assert.Equal(workShifts, workShiftsDB);
        }

        [Fact]
        public void TestGetShifWorkByDate() 
        {
            List<WorkShift> workShifts = CreateRandomShiftWork(_optionTestAccaounts._accounts, 10);

            AddShiftWorks(workShifts);

            WorkShift randomWorkShift = RandomWorkShift(workShifts);

            CheckGetShifWorkByDate(workShifts, randomWorkShift.Date.Date);
        }
        private WorkShift RandomWorkShift(List<WorkShift> workShifts)
            => workShifts[new Random().Next(0, workShifts.Count - 1)];
        DateTime RandomDay()
        {
            DateTime start = new DateTime(1995, 1, 1);
            int range = (DateTime.Today - start).Days;
            return start.AddDays(_rnd.Next(range));
        }
        private void CheckGetShifWorkByDate(List<WorkShift> workShifts, DateTime dateTime)
        {
            List<WorkShift> workShiftsDB = _workShiftManager.GetShifWorkByDate(dateTime);
            workShifts = workShifts.Where(x => x.Date.Date == dateTime).ToList();

            Assert.Equal(workShifts,workShiftsDB);
        }

        public void Dispose()
        {
            _workShiftManager.ClearTableWorkShift();
        }
    }
}
