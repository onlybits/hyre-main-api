﻿// Licensed to Hyre under one or more agreements.
// Hyre [www.hyre.com.br] licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#region

using MediatR;

#endregion

namespace Hyre.Modules.Jobs.Application.UseCases.JobOpportunities.Update;

/// <summary>
///   This is the use case contract to update a job opportunity.
/// </summary>
internal interface IUpdateJobOpportunityUseCase : IRequestHandler<UpdateJobOpportunityRequest>;