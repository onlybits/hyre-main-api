// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#region

using Hyre.Shared.Abstractions.Kernel.ValueObjects;

#endregion

namespace Hyre.Modules.Jobs.Core.ValueObjects.Candidates;

/// <summary>
///   Represents the candidate's disability.
/// </summary>
/// <param name="Hearing">The candidate has a hearing disability.</param>
/// <param name="Vision">The candidate has a vision disability.</param>
/// <param name="Intellectual">The candidate has an intellectual disability.</param>
/// <param name="Physical">The candidate has a physical disability.</param>
public sealed record CandidateDisability(
	bool? Hearing,
	bool? Vision,
	bool? Intellectual,
	bool? Physical) : ValueObject;