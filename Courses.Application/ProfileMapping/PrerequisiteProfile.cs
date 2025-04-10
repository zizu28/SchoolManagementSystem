using AutoMapper;
using Courses.Application.DTOs.PrerequisiteDTOs;
using Courses.Domain.Entities;

namespace Courses.Application.ProfileMapping
{
	public class PrerequisiteProfile : Profile
	{
		public PrerequisiteProfile()
		{
			CreateMap<PrerequisiteCreateDto, Prerequisite>()
			.ForMember(dest => dest.CourseId, opt => opt.Ignore()) // Set in handler
			.ForMember(dest => dest.RequiredCourse, opt => opt.Ignore());

			// Update DTO -> Entity
			CreateMap<PrerequisiteUpdateDto, Prerequisite>()
				.ForAllMembers(opts => opts.Condition(
					(src, dest, srcMember) => srcMember != null
				));

			// Entity -> Response DTO
			CreateMap<Prerequisite, PrerequisiteResponseDto>()
				.ForMember(dest => dest.RequiredCourse,
					opt => opt.MapFrom(src => src.RequiredCourse));
		}
	}
}
