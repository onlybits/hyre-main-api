// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#region

using Hyre.Modules.Identity.Application.Services;

#endregion

namespace Hyre.Modules.Identity.Application.UseCases.Refresh;

/// <summary>
///   This is the use case for refreshing a token.
/// </summary>
/// <inheritdoc cref="IRefreshUseCase" />
internal sealed class RefreshUseCase : IRefreshUseCase
{
	private readonly IIdentityService _identityService;

	/// <summary>
	///   Initializes a new instance of the <see cref="RefreshUseCase" /> class.
	/// </summary>
	/// <param name="identityService">The identity service to use.</param>
	public RefreshUseCase(IIdentityService identityService) => _identityService = identityService;

	public async Task<RefreshResponse> Handle(RefreshRequest request, CancellationToken cancellationToken)
	{
		var (accessToken, refreshToken) = await _identityService.CreateRefreshTokenAsync(request.Input.AccessToken, request.Input.RefreshToken);

		return new RefreshResponse(accessToken, refreshToken);
	}
}