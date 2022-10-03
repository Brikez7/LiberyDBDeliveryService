namespace LiberyDBDeliveryService.Models.DB
{
    public abstract class ConnectionBase
    {
        protected string _connectionString { get; set; }
        public ConnectionBase() 
        {
            _connectionString = "";
        }
        protected abstract string CreatePathConnectionDatabase();
        public abstract string GetPathConnection();
    }
}