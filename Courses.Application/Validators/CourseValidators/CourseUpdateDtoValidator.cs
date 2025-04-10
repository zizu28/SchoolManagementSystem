using Courses.Application.DTOs.CourseDTOs;
using FluentValidation;

namespace Courses.Application.Validators.CourseValidators
{
	public partial class CourseCreateDtoValidator
	{
		public class CourseUpdateDtoValidator : AbstractValidator<CourseUpdateDto>
		{
			public CourseUpdateDtoValidator()
			{
				When(x => x.Code != null, () => {
					RuleFor(x => x.Code)
						.Matches(@"^[A-Z]{2,4}-\d{3}$");
				});

				When(x => x.Title != null, () => {
					RuleFor(x => x.Title)
						.MaximumLength(100);
				});

				When(x => x.Credits > 0, () => {
					RuleFor(x => x.Credits)
						.InclusiveBetween(1, 5);
				});

				RuleFor(x => x)
					.Must(x => x.Code != null || x.Title != null || x.Description != null ||
							  x.Credits <= 0 || x.Type != null || x.DepartmentId != null)
					.WithMessage("At least one field must be provided for update");
				}
		}
	}
}
