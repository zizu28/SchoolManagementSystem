using FluentValidation;
using Students.Application.DTOs.EntityCreateDTOs;

namespace Students.Application.Validators.DtoCreateValidators
{
	public class AdmissionApplicationCreateDtoValidator : AbstractValidator<AdmissionApplicationCreateDto>
	{
		public AdmissionApplicationCreateDtoValidator()
		{
			RuleFor(x => x.ProgramCode)
				.Matches(@"^[A-Z]{3}-\d{3}$");
		}
	}
}
