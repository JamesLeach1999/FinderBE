using FluentValidation;

namespace FinderBE.Validation;

public class UserRequestValidator : AbstractValidator<Guid>
{
    public UserRequestValidator()
    {
        RuleFor(userId => userId.ToString()).Matches("/^[0-9a-f]{8}-[0-9a-f]{4}-[0-5][0-9a-f]{3}-[089ab][0-9a-f]{3}-[0-9a-f]{12}$/i").NotEmpty();
    }
}
