using Microsoft.AspNetCore.Diagnostics;
using SchoolManagement.Core.Entities;
using Students.Application.Exceptions;
using System.Net;

namespace Students.Web
{
	public class GlobalExceptionHandler : IExceptionHandler
	{
		public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
		{
			httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
			httpContext.Response.ContentType = "application/json";

			var contextFeatures = httpContext.Features.Get<IExceptionHandlerFeature>();
			if (contextFeatures is not null)
			{
				httpContext.Response.StatusCode = contextFeatures.Error switch
				{
					NotFoundException => StatusCodes.Status404NotFound,
					BadRequestException => StatusCodes.Status400BadRequest,
					_ => StatusCodes.Status500InternalServerError
				};
				await httpContext.Response.WriteAsJsonAsync(new ErrorDetails
				{
					StatusCode = httpContext.Response.StatusCode,
					Message = contextFeatures.Error.Message
				}.ToString(), cancellationToken);
			}
			return true;
		}
	}
}
