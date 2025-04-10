using Core.Identity.Abstractions;
using Core.Identity.Models;
using Microsoft.AspNetCore.Identity;

namespace Identity.API.Services
{
	public class IdentityService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
		: IIdentityService
	{
		private readonly UserManager<ApplicationUser> _userManager = userManager;
		private readonly SignInManager<ApplicationUser> _signInManager = signInManager;

		public async Task<Result> AddToRoleAsync(string userName, string roleName)
		{
			var user = await _userManager.FindByNameAsync(userName);
			if (user is null)
			{
				return Result.Failure(["User not found."]);
			}
			var result = await _userManager.AddToRoleAsync(user, roleName);
			if (result.Succeeded)
			{
				return Result.Success();
			}
			else
			{
				return Result.Failure(result.Errors.Select(e => e.Description));
			}
		}

		public async Task<Result> ChangePasswordAsync(string userName, string currentPassword, string newPassword)
		{
			var user = await _userManager.FindByNameAsync(userName);
			if (user is null)
			{
				return Result.Failure(["User not found."]);
			}

			var result = await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);

			if (result.Succeeded)
			{
				return Result.Success();
			}
			else
			{
				return Result.Failure(result.Errors.Select(e => e.Description));
			}
		}

		public async Task<Result<bool>> IsInRoleAsync(string userName, string roleName)
		{
			var user = await _userManager.FindByNameAsync(userName);
			if (user is null)
			{
				return Result<bool>.Failure(["User not found."]);
			}

			var isInRole = await _userManager.IsInRoleAsync(user, roleName);
			if (isInRole)
			{
				return Result<bool>.Success(true);
			}
			else
			{
				return Result<bool>.Failure(["User is not in the specified role."]);
			}
		}

		public async Task<Result> LoginAsync(string userName, string password)
		{
			var result = await _signInManager
				.PasswordSignInAsync(userName, password, isPersistent: false, lockoutOnFailure: false);
			if (result.Succeeded)
			{
				return Result.Success();
			}
			else
			{
				return Result.Failure(["Invalid login attempt."]);
			}
		}

		public async Task<Result> LogoutAsync()
		{
			await _signInManager.SignOutAsync();
			return Result.Success();
		}

		public async Task<Result> RegisterAsync(string email, string userName, string password)
		{
			var user = new ApplicationUser { UserName = userName, Email = email };
			var result = await _userManager.CreateAsync(user, password);
			if (result.Succeeded)
			{
				return Result.Success();
			}
			else
			{
				return Result.Failure(result.Errors.Select(e => e.Description));
			}
		}

		public async Task<Result> RemoveFromRoleAsync(string userName, string roleName)
		{
			var user = await _userManager.FindByNameAsync(userName);
			if (user is null)
			{
				return Result.Failure(["User not found."]);
			}
			var result = await _userManager.RemoveFromRoleAsync(user, roleName);
			if (result.Succeeded)
			{
				return Result.Success();
			}
			else
			{
				return Result.Failure(result.Errors.Select(e => e.Description));
			}
		}
	}
}