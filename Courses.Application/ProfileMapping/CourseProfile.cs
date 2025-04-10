using AutoMapper;
using Courses.Application.DTOs.CourseDTOs;
using Courses.Domain.Entities;

namespace Courses.Application.ProfileMapping
{
	public class CourseProfile : Profile
	{
		public CourseProfile()
		{
			CreateMap<CourseCreateDto, Course>()
				.ForMember(dest => dest.CourseId, opt => opt.Ignore())
				.ForMember(dest => dest.Sections, opt => opt.Ignore())
				.ForMember(dest => dest.Prerequisites, opt => opt.Ignore())
				.ForMember(dest => dest.Syllabus, opt => opt.Ignore());

			CreateMap<CourseUpdateDto, Course>()
				.ForAllMembers(opts => opts.Condition(
				(src, dest, srcMember) => srcMember != null)); 

			CreateMap<Course, CourseResponseDto>();
		}
	}
}
