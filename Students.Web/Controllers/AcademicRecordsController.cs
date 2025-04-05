using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Students.Application.CQRS.Commands.AcademicRecordsCommands;
using Students.Application.CQRS.Queries.AcademicRecordsQueries;
using Students.Application.DTOs.EntityCreateDTOs;
using Students.Application.DTOs.EntityUpdateDtTOs;
using Students.Application.DTOs.ResponseDTOs;
using Students.Domain.Entities;
using Students.Infrastructure.CacheServices.AcademicRecordCache;

namespace Students.Web.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AcademicRecordsController(IMediator mediator, 
		IAcademicRecordCacheService cacheService, IMapper mapper) : ControllerBase
	{
		private readonly IMediator _mediator = mediator;
		private readonly IAcademicRecordCacheService _cacheService = cacheService;
		private readonly IMapper _mapper = mapper;

		[HttpGet]
		public async Task<IActionResult> GetAllAcademicRecordsAsync()
		{
			var query = new GetAllAcademicRecordsQuery();
			var result = await _cacheService.GetAllAsync("academic-records")
				??	await _mediator.Send(query);
			return Ok(result);
		}

		[HttpGet("{id:guid}", Name = "GetAcademicRecordById")]
		public async Task<ActionResult<AcademicRecordResponseDto>> GetAcademicRecordByIdAsync(Guid id)
		{
			var query = new GetAcademicRecordByIdQuery { AcademicRecordId = id };
			var result = await _cacheService.GetAsync(id)
				?? await _mediator.Send(query);
			return Ok(result);
		}

		[HttpPost]
		public async Task<IActionResult> CreateAcademicRecordAsync([FromBody] AcademicRecordCreateDto academicRecord)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			var command = new CreateAcademicRecordCommand { AcademicRecord = academicRecord };
			var result = await _mediator.Send(command);
			await _cacheService.SetAsync(result.Id, _mapper.Map<AcademicRecord>(academicRecord));
			return CreatedAtRoute("GetAcademicRecordById", new { id = result.Id }, result);
		}

		[HttpPut("{id:guid}")]
		public async Task<IActionResult> UpdateAcademicRecordAsync(Guid id, [FromBody] AcademicRecordUpdateDto academicRecord)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			var command = new UpdateAcademicRecordCommand { AcademicRecordId = id, AcademicRecord = academicRecord };
			var result = await _mediator.Send(command);
			return Ok(result);
		}

		[HttpDelete("{id:guid}")]
		public async Task<IActionResult> DeleteAcademicRecordAsync(Guid id)
		{
			var command = new DeleteAcademicRecordCommand { AcademicRecordId = id };
			await _mediator.Send(command);
			await _cacheService.RemoveAsync(id);
			return NoContent();
		}
	}
}
