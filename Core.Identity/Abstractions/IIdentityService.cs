using Core.Identity.Models;

namespace Core.Identity.Abstractions
{
	public interface IIdentityService
	{
		Task<Result> RegisterAsync(string email, string userName, string password);
		Task<Result> LoginAsync(string userName, string password);
		Task<Result> LogoutAsync();
		Task<Result> ChangePasswordAsync(string userName, string currentPassword, string newPassword);
		Task<Result> AddToRoleAsync(string userName, string roleName);
		Task<Result> RemoveFromRoleAsync(string userName, string roleName);
		Task<Result<bool>> IsInRoleAsync(string userName, string roleName);
	}
}
