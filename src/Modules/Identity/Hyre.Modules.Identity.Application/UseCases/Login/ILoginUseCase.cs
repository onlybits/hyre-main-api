// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#region

using MediatR;

#endregion

namespace Hyre.Modules.Identity.Application.UseCases.Login;

/// <summary>
///   This is the use case contract
/// </summary>
internal interface ILoginUseCase : IRequestHandler<LoginRequest, LoginResponse>;