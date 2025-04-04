using FluentValidation;
using Students.Application.DTOs.EntityCreateDTOs;

namespace Students.Application.Validators.DtoCreateValidators
{
	public class EnrollmentCreateDtoValidator : AbstractValidator<EnrollmentCreateDto>
	{
		public EnrollmentCreateDtoValidator()
		{
			RuleFor(x => x.StudentId).NotEmpty();
			RuleFor(x => x.CourseSectionId).NotEmpty();
		}
	}
}
