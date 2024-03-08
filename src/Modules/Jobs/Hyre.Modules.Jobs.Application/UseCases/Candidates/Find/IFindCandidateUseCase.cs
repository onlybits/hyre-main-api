// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#region

using Hyre.Modules.Jobs.Application.Common;
using MediatR;

#endregion

namespace Hyre.Modules.Jobs.Application.UseCases.Candidates.Find;

/// <summary>
///   This is the use case contract to find a candidate.
/// </summary>
internal interface IFindCandidateUseCase : IRequestHandler<FindCandidateRequest, CandidateResponse>;