using FluentValidation;
using Students.Application.DTOs.EntityCreateDTOs;

namespace Students.Application.Validators.DtoCreateValidators
{
	public class StudentCreateDtoValidator : AbstractValidator<StudentCreateDto>
	{
		public StudentCreateDtoValidator()
		{
			RuleFor(x => x.FirstName)
				.NotEmpty().WithMessage("First name cannot be empty.")
				.Length(2, 50)
				.Matches(@"^[a-zA-Z]+$");

			RuleFor(x => x.LastName)
				.NotEmpty().WithMessage("Last name cannot be empty.")
				.Length(2, 50)
				.Matches(@"^[a-zA-Z]+$");

			RuleFor(x => x.Email)
				.NotEmpty().WithMessage("Email address should not be empty")
				.EmailAddress();

			RuleFor(x => x.DateOfBirth)
				.LessThan(DateOnly.FromDateTime(DateTime.Today.AddYears(-16)))
				.WithMessage("Student must be at least 16 years old.");
		}
	}
}
