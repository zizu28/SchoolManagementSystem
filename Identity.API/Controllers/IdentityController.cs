using Core.Identity.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Identity.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class IdentityController(IIdentityService identityService) : ControllerBase
	{
		private readonly IIdentityService _identityService = identityService;

		[HttpPost("register")]
		public async Task<IActionResult> Register(string email, string username, string password)
		{
			var result = await _identityService.RegisterAsync(email, username, password);
			if(result.Succeeded)
			{
				return Ok(result);
			}
			return BadRequest(result);
		}

		[HttpPost("login")]
		public async Task<IActionResult> Login(string username, string password)
		{
			var result = await _identityService.LoginAsync(username, password);
			if (result.Succeeded)
			{
				return Ok(result);
			}
			return BadRequest(result);
		}

		[HttpPost("logout")]
		[Authorize]
		public async Task<IActionResult> Logout()
		{
			var result = await _identityService.LogoutAsync();
			if (result.Succeeded)
			{
				return Ok(result);
			}
			return BadRequest(result);
		}

		[HttpPost("change-password")]
		[Authorize]
		public async Task<IActionResult> ChangePassword(string username, string oldPassword, string newPassword)
		{
			var result = await _identityService.ChangePasswordAsync(username, oldPassword, newPassword);
			if (result.Succeeded)
			{
				return Ok(result);
			}
			return BadRequest(result);
		}

		[HttpPost("add-to-role")]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> AddToRole(string username, string roleName)
		{
			var result = await _identityService.AddToRoleAsync(username, roleName);
			if (result.Succeeded)
			{
				return Ok(result);
			}
			return BadRequest(result);
		}

		[HttpPost("remove-from-role")]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> RemoveFromRole(string username, string roleName)
		{
			var result = await _identityService.RemoveFromRoleAsync(username, roleName);
			if (result.Succeeded)
			{
				return Ok(result);
			}
			return BadRequest(result);
		}

		[HttpGet("is-in-role")]
		[Authorize]
		public async Task<IActionResult> IsInRole(string username, string roleName)
		{
			var result = await _identityService.IsInRoleAsync(username, roleName);
			if (result.Succeeded)
			{
				return Ok(result);
			}
			return BadRequest(result);
		}
	}
}
