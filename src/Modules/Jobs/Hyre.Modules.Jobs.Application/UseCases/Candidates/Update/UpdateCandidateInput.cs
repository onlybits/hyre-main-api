// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#region

using Hyre.Modules.Jobs.Core.ValueObjects.Candidates;

#endregion

namespace Hyre.Modules.Jobs.Application.UseCases.Candidates.Update;

/// <summary>
///   Represents the input data to update a candidate.
/// </summary>
/// <param name="Name">The new candidate's name.</param>
public sealed record UpdateCandidateInput(
	CandidateName? Name);