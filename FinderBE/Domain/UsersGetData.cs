using FinderBE.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using System.Data;

namespace FinderBE.Domain;

public class UsersGetData(IConfiguration configuration, UsersSqlDbContext userContext) : IGetValues<User>
{
    public async Task<List<User>> GetValues()
    {
        try
        {
            var allUsers = await userContext.Users.ToListAsync();

            return allUsers;
        }
        catch (Exception ex) {
            throw new Exception("Error fetching all users", ex);
        }
    }

    public async Task<User> GetUser(Guid userId)
    {
        try
        {
            var query = "SELECT * FROM users.users WHERE userId = @userId";

            var userIdParam = new SqlParameter("@userId", SqlDbType.UniqueIdentifier) { Value = userId };
            var singleUser = await userContext.Users.FromSqlRaw(query, userIdParam).FirstOrDefaultAsync();

            return singleUser;
        }
        catch (Exception ex) {
            throw new Exception("Error getting single user", ex);
        }
    }
}
