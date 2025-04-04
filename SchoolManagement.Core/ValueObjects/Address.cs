
namespace SchoolManagement.Core.ValueObjects
{
	public record Address
	{
		public string Street { get; init; }
		public string City { get; init; }
		public string PostalCode { get; init; }
		public string Country { get; init; }

		public Address(string street, string city, string postalCode, string country)
		{
			if(string.IsNullOrWhiteSpace(street) || string.IsNullOrWhiteSpace(city) 
				|| string.IsNullOrWhiteSpace(postalCode) || string.IsNullOrWhiteSpace(country))
			{
				throw new ArgumentNullException("Neither street nor city nor postal code nor country" +
					" can be null.");
			}

			Street = street;
			City = city;
			PostalCode = postalCode;
			Country = country;
		}
	}
}
