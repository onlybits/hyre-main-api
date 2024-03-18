// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace Hyre.Modules.Identity.Application.UseCases.Login;

/// <summary>
///   Represents the input data to login.
/// </summary>
/// <param name="Email">The email of the user.</param>
/// <param name="Password">The password of the user.</param>
public sealed record LoginInput(string Email, string Password);