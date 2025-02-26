using Bookstore.Server.Data.Models;
using FluentValidation;

namespace Bookstore.Server.Validations;

public class UserValidator : AbstractValidator<User>
{
    public UserValidator()
    {
        RuleFor(user => user.FirstName).NotNull().NotEmpty();
        RuleFor(user => user.LastName).NotNull().NotEmpty();
        RuleFor(user => user.Email).NotNull().NotEmpty().EmailAddress();

        RuleFor(user => user.Password)
            .NotNull()
            .NotEmpty()
            .MinimumLength(6)
            .Must(password => password.FirstOrDefault(ch => ch >= 'A' && ch <= 'Z') != 0)
                .WithMessage("Password must contain at least one uppercase letter")
            .Must(password => password.FirstOrDefault(ch => ch >= '0' && ch <= '9') != 0)
                .WithMessage("Password must contain at least one number");
    }
}