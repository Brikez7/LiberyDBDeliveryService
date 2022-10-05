using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiberyDBDeliveryService.Models.DB;

namespace TestsDeliveryServiceLibery.TestOptions.Connection
{
    internal class ConnectionTestDB : ConnectionBase
    {
        public ConnectionTestDB()
        {
            _connectionString = CreatePathConnectionDatabase();
        }
        public override string GetPathConnection() => _connectionString;

        public void UpdateConnection(string TestConnection)
        {
            _connectionString = TestConnection;
        }

        protected override string CreatePathConnectionDatabase()
        {
            string startPathConnection = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=";
            string endPathConnection = "\\DeliveryService.mdf;Integrated Security=True;Initial Catalog=EfGetStarted1;Connect Timeout=30";
            string pathToTest = Directory.GetCurrentDirectory();
            return startPathConnection + pathToTest + endPathConnection;
        }
    }
}
