using FluentValidation;
using Students.Application.DTOs.EntityUpdateDtTOs;

namespace Students.Application.Validators.DtoUpdateValidators
{
	public class StudentUpdateDtoValidator : AbstractValidator<StudentUpdateDto>
	{
		public StudentUpdateDtoValidator()
		{
			When(x => x.FirstName != null, () =>
			{
				RuleFor(x => x.FirstName).Length(2, 50);
			});

			When(x => x.LastName != null, () =>
			{
				RuleFor(x => x.FirstName).Length(2, 50);
			});

			When(x => x.Email != null, () =>
			{
				RuleFor(x => x.Email).EmailAddress();
			});

			When(x => x.DateOfBirth > DateOnly.FromDateTime(DateTime.Today.AddYears(-16)), () =>
			{
				RuleFor(x => x.DateOfBirth)
				.LessThan(DateOnly.FromDateTime(DateTime.Today.AddYears(-16)))
				.WithMessage("Student should be at least 16 years old");
			});
		}
	}
}
