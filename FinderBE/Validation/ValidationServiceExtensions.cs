using FinderBE.Domain;
using FinderBE.Models;
using FluentValidation;

namespace FinderBE.Validation;

public static class ValidationServiceExtensions
{
    public static IServiceCollection AddValidation(this IServiceCollection services)
    {
        services.AddTransient<AbstractValidator<Guid>, UserRequestValidator>();

        return services;
    }
}
