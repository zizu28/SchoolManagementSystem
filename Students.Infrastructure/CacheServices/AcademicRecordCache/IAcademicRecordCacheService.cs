﻿using Students.Application.DTOs.ResponseDTOs;
using Students.Domain.Entities;

namespace Students.Infrastructure.CacheServices.AcademicRecordCache
{
	public interface IAcademicRecordCacheService : IGenericCacheService<AcademicRecord, AcademicRecordResponseDto>
	{
	}
}
