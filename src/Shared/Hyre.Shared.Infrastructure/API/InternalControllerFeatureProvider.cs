// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#region

using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;

#endregion

namespace Hyre.Shared.Infrastructure.API;

/// <summary>
///   This class is responsible for filtering out internal controllers from the application.
/// </summary>
internal sealed class InternalControllerFeatureProvider : ControllerFeatureProvider
{
	/// <summary>
	///   Overrides the default implementation to filter out internal controllers.
	/// </summary>
	/// <param name="typeInfo">The type information of the controller.</param>
	/// <returns> It will return true if the class is a controller. </returns>
	protected override bool IsController(TypeInfo typeInfo) =>
		typeInfo is { IsClass: true, IsAbstract: false, ContainsGenericParameters: false } &&
		!typeInfo.IsDefined(typeof(NonControllerAttribute)) &&
		(typeInfo.Name.EndsWith("Controller", StringComparison.OrdinalIgnoreCase) ||
		 typeInfo.IsDefined(typeof(ControllerAttribute)));
}