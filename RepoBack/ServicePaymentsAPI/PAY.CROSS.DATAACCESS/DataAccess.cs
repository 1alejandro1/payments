using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System.Data;
using static Azure.Core.HttpHeader;

namespace PAY.CROSS.DATAACCESS
{
    public class DataAccess : IDataAccess
    {
        private readonly DBOptionItems _connectionDb;

        public DataAccess(IOptions<DBOptions> dataBaseSettings)
        {
            _connectionDb = dataBaseSettings.Value.connections!
           .FirstOrDefault(x => x.name!.Equals("DB_SERVICE_PAYMENT"))
           ?? throw new ArgumentNullException("DB_SERVICE_PAYMENT", "No se encontro el alias para la base de datos");
        }
        public async Task<DataTable> ExecuteSP_DataTable(string storeProcedure, List<SqlParameter>? parameters = default)
        {
            try
            {
                DataTable dataTable = new();
                var cn = Connection();
                using SqlConnection sqlConnection = new(cn);
                using SqlCommand sqlCommand = new(storeProcedure, sqlConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.CommandTimeout = 600;

                if (parameters is not null)
                {
                    foreach (var item in parameters)
                    {
                        item.Value ??= DBNull.Value;
                        sqlCommand.Parameters.Add(item);
                    }
                }

                await sqlConnection.OpenAsync();
                using var reader = await sqlCommand.ExecuteReaderAsync();
                dataTable.Load(reader);

                return dataTable;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public async Task<string> ExecuteSP_string(string storeProcedure, List<SqlParameter>? parameters = default)
        {
            DataTable dataTable = new();
            string resultado = "";
            var cn = Connection();
            using SqlConnection sqlConnection = new(cn);
            using SqlCommand sqlCommand = new(storeProcedure, sqlConnection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.CommandTimeout = 600;
            
            if (parameters is not null)
            {
                foreach (var item in parameters)
                {
                    item.Value ??= DBNull.Value;
                    sqlCommand.Parameters.Add(item);
                }
            }

            await sqlConnection.OpenAsync();
            using var reader = await sqlCommand.ExecuteReaderAsync();
            dataTable.Load(reader);

            if (dataTable.Rows.Count > 0)
            {
                resultado = dataTable.Rows[0]["id_bi"].ToString()!;
            }

            return resultado;
        }
        public async Task<bool> ExecuteSP_bool(string storeProcedure, List<SqlParameter>? parameters = default)
        {
            using SqlConnection sqlConnection = new(Connection());
            using SqlCommand sqlCommand = new(storeProcedure, sqlConnection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.CommandTimeout = 600;

            if (parameters is not null)
            {
                foreach (var item in parameters)
                {
                    item.Value ??= DBNull.Value;
                    sqlCommand.Parameters.Add(item);
                }
            }

            await sqlConnection.OpenAsync();
            var success = Convert.ToBoolean(await sqlCommand.ExecuteScalarAsync());
            await sqlConnection.CloseAsync();

            return success;
        }
        private string Connection()
        {
            var connection = $"Persist Security Info=True;User ID={_connectionDb.user};Password={_connectionDb.password};Server={_connectionDb.server}; Database={_connectionDb.dataBase}; Encrypt=True;TrustServerCertificate=True;";

            return connection;
        }
        public async Task<Tuple<bool, string>> Check(string name)
        {
            Tuple<bool, string> _conexion = null;            
            using (var conexion = new SqlConnection(Connection()))
            {
                try
                {
                    try
                    {
                        await conexion.OpenAsync();
                        _conexion = new Tuple<bool, string>(true, $"BATABASE: {_connectionDb.dataBase}; SERVER: {_connectionDb.server}; USER: {_connectionDb.user}");
                    }
                    catch (Exception ex)
                    {
                        _conexion = new Tuple<bool, string>(false, $"COULD NOT CONNECT TO DATABASE: {_connectionDb.dataBase} SERVER: {_connectionDb.server}; USER: {_connectionDb.user}; EXCEPTION: {ex.Message.ToUpper()}");
                    }
                    finally
                    {
                        conexion.Close();
                        SqlConnection.ClearAllPools();
                    }
                }
                catch (Exception ex)
                {
                    _conexion = new Tuple<bool, string>(false, $"CONFIG PARMETER DATABASE: {name}; EXCEPTION: {ex.Message.ToUpper()}");
                }
                return _conexion;
            }
        }
    }
}
