using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Students.Application.CQRS.Commands.StudentCommands;
using Students.Application.CQRS.Queries.StudentsQueries;
using Students.Application.DTOs.EntityCreateDTOs;
using Students.Application.DTOs.EntityUpdateDtTOs;
using Students.Domain.Entities;
using Students.Infrastructure.CacheServices.StudentCache;

namespace Students.Web.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class StudentsController(IMediator mediator,
		IStudentCacheService cacheService, IMapper mapper) : ControllerBase
	{
		private readonly IMediator _mediator = mediator;
		private readonly IStudentCacheService _cacheService = cacheService;
		private readonly IMapper _mapper = mapper;

		[HttpGet]
		public async Task<IActionResult> GetAllStudentsAsync()
		{
			var query = new GetAllStudentsQuery();
			var result = await _cacheService.GetAllAsync("students");
			if (!result.Any() || result == null)
			{
				var resultFromDb = await _mediator.Send(query);
				await _cacheService.SetManyAsync("students", _mapper.Map<IEnumerable<Student>>(resultFromDb));
				return Ok(resultFromDb);
			}
			return Ok(result);
		}

		[HttpGet("{id:guid}", Name = "GetStudentById")]
		public async Task<IActionResult> GetStudentByIdAsync(Guid id)
		{
			var query = new GetStudentByIdQuery { StudentId = id };
			var result = await _cacheService.GetAsync(id)
				?? await _mediator.Send(query);
			return Ok(result);
		}


		[HttpPost]
		public async Task<IActionResult> CreateStudentAsync([FromBody] StudentCreateDto student)
		{
			if(!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			var command = new CreateStudentCommand { Student = student };
			var result = await _mediator.Send(command);
			return CreatedAtRoute("GetStudentById", new { id = result.Id }, result);
		}

		[HttpPut("{id:guid}")]
		public async Task<IActionResult> UpdateStudentAsync(Guid id, [FromBody] StudentUpdateDto student)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			var command = new UpdateStudentCommand { StudentId = id, Student = student };
			var result = await _mediator.Send(command);
			return Ok(result);
		}

		[HttpDelete("{id:guid}")]
		public async Task<IActionResult> DeleteStudentAsync(Guid id)
		{
			var command = new DeleteStudentCommand { StudentId = id };
			await _mediator.Send(command);
			return NoContent();
		}
	}
}
