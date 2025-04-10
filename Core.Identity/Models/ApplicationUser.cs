using Core.Identity.Abstractions;
using Microsoft.AspNetCore.Identity;

namespace Core.Identity.Models
{
	public class ApplicationUser : IdentityUser<Guid>, IUser
	{
	}
}
