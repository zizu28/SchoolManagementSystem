using FluentValidation;
using Students.Application.DTOs.EntityCreateDTOs;

namespace Students.Application.Validators.DtoCreateValidators
{
	public class AcademicRecordCreateDtoValidator : AbstractValidator<AcademicRecordCreateDto>
	{
		public AcademicRecordCreateDtoValidator()
		{
			RuleFor(x => x.GPA)
				.InclusiveBetween(0, 4m);

			RuleFor(x => x.TotalCredits)
				.GreaterThanOrEqualTo(0);
		}
	}
}
