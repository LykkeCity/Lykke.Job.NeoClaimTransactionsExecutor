using Lykke.SettingsReader.Attributes;

namespace Lykke.Job.NeoClaimTransactionsExecutor.Settings.JobSettings
{
    public class DbSettings
    {
        [AzureTableCheck]
        public string LogsConnString { get; set; }

        [AzureTableCheck]
        public string DataConnString { get; set; }

        [MongoCheck]
        public string MongoConnString { get; set; }
    }
}
