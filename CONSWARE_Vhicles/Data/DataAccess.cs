using System;
using System.Data;
using Dapper;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Common;

namespace CONSWARE_Vhicles.Data
{
    public class DataAccess
    {
        private readonly string _connectionString;
        private readonly string _schema;

        public DataAccess(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("MySqlConnection");
            _schema = configuration["DatabaseSettings:Schema"];
        }

        public void Execute(string sql)
        {
            try
            {
                using var connection = new MySqlConnection(_connectionString);
                connection.Open();

                connection.Execute(sql);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al conectar a la base de datos: {ex.Message}");
                throw;
            }
        }

        public IEnumerable<T> Query<T>(string sql)
        {
            try
            {
                using var connection = new MySqlConnection(_connectionString);
                connection.Open();

                return connection.Query<T>(sql);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al conectar a la base de datos: {ex.Message}");
                throw;
            }
        }

        public T QueryFirstOrDefault<T>(string sql)
        {
            try
            {
                using var connection = new MySqlConnection(_connectionString);
                connection.Open();

                return connection.QueryFirstOrDefault<T>(sql);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al conectar a la base de datos: {ex.Message}");
                throw;
            }
        }

        public int GetLastInsertedId()
        {
            try
            {
                using var connection = new MySqlConnection(_connectionString);
                connection.Open();

                return connection.QueryFirstOrDefault<int>("SELECT LAST_INSERT_ID()");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener el último ID insertado: {ex.Message}");
                throw;
            }
        }

        public IEnumerable<T> ExecuteStoredProcedure<T>(string storedProcedureName, object parameters = null)
        {
            try
            {
                using var connection = new MySqlConnection(_connectionString);
                connection.Open();

                return connection.Query<T>(storedProcedureName, parameters, commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al ejecutar el procedimiento almacenado: {ex.Message}");
                throw;
            }
        }
    }
}

