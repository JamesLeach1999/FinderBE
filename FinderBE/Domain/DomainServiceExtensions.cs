using FinderBE.Models;

namespace FinderBE.Domain;

public static class DomainServiceExtensions
{
    public static IServiceCollection AddDomain(this IServiceCollection services)
    {
        services.AddTransient<ISqlDbConnection<User>, EstablishSqlConnection<User>>();
        services.AddTransient<IGetValues<User>, UserGetValuesSql>();

        return services;
    }
}
