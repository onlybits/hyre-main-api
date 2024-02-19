// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace Hyre.Shared.Abstractions.Kernel.Constants;

/// <summary>
///   Error messages for the entity.
/// </summary>
internal abstract class EntityErrorMessages
{
	/// <summary>
	///   Used when the creation date is empty.
	/// </summary>
	public const string CreateDateWithEmptyValue = "A data de criação não pode ser vazia.";

	/// <summary>
	///   Used when the creation date is in the future.
	/// </summary>
	public const string CreateDateInTheFuture = "A data de criação não pode ser no futuro.";
}