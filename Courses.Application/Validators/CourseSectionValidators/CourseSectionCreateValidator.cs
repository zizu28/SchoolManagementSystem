using Courses.Application.DTOs.CourseSectionDTOs;
using FluentValidation;

namespace Courses.Application.Validators.CourseSectionValidators
{
	public class CourseSectionCreateValidator : AbstractValidator<CourseSectionCreateDto>
	{
		public CourseSectionCreateValidator()
		{
			RuleFor(x => x.CourseId).NotEmpty();
			RuleFor(x => x.FacultyId).NotEmpty();
			RuleFor(x => x.RoomNumber).NotEmpty().MaximumLength(20);

			RuleFor(x => x.StartDate)
				.GreaterThan(DateTime.UtcNow.AddDays(1))
				.WithMessage("Start date must be at least 24 hours in future");

			RuleFor(x => x.EndDate)
				.GreaterThan(x => x.StartDate)
				.WithMessage("End date must be after start date");

			RuleFor(x => x.MaxCapacity)
				.InclusiveBetween(10, 300);

			RuleFor(x => x.ScheduleSlots)
			.NotEmpty()
			.ForEach(slot => slot.SetValidator(new ScheduleSlotCreateValidator()));
		}
	}
}
