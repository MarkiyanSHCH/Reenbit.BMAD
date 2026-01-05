using System.Data;

namespace Reenbit.BMAD.Sql.Common
{
    public interface IDbConnectionHandler
    {
        public IDbConnection CreateConnection();
    }
}
