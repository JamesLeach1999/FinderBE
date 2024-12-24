using FinderBE.Models;
using MySqlConnector;
using System.Text;

namespace FinderBE.Domain;

public class UserGetValuesSql(IConfiguration configuration) : EstablishSqlConnection<User>(configuration), IGetValues<User>
{
    public async Task<List<User>> GetValues()
    {
        try
        {
            using var sqlReturnValue = await ExecuteSqlQuery("SELECT * FROM users");

            var users = new List<User>();

            while (await sqlReturnValue.ReadAsync())
            {
                var userId = new Guid(sqlReturnValue.GetString(0));
                var username = sqlReturnValue.GetString(1);
                var password = sqlReturnValue.GetString(2);
                var email = sqlReturnValue.GetString(3);
                users.Add(new User
                {
                    UserId = userId,
                    Username = username,
                    Password = password,
                    Email = email
                });
            }

            return users;
        }
        catch (Exception ex) {
            throw new Exception("error", ex);
        }
        
    }
}
