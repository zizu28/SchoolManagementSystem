
namespace SchoolManagement.Core.ValueObjects
{
	public record PhoneNumber
	{
		public string Number { get; set; }
		public PhoneNumber(string number)
		{
			if (string.IsNullOrWhiteSpace(number))
			{
				throw new ArgumentNullException("Invalid phone number", nameof(number));
			}

			Number = number;
		}
	}
}
