using System;
using System.Collections.Generic;
using System.Text;

namespace Lykke.Job.NeoClaimTransactionsExecutor.Services
{
    public class ClaimGasStarterSettings
    {
        public string NeoAssetId { get; set; }

        public string GasAssetId { get; set; }

        public string ClaimTriggerCronExpression { get; set; }

        public string NeoHotWalletAddress { get; set; }


    }
}
