// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace Hyre.Modules.Identity.Application.UseCases.Login;

/// <summary>
///   Response for the login use case
/// </summary>
/// <param name="AccessToken">The access token</param>
/// <param name="RefreshToken">The refresh token</param>
public sealed record LoginResponse(string AccessToken, string RefreshToken);