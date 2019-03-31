using System;
using Lykke.Common.Chaos;

namespace Lykke.Job.NeoClaimTransactionsExecutor.Settings.JobSettings
{
    public class NeoClaimTransactionsExecutorJobSettings
    {
        public DbSettings Db { get; set; }

        public string NeoHotWalletAddress { get; set; }
        
        public string NeoAssetId { get; set; }

        public string GasAssetId { get; set; }

        public TimeSpan PeriodBetweenClaims { get; set; }
        
        public ChaosSettings ChaosKitty { get; set; }

        public string SignFacadeApiKey { get; set; }

        public string SignFacadeUrl { get; set; }

        public CqrsSettings Cqrs { get; set; }

        public string NeoApiUrl { get; set; }

        public string AssetsServiceUrl { get; set; }
    }
}
