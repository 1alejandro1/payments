using Microsoft.Data.SqlClient;
using System.Data;

namespace PAY.CROSS.DATAACCESS
{
    public interface IDataAccess
    {
        Task<Tuple<bool, string>> Check(string name);
        DataTable SelectStoredProcedure(string name, string query, List<SqlParameter> parameter);
        bool ExecuteStoredProcedure(string name, string query, List<SqlParameter> parameter);
        DataTable Select(string name, string query);
    }
}
