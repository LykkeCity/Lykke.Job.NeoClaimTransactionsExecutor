using Autofac;
using Common.Log;
using Lykke.Common.Log;
using Lykke.Job.NeoClaimTransactionsExecutor.Settings.JobSettings;
using Lykke.Service.BlockchainApi.Client;
using Lykke.Service.BlockchainSignFacade.Client;
using Lykke.SettingsReader;

namespace Lykke.Job.NeoClaimTransactionsExecutor.Modules
{
    public class BlockChainModule : Module
    {
        private readonly NeoClaimTransactionsExecutorJobSettings _settings;
        private readonly IReloadingManager<NeoClaimTransactionsExecutorJobSettings> _settingsManager;

        public BlockChainModule(NeoClaimTransactionsExecutorJobSettings settings, IReloadingManager<NeoClaimTransactionsExecutorJobSettings> settingsManager)
        {
            _settings = settings;
            _settingsManager = settingsManager;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(ctx => CreateBlockchainSignFacadeClient(ctx.Resolve<ILogFactory>().CreateLog(this)))
                .As<IBlockchainSignFacadeClient>()
                .SingleInstance();

            builder.Register(ctx => new BlockchainApiClient(ctx.Resolve<ILogFactory>(), _settings.NeoApiUrl))
                .As<IBlockchainApiClient>()
                .SingleInstance();
        }

        private IBlockchainSignFacadeClient CreateBlockchainSignFacadeClient(ILog log)
        {
            return new BlockchainSignFacadeClient
            (
                hostUrl: _settings.SignFacadeUrl,
                apiKey: _settings.SignFacadeApiKey,
                log: log
            );
        }
    }
}
