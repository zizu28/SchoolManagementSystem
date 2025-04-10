using Courses.Application.DTOs.CourseSectionDTOs;
using FluentValidation;

namespace Courses.Application.Validators.CourseSectionValidators
{
	public class CourseSectionUpdateValidator : AbstractValidator<CourseSectionUpdateDto>
	{
		public CourseSectionUpdateValidator()
		{
			When(x => x.CourseId != null, () =>
				RuleFor(x => x.CourseId).NotEmpty());

			When(x => x.RoomNumber != null, () =>
				RuleFor(x => x.RoomNumber).MaximumLength(20));

			RuleFor(x => x).Must(x =>
				x.CourseId != null || x.FacultyId != null ||
				x.RoomNumber != null || x.StartDate != null ||
				x.EndDate != null || x.MaxCapacity != null ||
				x.ScheduleSlots != null
			).WithMessage("At least one field must be provided");
		}
	}
}
