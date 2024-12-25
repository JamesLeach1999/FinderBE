using MySqlConnector;
namespace FinderBE.Helpers;

public interface ICustomOrm<T>
{
    Task<List<T>> MapSqlValues(MySqlDataReader reader, Func<MySqlDataReader, T> mapper);
}
