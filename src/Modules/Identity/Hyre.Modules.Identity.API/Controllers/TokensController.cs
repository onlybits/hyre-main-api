// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#region

using Hyre.Modules.Identity.Application.UseCases.Refresh;
using MediatR;
using Microsoft.AspNetCore.Mvc;

#endregion

namespace Hyre.Modules.Identity.API.Controllers;

[ApiController]
[Route("api/tokens")]
public sealed class TokensController : ControllerBase
{
	private readonly ISender _sender;

	public TokensController(ISender sender) => _sender = sender;

	[HttpPost("refresh")]
	public async Task<IActionResult> Refresh([FromBody] RefreshInput input, CancellationToken cancellationToken)
	{
		var request = new RefreshRequest(input);
		var result = await _sender.Send(request, cancellationToken);

		return Ok(result);
	}
}