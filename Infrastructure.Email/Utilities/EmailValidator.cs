using System.Text.RegularExpressions;

namespace Infrastructure.Email.Utilities
{
	public static partial class EmailValidator
	{
		[GeneratedRegex(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", RegexOptions.IgnoreCase | RegexOptions.Compiled, "en-US")]
		private static partial Regex GenerateEmailRegex();
		private static readonly Regex EmailRegex = GenerateEmailRegex();
		public static bool IsValidEmail(string email)
		{
			return !string.IsNullOrWhiteSpace(email) && EmailRegex.IsMatch(email);
		}

		public static IEnumerable<string> FilterValidEmails(IEnumerable<string> emails)
		{
			return emails.Where(IsValidEmail);
		}
	}
}
