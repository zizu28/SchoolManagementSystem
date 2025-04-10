using Courses.Application.DTOs.SyllabusDTOs;
using FluentValidation;

namespace Courses.Application.Validators.SyllabusValidators
{
	public class SyllabusUpdateValidator : AbstractValidator<SyllabusUpdateDto>
	{
		public SyllabusUpdateValidator()
		{
			When(x => x.LearningOutcomes != null, () =>
			{
				RuleFor(x => x.LearningOutcomes)
					.MinimumLength(50);
			});

			When(x => x.Textbooks != null, () =>
			{
				RuleFor(x => x.Textbooks)
					.MaximumLength(500);
			});

			When(x => x.GradingPolicy != null, () =>
			{
				RuleFor(x => x.GradingPolicy)
					.MaximumLength(1000);
			});

			RuleFor(x => x)
				.Must(x => x.LearningOutcomes != null || x.Textbooks != null || x.GradingPolicy != null)
				.WithMessage("At least one syllabus field must be provided");
		}
	}
}