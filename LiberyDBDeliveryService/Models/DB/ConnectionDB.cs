using System.Configuration;
using System.Data.SqlClient;
using System.IO;

namespace LiberyDBDeliveryService.Models.DB
{
    public class PathConnection : ConnectionBase
    {
        public PathConnection()
        {
            _connectionString = CreatePathConnectionDatabase();
        }
        public override string GetPathConnection()
            => _connectionString;
        protected override string CreatePathConnectionDatabase()
        {
            var pathToCurrentProject = Directory.GetCurrentDirectory();
            var beginPathConnection = ConfigurationManager.AppSettings["BeginPathConnection"];
            var endPathConnection = ConfigurationManager.AppSettings["EndPathConnection"];
            return beginPathConnection + pathToCurrentProject + endPathConnection;
        }
    }
}
