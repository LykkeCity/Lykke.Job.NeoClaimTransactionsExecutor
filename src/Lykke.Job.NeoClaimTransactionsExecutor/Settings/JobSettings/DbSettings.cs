using Lykke.SettingsReader.Attributes;

namespace Lykke.Job.NeoClaimTransactionsExecutor.Settings.JobSettings
{
    public class DbSettings
    {
        [AzureTableCheck]
        public string LogsConnString { get; set; }
    }
}
