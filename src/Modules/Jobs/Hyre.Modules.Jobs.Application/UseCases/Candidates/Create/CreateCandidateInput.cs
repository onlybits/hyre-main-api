// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#region

using Hyre.Modules.Jobs.Core.ValueObjects.Candidates;

#endregion

namespace Hyre.Modules.Jobs.Application.UseCases.Candidates.Create;

/// <summary>
///   Represents the input data for the create candidate use case.
/// </summary>
/// <param name="Name">The candidate's name.</param>
/// <param name="Email">The candidate's email.</param>
public sealed record CreateCandidateInput(
	CandidateName Name,
	CandidateEmail Email);