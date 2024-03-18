// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#region

using Hyre.Modules.Identity.Application.Exceptions;
using Hyre.Modules.Identity.Application.Services;

#endregion

namespace Hyre.Modules.Identity.Application.UseCases.Login;

/// <summary>
///   This is the use case that will handle the login operation.
/// </summary>
/// <inheritdoc cref="ILoginUseCase" />
internal sealed class LoginUseCase : ILoginUseCase
{
	private readonly IIdentityService _identityService;

	/// <summary>
	///   Initializes a new instance of the <see cref="LoginUseCase" /> class.
	/// </summary>
	/// <param name="identityService">The identity service.</param>
	public LoginUseCase(IIdentityService identityService) => _identityService = identityService;

	public async Task<LoginResponse> Handle(LoginRequest request, CancellationToken cancellationToken)
	{
		if (!await _identityService.ValidateAsync(request.Input.Email, request.Input.Password))
		{
			throw new UserInvalidCredentialsException();
		}

		var (accessToken, refreshToken) = await _identityService.CreateAccessTokenAsync(true);

		return new LoginResponse(accessToken, refreshToken);
	}
}