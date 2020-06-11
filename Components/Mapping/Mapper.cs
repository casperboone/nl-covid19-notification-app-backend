﻿// Copyright © 2020 De Staat der Nederlanden, Ministerie van Volksgezondheid, Welzijn en Sport.
// Licensed under the EUROPEAN UNION PUBLIC LICENCE v. 1.2
// SPDX-License-Identifier: EUPL-1.2

using System.Linq;
using System.Text;
using Newtonsoft.Json;
using NL.Rijksoverheid.ExposureNotification.BackEnd.Components.ExposureKeySetsEngine;
using NL.Rijksoverheid.ExposureNotification.BackEnd.Components.ResourceBundle;
using NL.Rijksoverheid.ExposureNotification.BackEnd.Components.RiskCalculationConfig;
using NL.Rijksoverheid.ExposureNotification.BackEnd.Components.Workflows.KeysFirstWorkflow.EscrowTeks;

namespace NL.Rijksoverheid.ExposureNotification.BackEnd.Components.Mapping
{
    public static class Mapper
    {
        public static RiskCalculationConfigResponse ToResponse(this RiskCalculationConfigContent e)
            => new RiskCalculationConfigResponse
            {
                MinimumRiskScore = e.MinimumRiskScore,
                DaysSinceLastExposureScores​ = e.DaysSinceLastExposureScores​,
                AttenuationScores​ = e.AttenuationScores​,
                DurationAtAttenuationThresholds​ = e.DurationAtAttenuationThresholds​,
                DurationScores = e.DurationScores,
                TransmissionRiskScores​ = e.TransmissionRiskScores​
            };


        public static RiskCalculationContentEntity ToEntity(this RiskCalculationConfigArgs args)
        {
            var content = new RiskCalculationConfigContent
            {
                MinimumRiskScore = args.MinimumRiskScore,
                DaysSinceLastExposureScores​ = args.DaysSinceLastExposureScores​,
                AttenuationScores​ = args.AttenuationScores​,
                DurationAtAttenuationThresholds​ = args.DurationAtAttenuationThresholds​,
                DurationScores = args.DurationScores,
                TransmissionRiskScores​ = args.TransmissionRiskScores​
            };

            return new RiskCalculationContentEntity
            {
                Release = args.Release,
                Content = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(content))
            };
        }

        public static KeysFirstTeksWorkflowEntity ToEntity(this KeysFirstEscrowArgs args)
        {
            var content = args.Items.Select(x =>
                new WorkflowKeyContent
                {
                    KeyData = x.KeyData,
                    TransmissionRiskLevel = x.TransmissionRiskLevel,
                    RollingPeriod = x.RollingPeriod,
                    RollingStartNumber = x.RollingStartNumber
                }).ToArray();

            return new KeysFirstTeksWorkflowEntity
            {
                AuthorisationToken = args.Token,
                TekContent = JsonConvert.SerializeObject(content) //TODO deserialize had better not screw with this cos array...
            };
        }

        public static ResourceBundleContentEntity ToEntity(this ResourceBundleArgs args)
        {
            var content = new ResourceBundleEntityContent
            {
                Text = args.Text,
                IsolationPeriodDays = args.IsolationPeriodDays,
                ObservedTemporaryExposureKeyRetentionDays = args.ObservedTemporaryExposureKeyRetentionDays,
                TemporaryExposureKeyRetentionDays = args.TemporaryExposureKeyRetentionDays,
            };

            return new ResourceBundleContentEntity
            {
                Release = args.Release,
                Content = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(content))
            };
        }

        ///// <summary>
        ///// TODO decide whether we really need to Json version but it is useful to see in a readable form what the content of the zipped AG content is too
        ///// </summary>
        ///// <param name="output"></param>
        ///// <returns></returns>
        //public static ExposureKeySetConfigEntity ToEntity(this ExposureKeySetOutput output)
        //{
        //    var jsonContent = JsonConvert.SerializeObject(output.DebugContentJson);
        //    return new ExposureKeySetConfigEntity
        //    {
        //        Release = output.Created,
        //        CreatingJobQualifier = output.CreatingJobQualifier,
        //        CreatingJobName = output.CreatingJobName,
        //        JsonContent = jsonContent,
        //        AgContent = output.AgContent
        //    };
        //}
    }
}