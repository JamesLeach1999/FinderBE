using MySqlConnector;

namespace FinderBE.Domain;

public interface ISqlDbConnection<ModelType>
{
    public Task<MySqlConnection> OpenConnection();
    public Task<MySqlDataReader> ExecuteSqlQuery(string sqlQuery);
}
