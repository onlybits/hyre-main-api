// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#region

using Hyre.Shared.Abstractions.Kernel.Events;

#endregion

namespace Hyre.Modules.Jobs.Core.Events;

/// <summary>
///   Represents the event that occurs when a candidate is created.
/// </summary>
public sealed record CandidateCreatedEvent : DomainEvent;