using Core.Identity.Abstractions;
using Microsoft.AspNetCore.Identity;

namespace Identity.API.Models
{
	public class ApplicationUser : IdentityUser<Guid>, IUser
	{
	}
}
