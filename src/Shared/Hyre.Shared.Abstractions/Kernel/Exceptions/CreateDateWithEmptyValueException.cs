// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#region

using Hyre.Shared.Abstractions.Exceptions;
using Hyre.Shared.Abstractions.Kernel.Constants;

#endregion

namespace Hyre.Shared.Abstractions.Kernel.Exceptions;

/// <summary>
///   Exception thrown when the creation date is empty.
/// </summary>
public sealed class CreateDateWithEmptyValueException : DomainException
{
	/// <summary>
	///   Initializes a new instance of the <see cref="CreateDateWithEmptyValueException" /> class.
	/// </summary>
	public CreateDateWithEmptyValueException() : base(EntityErrorMessages.CreateDateWithEmptyValue)
	{
	}
}