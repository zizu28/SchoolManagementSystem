namespace SchoolManagement.Core.Constants
{
	public static class ApiConstant
	{
		// API Base URL (can be configured via appsettings if needed)
		public const string BaseUrl = "https://api.school.edu";

		// Default page size for pagination
		public const int PageSizeDefault = 20;

		// Status codes for API responses
		public static class StatusCodes
		{
			public const int Success = 200;
			public const int Created = 201;
			public const int NotFound = 404;
			public const int BadRequest = 400;
		}
	}
}
