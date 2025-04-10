using AutoMapper;
using Courses.Application.DTOs.CourseSectionDTOs;
using Courses.Domain.Entities;

namespace Courses.Application.ProfileMapping
{
	public class CourseSectionProfile : Profile
	{
		public CourseSectionProfile()
		{
			CreateMap<CourseSectionCreateDto, CourseSection>()
			.ForMember(dest => dest.Id, opt => opt.Ignore())
			.ForMember(dest => dest.Course, opt => opt.Ignore())
			.ForMember(dest => dest.Schedule,
				opt => opt.MapFrom(src => src.ScheduleSlots));

			CreateMap<ScheduleSlotCreateDto, ScheduleSlot>()
				.ForMember(dest => dest.Id, opt => opt.Ignore());

			// Update DTO -> Entity
			CreateMap<CourseSectionUpdateDto, CourseSection>()
				.ForAllMembers(opts => opts.Condition(
					(src, dest, srcMember) => srcMember != null
				));

			// Entity -> Response DTO
			CreateMap<CourseSection, CourseSectionResponseDto>()
				.ForMember(dest => dest.Course,
					opt => opt.MapFrom(src => src.Course))
				.ForMember(dest => dest.ScheduleSlots,
					opt => opt.MapFrom(src => src.Schedule));

			CreateMap<ScheduleSlot, ScheduleSlotResponseDto>()
				.ForMember(dest => dest.TimeSlot,
					opt => opt.MapFrom(src =>
						$"{src.StartTime:HH:mm} - {src.EndTime:HH:mm}"));
		}
	}
}
