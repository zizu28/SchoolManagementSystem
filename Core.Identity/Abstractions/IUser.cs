namespace Core.Identity.Abstractions
{
	public interface IUser
	{
		Guid Id { get; }
		string Email { get; }
		string UserName { get; }
	}
}
