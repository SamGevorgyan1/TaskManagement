using FluentValidation;
using TaskManagement.Application.Users.Command;

namespace TaskManagement.Application.Users.Validators;

public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
{
    public RegisterCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MinimumLength(4).WithMessage("Name must be at least 4 characters long.")
            .MaximumLength(50).WithMessage("Name must not exceed 50 characters.");

        RuleFor(x => x.Surname)
            .NotEmpty().WithMessage("Surname is required.")
            .MinimumLength(4).WithMessage("Surname must be at least 4 characters long.")
            .MaximumLength(50).WithMessage("Surname must not exceed 50 characters.");

        RuleFor(x => x.Role)
            .IsInEnum().WithMessage("Role is invalid.");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("A valid email is required.");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required.")
            .MinimumLength(6).WithMessage("Password must be at least 6 characters long.");
    }
}