using Courses.Application.DTOs.CourseSectionDTOs;
using FluentValidation;

namespace Courses.Application.Validators.CourseSectionValidators
{
	public class ScheduleSlotCreateValidator : AbstractValidator<ScheduleSlotCreateDto>
	{
		public ScheduleSlotCreateValidator()
		{
			RuleFor(x => x.DayOfWeek).IsInEnum();
			RuleFor(x => x.StartTime).LessThan(x => x.EndTime);
		}
	}
}
