using System.Security.Claims;

namespace SchoolManagement.Core.Contracts
{
	public interface IIdentityService
	{
		Task<bool> ValidateTokenAsyn(string token);
		Task<IEnumerable<Claim>> GetUserClaimsAsync(Guid userId);
	}
}
