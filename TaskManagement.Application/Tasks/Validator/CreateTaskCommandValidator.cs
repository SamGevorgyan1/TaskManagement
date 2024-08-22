using FluentValidation;
using TaskManagement.Application.Tasks.Commands;

namespace TaskManagement.Application.Tasks.Validator;

public class CreateTaskCommandValidator : AbstractValidator<CreateTaskCommand>
{
    public CreateTaskCommandValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required.")
            .MaximumLength(100).WithMessage("Title must not exceed 100 characters.");

        RuleFor(x => x.Description)
            .MaximumLength(500).WithMessage("Description must not exceed 500 characters.");

        RuleFor(x => x.Status)
            .IsInEnum().WithMessage("Status is not valid.");

        RuleFor(x => x.Priority)
            .IsInEnum().WithMessage("Priority is not valid.");

        RuleFor(x => x.DueDate)
            .Cascade(CascadeMode.Stop)
            .GreaterThanOrEqualTo(DateTime.Now).WithMessage("DueDate must be in the future.");

        RuleForEach(x => x.AssigneeEmails)
            .EmailAddress().WithMessage("Invalid email format.");
    }
}