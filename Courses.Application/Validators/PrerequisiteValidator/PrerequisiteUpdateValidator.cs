using Courses.Application.DTOs.PrerequisiteDTOs;
using FluentValidation;

namespace Courses.Application.Validators.PrerequisiteValidator
{
	public class PrerequisiteUpdateValidator : AbstractValidator<PrerequisiteUpdateDto>
	{
		public PrerequisiteUpdateValidator()
		{
			RuleFor(x => x.RequiredCourseId)
				.NotEmpty()
				.When(x => x.RequiredCourseId != null)
				.WithMessage("New required course must be specified");
		}
	}
}
