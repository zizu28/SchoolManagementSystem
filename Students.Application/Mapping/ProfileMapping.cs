using AutoMapper;
using Students.Application.DTOs.EntityCreateDTOs;
using Students.Application.DTOs.EntityUpdateDtTOs;
using Students.Application.DTOs.ResponseDTOs;
using Students.Domain.Entities;
using Students.Domain.Enums;

namespace Students.Application.Mapping
{
	public class ProfileMapping : Profile
	{
		public ProfileMapping()
		{
			CreateMap<StudentCreateDto, Student>();
			CreateMap<StudentUpdateDto, Student>();
			CreateMap<Student, StudentResponseDto>().ReverseMap();


			// Admission application mapping
			CreateMap<AdmissionApplicationCreateDto, AdmissionApplication>()
				.ForMember(dest => dest.Status, 
				option => option.MapFrom(src => Enum.Parse<AdmissionStatus>(src.Status, true)));
			CreateMap<AdmissionApplication, AdmissionApplicationCreateDto>()
				.ForMember(dest => dest.Status,
				option => option.MapFrom(src => src.Status.ToString()));
			CreateMap<AdmissionApplicationUpdateDto, AdmissionApplication>()
				.ForMember(dest => dest.Status,
				option => option.MapFrom(src => Enum.Parse<AdmissionStatus>(src.Status, true)));
			CreateMap<AdmissionApplication, AdmissionApplicationUpdateDto>()
				.ForMember(dest => dest.Status,
				option => option.MapFrom(src => src.Status.ToString()));
			CreateMap<AdmissionApplication, AdmissionApplicationResponseDto>().ReverseMap();

			// Enrollment Mapping
			CreateMap<EnrollmentCreateDto, Enrollment>()
				.ForMember(dest => dest.Status,
				opt => opt.MapFrom(src => Enum.Parse<EnrollmentStatus>(src.Status, true)));
			CreateMap<Enrollment, EnrollmentCreateDto>()
				.ForMember(dest => dest.Status,
				opt => opt.MapFrom(src => src.Status.ToString()));
			CreateMap<EnrollmentUpdateDto, Enrollment>()
				.ForMember(dest => dest.Status,
				opt => opt.MapFrom(src => Enum.Parse<EnrollmentStatus>(src.Status, true)));
			CreateMap<Enrollment, EnrollmentUpdateDto>()
				.ForMember(dest => dest.Status,
				opt => opt.MapFrom(src => src.Status.ToString()));
			CreateMap<Enrollment, EnrollmentResponseDto>().ReverseMap();

			CreateMap<AcademicRecordCreateDto, AcademicRecord>();
			CreateMap<AcademicRecordUpdateDto, AcademicRecord>();
			CreateMap<AcademicRecord, AcademicRecordResponseDto>().ReverseMap();
		}
	}
}
