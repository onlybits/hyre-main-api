// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#region

using Hyre.Modules.Identity.Application.UseCases.Login;
using MediatR;
using Microsoft.AspNetCore.Mvc;

#endregion

namespace Hyre.Modules.Identity.API.Controllers;

[ApiController]
[Route("api/authentication")]
public sealed class AuthenticationController : ControllerBase
{
	private readonly ISender _sender;

	public AuthenticationController(ISender sender) => _sender = sender;

	[HttpPost("login")]
	public async Task<IActionResult> Login([FromBody] LoginInput input, CancellationToken cancellationToken)
	{
		var request = new LoginRequest(input);
		var result = await _sender.Send(request, cancellationToken);

		return Ok(result);
	}
}