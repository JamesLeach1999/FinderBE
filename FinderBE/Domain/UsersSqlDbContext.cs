using Microsoft.EntityFrameworkCore;

using FinderBE.Models;
namespace FinderBE.Domain;


public class UsersSqlDbContext : DbContext
{
    public DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseMySQL("server=localhost;database=users;user=root;password=jadlljames");
    }

}
