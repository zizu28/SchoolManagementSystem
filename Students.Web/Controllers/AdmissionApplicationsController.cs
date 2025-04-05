using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Students.Application.CQRS.Commands.AdmissionApplicationCommands;
using Students.Application.CQRS.Queries.AdmissionApplicationQueries;
using Students.Application.DTOs.EntityCreateDTOs;
using Students.Application.DTOs.EntityUpdateDtTOs;
using Students.Application.DTOs.ResponseDTOs;
using Students.Domain.Entities;
using Students.Infrastructure.CacheServices.AdmissionApplicationCache;
using System.Text.Json;

namespace Students.Web.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AdmissionApplicationsController(IMediator mediator, 
		IAdmissionApplicationCache cache, IMapper mapper) : ControllerBase
	{
		private readonly IMediator _mediator = mediator;
		private readonly IAdmissionApplicationCache _cache = cache;
		private readonly IMapper _mapper = mapper;

		[HttpGet]
		public async Task<IActionResult> GetAllAdmissionApplicationsAsync()
		{
			var query = new GetAllAdmissionApplicationsQuery();
			var result = await _cache.GetAllAsync("admission-applications")
				?? await _mediator.Send(query);
			return Ok(result);
		}

		[HttpGet("{id:guid}", Name = "GetAdmissionApplicationById")]
		public async Task<ActionResult<AdmissionApplicationResponseDto>> GetAdmissionApplicationByIdAsync(Guid id)
		{
			var query = new GetAdmissionApplicationByIdQuery { AdmissionApplicationId = id };
			var result = await _cache.GetAsync(id)
				?? await _mediator.Send(query);
			return Ok(result);
		}

		[HttpPost]
		public async Task<IActionResult> CreateAdmissionApplicationAsync([FromBody] AdmissionApplicationCreateDto admissionApplication)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			var command = new CreateAdmissionApplicationCommand { AdmissionApplication = admissionApplication };
			var result = await _mediator.Send(command);
			await _cache.SetAsync(result.Id, _mapper.Map<AdmissionApplication>(admissionApplication));
			return CreatedAtRoute("GetAdmissionApplicationById", new { id = result.Id }, result);
		}

		[HttpPut("{id:guid}")]
		public async Task<IActionResult> UpdateAdmissionApplicationAsync(Guid id, [FromBody] AdmissionApplicationUpdateDto admissionApplication)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			var command = new UpdateAdmissionApplicationCommand { AdmissionApplicationId = id, AdmissionApplication = admissionApplication };
			var result = await _mediator.Send(command);
			return Ok(result);
		}

		[HttpDelete("{id:guid}")]
		public async Task<IActionResult> DeleteAdmissionApplicationAsync(Guid id)
		{
			var command = new DeleteAdmissionApplicationCommand { AdmissionApplicationId = id };
			await _mediator.Send(command);
			await _cache.RemoveAsync(id);
			return NoContent();
		}
	}
}
