using Courses.Application.DTOs.PrerequisiteDTOs;
using FluentValidation;

namespace Courses.Application.Validators.PrerequisiteValidator
{
	public class PrerequisiteCreateValidator : AbstractValidator<PrerequisiteCreateDto>
	{
		public PrerequisiteCreateValidator()
		{
			RuleFor(x => x.RequiredCourseId)
			.NotEmpty()
			.WithMessage("Required course must be specified");
		}
	}
}
