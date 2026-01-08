using Microsoft.Data.SqlClient;
using System.Data;

namespace PAY.CROSS.DATAACCESS
{
    public interface IDataAccess
    {
        Task<Tuple<bool, string>> Check(string name);
        Task<string> ExecuteSP_string(string storeProcedure, List<SqlParameter>? parameters = default);
        Task<DataTable> ExecuteSP_DataTable(string storeProcedure, List<SqlParameter>? parameters = default);
        Task<bool> ExecuteSP_bool(string storeProcedure, List<SqlParameter>? parameters = default);
    }
}
