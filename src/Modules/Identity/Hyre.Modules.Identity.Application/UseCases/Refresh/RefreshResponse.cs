// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace Hyre.Modules.Identity.Application.UseCases.Refresh;

/// <summary>
///   Represents the response for refreshing a token.
/// </summary>
/// <param name="AccessToken">The new access token.</param>
/// <param name="RefreshToken">The new refresh token.</param>
public sealed record RefreshResponse(string AccessToken, string RefreshToken);