using MySqlConnector;
using System.Text;

namespace FinderBE.Domain;

public class EstablishSqlConnection<T> : ISqlDbConnection<T>
{
    private string _connectionString;
    private MySqlConnection _connection;
    private IConfiguration _configuration;
    public EstablishSqlConnection(IConfiguration configuration)
    {
        _configuration = configuration;
        var sb = new StringBuilder();
        sb.Append(_configuration.GetConnectionString("DefaultConnection"));
        var modelName = typeof(T).Name;
        sb.Append($"Database={modelName.ToLower()}s");
        _connectionString = sb.ToString();
    }

    public async Task<MySqlConnection> OpenConnection()
    {
        try
        {
            var connection = new MySqlConnection(_connectionString);

            await connection.OpenAsync();

            return connection;
        }
        catch (Exception ex)
        {
            throw new Exception("Error establishing database connection", ex);
        }
    }

    public async Task<MySqlDataReader> ExecuteSqlQuery(string sqlQuery)
    {
        try
        {
            var connection = await OpenConnection();

            var command = new MySqlCommand(sqlQuery, connection);
            var reader = await command.ExecuteReaderAsync();

            return reader;
        }
        catch (Exception ex)
        {
            throw new Exception("Error executing SQL query", ex);
        }
    }
}
