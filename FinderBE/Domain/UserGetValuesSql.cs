using FinderBE.Helpers;
using FinderBE.Models;
using Microsoft.Data.SqlClient;
using System.Data;
namespace FinderBE.Domain;

public class UserGetValuesSql(IConfiguration configuration, ICustomOrm<User> customOrm) : EstablishSqlConnection<User>(configuration), IGetValues<User>
{
    public async Task<List<User>> GetValues()
    {
        try
        {
            using var sqlReader = await ExecuteSqlQuery("SELECT * FROM users");

            var users = await customOrm.MapSqlValues(sqlReader, mapper => new User
            {
                UserId = new Guid(sqlReader.GetString(0)),
                Username = sqlReader.GetString(1),
                Password = sqlReader.GetString(2),
                Email = sqlReader.GetString(3),
            });

            return users;
        }
        catch (Exception ex)
        {
            throw new Exception("error", ex);
        }

    }

    public async Task<User> GetValue(Guid id)
    {
        try
        {
            var query = "SELECT * FROM users.users WHERE userId = @userId";

            var sqlParams = new Dictionary<string, object>
            {
                {"@userId", id }
            };

            using var sqlReader = await ExecuteSqlQuery(query, sqlParams);

            var parameters = new Dictionary<string, object>
            {
                { "@userId", id}
            };

            var user = await customOrm.MapSqlValues(sqlReader, mapper => new User
            {
                UserId = new Guid(sqlReader.GetString(0)),
                Username = sqlReader.GetString(1),
                Password = sqlReader.GetString(2),
                Email = sqlReader.GetString(3),
            });

            return user == null ? throw new Exception("No user found") : user.FirstOrDefault();
        }
        catch (Exception ex)
        {
            throw new Exception("Error getting single user", ex);
        }
    }
}
