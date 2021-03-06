﻿// Copyright © 2020 De Staat der Nederlanden, Ministerie van Volksgezondheid, Welzijn en Sport.
// Licensed under the EUROPEAN UNION PUBLIC LICENCE v. 1.2
// SPDX-License-Identifier: EUPL-1.2

namespace NL.Rijksoverheid.ExposureNotification.BackEnd.Components.ProtocolSettings
{
    public interface IGaenContentConfig
    {
        //TODO this is KeysLast Workflow
        int KeysLastSecretLifetimeDays { get; }
        
        int ExposureKeySetLifetimeDays { get; }
        
        /// <summary>
        /// TODO Key count - rename
        /// </summary>
        int ExposureKeySetCapacity { get; }
        double ManifestLifetimeHours { get; set; }
    }
}
