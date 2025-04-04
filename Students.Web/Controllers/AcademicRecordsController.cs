using MediatR;
using Microsoft.AspNetCore.Mvc;
using Students.Application.CQRS.Commands.AcademicRecordsCommands;
using Students.Application.CQRS.Queries.AcademicRecordsQueries;
using Students.Application.DTOs.EntityCreateDTOs;
using Students.Application.DTOs.EntityUpdateDtTOs;

namespace Students.Web.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AcademicRecordsController(IMediator mediator) : ControllerBase
	{
		private readonly IMediator _mediator = mediator;

		[HttpGet]
		public async Task<IActionResult> GetAllAcademicRecordsAsync()
		{
			var query = new GetAllAcademicRecordsQuery();
			var result = await _mediator.Send(query);
			return Ok(result);
		}

		[HttpGet("{id:guid}", Name = "GetAcademicRecordById")]
		public async Task<IActionResult> GetAcademicRecordByIdAsync(Guid id)
		{
			var query = new GetAcademicRecordByIdQuery { AcademicRecordId = id };
			var result = await _mediator.Send(query);
			return Ok(result);
		}

		[HttpPost]
		public async Task<IActionResult> CreateAcademicRecordAsync([FromBody] AcademicRecordUpddateDto academicRecord)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			var command = new CreateAcademicRecordCommand { AcademicRecord = academicRecord };
			var result = await _mediator.Send(command);
			return CreatedAtAction("GetAcademicRecordById", new { id = result.Id }, result);
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
			return NoContent();
		}
	}
}
