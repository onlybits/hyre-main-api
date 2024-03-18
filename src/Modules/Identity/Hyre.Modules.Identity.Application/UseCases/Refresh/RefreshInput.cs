// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#region

#endregion

namespace Hyre.Modules.Identity.Application.UseCases.Refresh;

/// <summary>
///   Represents the input data for refreshing a token.
/// </summary>
/// <param name="AccessToken">The access token to refresh.</param>
/// <param name="RefreshToken">The refresh token to use.</param>
public sealed record RefreshInput(string AccessToken, string RefreshToken);