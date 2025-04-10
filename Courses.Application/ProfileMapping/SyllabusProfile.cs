using AutoMapper;
using Courses.Application.DTOs.SyllabusDTOs;
using Courses.Domain.Entities;

namespace Courses.Application.ProfileMapping
{
	public class SyllabusProfile : Profile
	{
		public SyllabusProfile()
		{
			CreateMap<SyllabusCreateDto, Syllabus>()
			.ForMember(dest => dest.Id, opt => opt.Ignore())
			.ForMember(dest => dest.Course, opt => opt.Ignore())
			.ForMember(dest => dest.CourseId, opt => opt.Ignore());

			// Update DTO -> Entity
			CreateMap<SyllabusUpdateDto, Syllabus>()
				.ForAllMembers(opts => opts.Condition(
					(src, dest, srcMember) => srcMember != null
				));

			// Entity -> Response DTO
			CreateMap<Syllabus, SyllabusResponseDto>()
				.ForMember(dest => dest.Course,
					opt => opt.MapFrom(src => src.Course))
				.ForMember(dest => dest.LastUpdated,
					opt => opt.MapFrom(src => src.Course.UpdatedAt));
		}
	}
}
