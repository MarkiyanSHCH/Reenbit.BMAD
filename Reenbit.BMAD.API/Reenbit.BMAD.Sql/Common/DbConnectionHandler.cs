using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace Reenbit.BMAD.Sql.Common
{
    public class DbConnectionHandler : IDbConnectionHandler
    {
        private readonly string _connectionString;

        public DbConnectionHandler(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("TestDb") ?? throw new InvalidOperationException("Connection string 'TestDb' is not configured.");
        }

        public DbConnectionHandler()
        {
            _connectionString = string.Empty; // Default to an empty string for the parameterless constructor.  
        }

        public IDbConnection CreateConnection() => new SqlConnection(_connectionString)
        {
            RetryLogicProvider = SqlConfigurableRetryFactory.CreateExponentialRetryProvider(new SqlRetryLogicOption()
            {
                NumberOfTries = 1,
                DeltaTime = TimeSpan.FromSeconds(1),
                MaxTimeInterval = TimeSpan.FromSeconds(20)
            })
        };
    }
}
