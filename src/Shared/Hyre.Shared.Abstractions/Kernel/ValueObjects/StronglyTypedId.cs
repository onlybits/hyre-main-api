// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace Hyre.Shared.Abstractions.Kernel.ValueObjects;

/// <summary>
///   This is the base record for all strongly typed identifiers in the system.
/// </summary>
/// <param name="Value">The value of the identifier.</param>
/// <typeparam name="T">The type of the identifier.</typeparam>
public abstract record StronglyTypedId<T>(T Value) : ValueObject;