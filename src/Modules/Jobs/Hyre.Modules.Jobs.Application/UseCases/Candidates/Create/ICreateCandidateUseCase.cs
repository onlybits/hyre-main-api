// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#region

using Hyre.Modules.Jobs.Application.Common;
using MediatR;

#endregion

namespace Hyre.Modules.Jobs.Application.UseCases.Candidates.Create;

/// <summary>
///   This use case contract responsible for creating a candidate.
/// </summary>
internal interface ICreateCandidateUseCase : IRequestHandler<CreateCandidateRequest, CandidateResponse>;