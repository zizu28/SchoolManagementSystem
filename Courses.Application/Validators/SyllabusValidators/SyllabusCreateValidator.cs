using Courses.Application.DTOs.SyllabusDTOs;
using FluentValidation;

namespace Courses.Application.Validators.SyllabusValidators
{
	public class SyllabusCreateValidator : AbstractValidator<SyllabusCreateDto>
	{
		public SyllabusCreateValidator()
		{
			RuleFor(x => x.LearningOutcomes)
			.NotEmpty().MinimumLength(50)
			.WithMessage("Learning outcomes must be at least 50 characters");

			RuleFor(x => x.Textbooks)
				.NotEmpty()
				.MaximumLength(500);

			RuleFor(x => x.GradingPolicy)
				.NotEmpty()
				.MaximumLength(1000);
		}
	}
}