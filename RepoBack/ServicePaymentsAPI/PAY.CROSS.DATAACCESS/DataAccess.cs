using Microsoft.Data.SqlClient;
using System.Data;

namespace PAY.CROSS.DATAACCESS
{
    public class DataAccess : IDataAccess
    {
        private DBOptions dBOption;

        public DataAccess(DBOptions dBOption)
        {
            this.dBOption = dBOption;
        }

        private string connection(DBOptionItems database)
        {
            string connection = string.Empty;
            try
            {
                connection = "Persist Security Info=True;User ID=" + database.user + ";Pwd=" + database.password + ";Server=" + database.server + ";Database=" + database.dataBase + ";Application Name =" + dBOption.name;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return connection;
        }

        public bool ExecuteStoredProcedure(string name, string query, List<SqlParameter> parameter)
        {
            SqlConnection conexion = new SqlConnection(connection(this.dBOption.connections.FirstOrDefault(x => x.name == name)));
            try
            {
                SqlCommand comando = new SqlCommand(query, conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.CommandTimeout = 600000;
                foreach (var item in parameter)
                {
                    if (item.Value == null)
                        item.Value = DBNull.Value;
                    comando.Parameters.Add(item);
                }
                conexion.Open();
                comando.ExecuteNonQuery();
                conexion.Close();
                SqlConnection.ClearAllPools();
                return true;
            }
            catch (SqlException Error)
            {
                conexion.Close();
                SqlConnection.ClearAllPools();
                throw new Exception(Error.Message);
            }
        }

        public DataTable SelectStoredProcedure(string name, string query, List<SqlParameter> parameter)
        {
            DataTable Consulta = new DataTable();
            SqlConnection conexion = new SqlConnection(connection(this.dBOption.connections.FirstOrDefault(x => x.name == name)));
            try
            {
                SqlConnection.ClearAllPools();
                SqlDataAdapter comando = new SqlDataAdapter(query, conexion);
                comando.SelectCommand.CommandType = CommandType.StoredProcedure;
                comando.SelectCommand.CommandTimeout = 3600000;
                foreach (var item in parameter)
                {
                    if (item.Value == null)
                        item.Value = DBNull.Value;
                    comando.SelectCommand.Parameters.Add(item);
                }
                conexion.Open();
                comando.Fill(Consulta);
            }
            catch (SqlException Error)
            {
                throw new Exception(Error.Message);
            }
            finally
            {
                conexion.Close();
                SqlConnection.ClearAllPools();
            }
            return Consulta;
        }

        public DataTable Select(string name, string query)
        {
            DataTable Consulta = new DataTable();
            SqlConnection conexion = new SqlConnection(connection(this.dBOption.connections.FirstOrDefault(x => x.name == name)));
            try
            {
                SqlConnection.ClearAllPools();
                SqlDataAdapter comando = new SqlDataAdapter(query, conexion);
                comando.SelectCommand.CommandType = CommandType.Text;
                comando.SelectCommand.CommandTimeout = 3600000;
                conexion.Open();
                comando.Fill(Consulta);
            }
            catch (SqlException Error)
            {
                throw new Exception(Error.Message);
            }
            finally
            {
                conexion.Close();
                SqlConnection.ClearAllPools();
            }
            return Consulta;
        }
        private string Connection(DBOptionItems database)
        {
            string connection;
            try
            {
                connection = "Persist Security Info=True;User ID=" + database.user + ";Pwd=" + database.password + ";Server=" + database.server + ";Database=" + database.dataBase + ";Application Name =" + dBOption.name;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return connection;
        }
        public async Task<Tuple<bool, string>> Check(string name)
        {
            Tuple<bool, string> _conexion = null;
            DBOptionItems item = this.dBOption.connections.FirstOrDefault(x => x.name == name);
            using (var conexion = new SqlConnection(Connection(item)))
            {
                try
                {
                    try
                    {
                        await conexion.OpenAsync();
                        _conexion = new Tuple<bool, string>(true, $"BATABASE: {item.dataBase}; SERVER: {item.server}; USER: {item.user}");
                    }
                    catch (Exception ex)
                    {
                        _conexion = new Tuple<bool, string>(false, $"COULD NOT CONNECT TO DATABASE: {item.dataBase} SERVER: {item.server}; USER: {item.user}; EXCEPTION: {ex.Message.ToUpper()}");
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
