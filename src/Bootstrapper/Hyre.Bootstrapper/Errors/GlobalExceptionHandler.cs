// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#region

using System.Net;
using Hyre.Shared.Abstractions.Exceptions;
using Microsoft.AspNetCore.Diagnostics;

#endregion

namespace Hyre.Bootstrapper.Errors;

/// <summary>
///   This class contains the global exception handler.
/// </summary>
public sealed class GlobalExceptionHandler : IExceptionHandler
{
	/// <summary>
	///   This method handles the exception.
	/// </summary>
	/// <param name="httpContext">The http context.</param>
	/// <param name="exception">The exception.</param>
	/// <param name="cancellationToken">Used to cancel the operation.</param>
	/// <returns>It will return true if the exception was handled.</returns>
	public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception,
		CancellationToken cancellationToken)
	{
		httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
		httpContext.Response.ContentType = "application/json";
		var contextFeature = httpContext.Features.Get<IExceptionHandlerFeature>();
		if (contextFeature != null)
		{
			httpContext.Response.StatusCode = contextFeature.Error switch
			{
				BadRequestException => StatusCodes.Status400BadRequest,
				AuthenticationException => StatusCodes.Status401Unauthorized,
				UnauthorizedAccessException => StatusCodes.Status403Forbidden,
				NotFoundException => StatusCodes.Status404NotFound,
				ConflictException => StatusCodes.Status409Conflict,
				DomainException => StatusCodes.Status422UnprocessableEntity,
				_ => StatusCodes.Status500InternalServerError
			};

			await httpContext.Response.WriteAsync(new ErrorDetails
			{
				Status = httpContext.Response.StatusCode,
				Message = contextFeature.Error.Message
			}.ToString(), cancellationToken);
		}

		return true;
	}
}