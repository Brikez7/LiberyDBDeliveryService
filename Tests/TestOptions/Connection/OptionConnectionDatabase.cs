using LiberyDBDeliveryService.Models.DB.Context;

namespace TestsDeliveryServiceLibery.TestOptions.Connection
{
    public class OptionConnectionDatabase : IDisposable
    {
        public OptionConnectionDatabase()
        {
            ConnectionToTestDatabase();
        }

        public void Dispose()
        {

        }

        private void ConnectionToTestDatabase()
        {
            ConnectionTestDB connectionTest = new ConnectionTestDB();
            DeliveryServiceContext.UpdateConnetion(connectionTest);
        }
    }
}