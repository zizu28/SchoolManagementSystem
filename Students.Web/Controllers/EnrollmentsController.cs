using MediatR;
using Microsoft.AspNetCore.Mvc;
using Students.Application.CQRS.Commands.EnrollmentCommands;
using Students.Application.CQRS.Queries.EnrollmentsQueries;
using Students.Application.DTOs.EntityCreateDTOs;
using Students.Application.DTOs.EntityUpdateDtTOs;

namespace Students.Web.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class EnrollmentsController(IMediator mediator) : ControllerBase
	{
		private readonly IMediator _mediator = mediator;

		[HttpGet]
		public async Task<IActionResult> GetAllEnrollmentsAsync()
		{
			var query = new GetAllEnrollmentsQuery();
			var result = await _mediator.Send(query);
			return Ok(result);
		}

		[HttpGet("{id:guid}", Name = "GetEnrollmentById")]
		public async Task<IActionResult> GetEnrollmentByIdAsync(Guid id)
		{
			var query = new GetEnrollmentByIdQuery { EnrollmentId = id };
			var result = await _mediator.Send(query);
			return Ok(result);
		}

		[HttpPost]
		public async Task<IActionResult> CreateEnrollmentAsync([FromBody] EnrollmentCreateDto enrollment)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			var command = new CreateEnrollmentCommand { Enrollment = enrollment };
			var result = await _mediator.Send(command);
			return CreatedAtRoute("GetEnrollmentById", new { id = result.Id }, result);
		}

		[HttpPut("{id:guid}")]
		public async Task<IActionResult> UpdateEnrollmentAsync(Guid id, [FromBody] EnrollmentUpdateDto enrollment)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			var command = new UpdateEnrollmentCommand { EnrollmentId = id, Enrollment = enrollment };
			var result = await _mediator.Send(command);
			return Ok(result);
		}

		[HttpDelete("{id:guid}")]
		public async Task<IActionResult> DeleteEnrollmentAsync(Guid id)
		{
			var command = new DeleteEnrollmentCommand { EnrollmentId = id };
			await _mediator.Send(command);
			return NoContent();
		}
	}
}
