namespace Lykke.Job.NeoClaimTransactionsExecutor.Services
{
    public class ClaimGasStarterSettings
    {
        public ClaimGasStarterSettings(string neoAssetId, 
            string gasAssetId, 
            string claimTriggerCronExpression, 
            string neoHotWalletAddress)
        {
            NeoAssetId = neoAssetId;
            GasAssetId = gasAssetId;
            ClaimTriggerCronExpression = claimTriggerCronExpression;
            NeoHotWalletAddress = neoHotWalletAddress;
        }

        public string NeoAssetId { get; }

        public string GasAssetId { get; }

        public string ClaimTriggerCronExpression { get; }

        public string NeoHotWalletAddress { get; }
    }
}
