using FluentValidation;
using Students.Application.DTOs.EntityUpdateDtTOs;

namespace Students.Application.Validators.DtoUpdateValidators
{
	public class AcademicRecordUpdateDtoValidator : AbstractValidator<AcademicRecordUpdateDto>
	{
		public AcademicRecordUpdateDtoValidator()
		{
			When(x => x.GPA > 0m, () => {
				RuleFor(x => x.GPA).InclusiveBetween(0, 4.0m);
			});

			When(x => x.TotalCredits >= 0m, () => {
				RuleFor(x => x.TotalCredits).GreaterThanOrEqualTo(0);
			});
		}
	}
}
