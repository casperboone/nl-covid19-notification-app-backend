﻿// Copyright © 2020 De Staat der Nederlanden, Ministerie van Volksgezondheid, Welzijn en Sport.
// Licensed under the EUROPEAN UNION PUBLIC LICENCE v. 1.2
// SPDX-License-Identifier: EUPL-1.2

using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NL.Rijksoverheid.ExposureNotification.BackEnd.Components.WebApi;
using NL.Rijksoverheid.ExposureNotification.BackEnd.Components.Workflows.KeysFirstWorkflow.Authorisation;
using NL.Rijksoverheid.ExposureNotification.BackEnd.Components.Workflows.KeysFirstWorkflow.EscrowTeks;

namespace NL.Rijksoverheid.ExposureNotification.BackEnd.ServerStandAlone.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class KeysFirstWorkflowController : ControllerBase
    {
        [HttpPost]
        [Route(EndPointNames.MobileAppApi.KeysFirstWorkflow.Teks)]
        public async Task<IActionResult> PostWorkflow([FromBody]KeysFirstEscrowArgs args, [FromServices]HttpPostKeysFirstEscrowCommand command)
        {
            return await command.Execute(args);
        }

        [HttpPost]
        [Route(EndPointNames.CaregiversPortalApi.KeysFirstWorkflow.Authorise)]
        public async Task<IActionResult> PostActivateTemporaryExposureKey([FromBody]KeysFirstAuthorisationArgs args, [FromServices]HttpPostKeysFirstAuthorisationCommand command)
        {
            return await command.Execute(args);
        }
    }
}