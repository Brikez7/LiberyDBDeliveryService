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
            return "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=D:\\Предметы\\С#\\КПиЯП\\Project\\LiberyDBDeliveryService\\Tests\\bin\\Debug\\net6.0\\DeliveryService.mdf;Integrated Security=True;Connect Timeout=30";
        }
    }
}
