using Courses.Application.DTOs.CourseDTOs;
using FluentValidation;

namespace Courses.Application.Validators.CourseValidators
{
	public partial class CourseCreateDtoValidator : AbstractValidator<CourseCreateDto>
	{
		public CourseCreateDtoValidator()
		{
			RuleFor(x => x.Code)
			.NotEmpty()
			.Matches(@"^[A-Z]{2,4}-\d{3}$")
			.WithMessage("Invalid course code format (e.g., 'CS-101')");

			RuleFor(x => x.Title)
				.NotEmpty()
				.MaximumLength(100);

			RuleFor(x => x.Credits)
				.InclusiveBetween(1, 5)
				.WithMessage("Credits must be between 1-5");

			RuleFor(x => x.DepartmentId)
				.NotEmpty();
		}
	}
}
