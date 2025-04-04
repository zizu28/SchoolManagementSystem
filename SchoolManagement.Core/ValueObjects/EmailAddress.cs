namespace SchoolManagement.Core.ValueObjects
{
	public record EmailAddress
	{
		public string Address { get; }
		public EmailAddress(string address)
		{
			if (string.IsNullOrWhiteSpace(address) || !address.Contains('@'))
			{
				throw new ArgumentNullException("Invalid email address", nameof(address));
			}
			Address = address;
		}
	}
}
