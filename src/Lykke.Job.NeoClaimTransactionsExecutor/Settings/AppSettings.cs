using Lykke.Job.NeoClaimTransactionsExecutor.Settings.JobSettings;
using Lykke.Sdk.Settings;

namespace Lykke.Job.NeoClaimTransactionsExecutor.Settings
{
    public class AppSettings : BaseAppSettings
    {
        public NeoClaimTransactionsExecutorJobSettings NeoClaimTransactionsExecutorJob { get; set; }
    }
}
