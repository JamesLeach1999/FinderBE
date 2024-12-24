using Microsoft.EntityFrameworkCore;
using FinderBE.Models;
using MySql.EntityFrameworkCore;
namespace FinderBE.Domain;


public class UsersSqlDbContext : DbContext
{
    public DbSet<User> Users { get; set; }

    public UsersSqlDbContext(DbContextOptions<UsersSqlDbContext> options)
           : base(options)
    {
    }
    
}
