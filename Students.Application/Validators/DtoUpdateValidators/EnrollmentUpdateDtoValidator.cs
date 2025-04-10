using FluentValidation;
using Students.Application.DTOs.EntityUpdateDtTOs;

namespace Students.Application.Validators.DtoUpdateValidators
{
	public class EnrollmentUpdateDtoValidator : AbstractValidator<EnrollmentUpdateDto>
	{
		public EnrollmentUpdateDtoValidator()
		{
			RuleFor(x => x.StudentId).NotEmpty();
			RuleFor(x => x.CourseOfferingId).NotEmpty();
		}
	}
}
