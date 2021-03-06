﻿// Copyright © 2020 De Staat der Nederlanden, Ministerie van Volksgezondheid, Welzijn en Sport.
// Licensed under the EUROPEAN UNION PUBLIC LICENCE v. 1.2
// SPDX-License-Identifier: EUPL-1.2

using System;
using System.Linq;
using NL.Rijksoverheid.ExposureNotification.BackEnd.Components.EfDatabase;
using NL.Rijksoverheid.ExposureNotification.BackEnd.Components.EfDatabase.Contexts;
using NL.Rijksoverheid.ExposureNotification.BackEnd.Components.Services;

namespace NL.Rijksoverheid.ExposureNotification.BackEnd.Components.Workflows.KeysLastWorkflow.BackgroundJobs
{
    public class KeysLastPurgeExpiredSecretsDbCommand
    {
        private readonly WorkflowDbContext _DbContextProvider;
        private readonly IUtcDateTimeProvider _DateTimeProvider;
        private readonly IKeysLastWorkflowConfig _KeysLastWorkflowConfig;

        public KeysLastPurgeExpiredSecretsDbCommand(WorkflowDbContext dbContextProvider, IUtcDateTimeProvider dateTimeProvider, IKeysLastWorkflowConfig tokenFirstWorkflowConfig)
        {
            _DbContextProvider = dbContextProvider;
            _DateTimeProvider = dateTimeProvider;
            _KeysLastWorkflowConfig = tokenFirstWorkflowConfig;
        }

        public void Execute()
        {
            var expired = _DateTimeProvider.Now() - TimeSpan.FromDays(_KeysLastWorkflowConfig.SecretLifetimeDays);

            _DbContextProvider.BeginTransaction();

            throw new NotImplementedException();

            //var q = _DbContextProvider.KeysLastWorkflows
            //    .Where(x => x.State == KeysLastWorkflowState.Unauthorised && x.Created < expired);

            //_DbContextProvider.KeysLastWorkflows.RemoveRange(q);

            _DbContextProvider.SaveChanges();
            _DbContextProvider.SaveAndCommit();
        }
    }
}