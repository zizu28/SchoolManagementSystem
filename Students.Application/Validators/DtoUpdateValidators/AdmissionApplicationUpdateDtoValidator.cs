using FluentValidation;
using Students.Application.DTOs.EntityUpdateDtTOs;

namespace Students.Application.Validators.DtoUpdateValidators
{
	public class AdmissionApplicationUpdateDtoValidator : AbstractValidator<AdmissionApplicationUpdateDto>
	{
		public AdmissionApplicationUpdateDtoValidator()
		{
			When(x => x.ProgramCode != null, () =>
			{
				RuleFor(x => x.ProgramCode).Matches(@"^[A-Z]{3}\d{3}$");
			});
		}
	}
}
