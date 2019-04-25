using System.Collections.Generic;
using Autofac;
using Autofac.Features.ResolveAnything;
using Lykke.Common.Log;
using Lykke.Cqrs;
using Lykke.Cqrs.Configuration;
using Lykke.Job.NeoClaimTransactionsExecutor.Contract;
using Lykke.Job.NeoClaimTransactionsExecutor.Settings;
using Lykke.Job.NeoClaimTransactionsExecutor.Settings.JobSettings;
using Lykke.Job.NeoClaimTransactionsExecutor.Workflow;
using Lykke.Job.NeoClaimTransactionsExecutor.Workflow.CommandHandlers;
using Lykke.Job.NeoClaimTransactionsExecutor.Workflow.Commands;
using Lykke.Job.NeoClaimTransactionsExecutor.Workflow.Events;
using Lykke.Job.NeoClaimTransactionsExecutor.Workflow.Sagas;
using Lykke.Messaging;
using Lykke.Messaging.RabbitMq;
using Lykke.Messaging.Serialization;
using Lykke.SettingsReader;

namespace Lykke.Job.NeoClaimTransactionsExecutor.Modules
{
    public class CqrsModule : Module
    {
        private readonly NeoClaimTransactionsExecutorJobSettings _settings;

        public CqrsModule(IReloadingManager<AppSettings> settingsManager)
        {
            _settings = settingsManager.CurrentValue.NeoClaimTransactionsExecutorJob;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(context => new AutofacDependencyResolver(context)).As<IDependencyResolver>().SingleInstance();

            builder.Register(c => new RetryDelayProvider(
                    _settings.Cqrs.WaitForTransactionRetryDelay))
                .AsSelf();

            builder.RegisterType<TransactionExecutionSaga>();

            // Command handlers
            builder.RegisterSource(new AnyConcreteTypeNotAlreadyRegisteredSource(t =>
                t.Namespace == typeof(AcquireLockCommandHandler).Namespace));


            builder.Register(CreateEngine)
                .As<ICqrsEngine>()
                .SingleInstance()
                .AutoActivate();
        }

        private CqrsEngine CreateEngine(IComponentContext ctx)
        {
            var rabbitMqSettings = new RabbitMQ.Client.ConnectionFactory
            {
                Uri = _settings.Cqrs.RabbitConnectionString
            };

            var rabbitMqEndpoint = rabbitMqSettings.Endpoint.ToString();

            var logFactory = ctx.Resolve<ILogFactory>();

            var messagingEngine = new MessagingEngine(
                logFactory,
                new TransportResolver(new Dictionary<string, TransportInfo>
                {
                    {
                        "RabbitMq",
                        new TransportInfo(rabbitMqEndpoint, rabbitMqSettings.UserName,
                            rabbitMqSettings.Password, "None", "RabbitMq")
                    }
                }),
                new RabbitMqTransportFactory(logFactory));

            var defaultRetryDelay = (long)_settings.Cqrs.RetryDelay.TotalMilliseconds;

            const string commandsPipeline = "commands";
            const string defaultRoute = "self";

            return new CqrsEngine
            (
                logFactory,
                ctx.Resolve<IDependencyResolver>(),
                messagingEngine,
                new DefaultEndpointProvider(),
                true,
                Register.DefaultEndpointResolver
                (
                    new RabbitMqConventionEndpointResolver
                    (
                        "RabbitMq",
                        SerializationFormat.MessagePack,
                        environment: "lykke"
                    )
                ),

                Register.BoundedContext(NeoClaimTransactionsExecutorBoundedContext.Name)
                    .FailedCommandRetryDelay(defaultRetryDelay)

                    .ListeningCommands(typeof(StartTransactionCommand))
                    .On(defaultRoute)
                    .WithLoopback()
                    .WithCommandsHandler<StartTransactionCommandHandler>()
                    .PublishingEvents(typeof(TransactionStartedtEvent))
                    .With(commandsPipeline)

                    .ListeningCommands(typeof(AcquireLockCommand))
                    .On(defaultRoute)
                    .WithCommandsHandler<AcquireLockCommandHandler>()
                    .PublishingEvents(typeof(LockAcquiredEvent), typeof(LockRejectedEvent))
                    .With(commandsPipeline)

                    .ListeningCommands(typeof(RetrieveAssetInfoCommand))
                    .On(defaultRoute)
                    .WithCommandsHandler(typeof(RetrieveAssetInfoCommandHandler))
                    .PublishingEvents(typeof(AssetInfoRetrievedEvent))
                    .With(commandsPipeline)

                    .ListeningCommands(typeof(BuildTransactionCommand))
                    .On(defaultRoute)
                    .WithCommandsHandler<BuildTransactionCommandHandler>()
                    .PublishingEvents(typeof(TransactionBuiltEvent), typeof(ClaimbaleGasNotAvailiableEvent))
                    .With(commandsPipeline)

                    .ListeningCommands(typeof(SignTransactionCommand))
                    .On(defaultRoute)
                    .WithCommandsHandler<SignTransactionCommandHandler>()
                    .PublishingEvents(typeof(TransactionSignedEvent))
                    .With(commandsPipeline)

                    .ListeningCommands(typeof(BroadcastTransactionCommand))
                    .On(defaultRoute)
                    .WithCommandsHandler<BroadcastTransactionCommandHandler>()
                    .PublishingEvents(
                        typeof(TransactionBroadcastedEvent))
                    .With(commandsPipeline)

                    .ListeningCommands(typeof(WaitForTransactionEndingCommand))
                    .On(defaultRoute)
                    .WithCommandsHandler<WaitForTransactionEndingCommandHandler>()
                    .PublishingEvents(typeof(GasClaimTransactionExecutedEvent))
                    .With(commandsPipeline)

                    .ListeningCommands(typeof(ClearTransactionCommand))
                    .On(defaultRoute)
                    .WithCommandsHandler<ClearTransactionCommandHandler>()
                    .PublishingEvents(typeof(TransactionClearedEvent))
                    .With(commandsPipeline)

                    .ListeningCommands(typeof(ReleaseLockCommand))
                    .On(defaultRoute)
                    .WithCommandsHandler<ReleaseLockCommandHandler>()
                    .PublishingEvents(typeof(LockReleasedEvent))
                    .With(commandsPipeline)

                    .ProcessingOptions(defaultRoute).MultiThreaded(8).QueueCapacity(1024),


                Register.Saga<TransactionExecutionSaga>($"{NeoClaimTransactionsExecutorBoundedContext.Name}.saga")
                    .ListeningEvents(typeof(TransactionStartedtEvent))
                    .From(NeoClaimTransactionsExecutorBoundedContext.Name)
                    .On(defaultRoute)
                    .PublishingCommands(typeof(AcquireLockCommand))
                    .To(NeoClaimTransactionsExecutorBoundedContext.Name)
                    .With(commandsPipeline)

                    .ListeningEvents(typeof(LockAcquiredEvent))
                    .From(NeoClaimTransactionsExecutorBoundedContext.Name)
                    .On(defaultRoute)
                    .PublishingCommands(typeof(RetrieveAssetInfoCommand))
                    .To(NeoClaimTransactionsExecutorBoundedContext.Name)
                    .With(commandsPipeline)

                    .ListeningEvents(typeof(LockRejectedEvent))
                    .From(NeoClaimTransactionsExecutorBoundedContext.Name)
                    .On(defaultRoute)

                    .ListeningEvents(typeof(AssetInfoRetrievedEvent))
                    .From(NeoClaimTransactionsExecutorBoundedContext.Name)
                    .On(defaultRoute)
                    .PublishingCommands(typeof(BuildTransactionCommand))
                    .To(NeoClaimTransactionsExecutorBoundedContext.Name)
                    .With(commandsPipeline)

                    .ListeningEvents(typeof(TransactionBuiltEvent))
                    .From(NeoClaimTransactionsExecutorBoundedContext.Name)
                    .On(defaultRoute)
                    .PublishingCommands(typeof(SignTransactionCommand))
                    .To(NeoClaimTransactionsExecutorBoundedContext.Name)
                    .With(commandsPipeline)

                    .ListeningEvents(typeof(ClaimbaleGasNotAvailiableEvent))
                    .From(NeoClaimTransactionsExecutorBoundedContext.Name)
                    .On(defaultRoute)
                    .PublishingCommands(typeof(ReleaseLockCommand))
                    .To(NeoClaimTransactionsExecutorBoundedContext.Name)
                    .With(commandsPipeline)

                    .ListeningEvents(typeof(TransactionSignedEvent))
                    .From(NeoClaimTransactionsExecutorBoundedContext.Name)
                    .On(defaultRoute)
                    .PublishingCommands(typeof(BroadcastTransactionCommand))
                    .To(NeoClaimTransactionsExecutorBoundedContext.Name)
                    .With(commandsPipeline)

                    .ListeningEvents(typeof(TransactionBroadcastedEvent))
                    .From(NeoClaimTransactionsExecutorBoundedContext.Name)
                    .On(defaultRoute)
                    .PublishingCommands(typeof(WaitForTransactionEndingCommand))
                    .To(NeoClaimTransactionsExecutorBoundedContext.Name)
                    .With(commandsPipeline)

                    .ListeningEvents(typeof(GasClaimTransactionExecutedEvent))
                    .From(NeoClaimTransactionsExecutorBoundedContext.Name)
                    .On(defaultRoute)
                    .PublishingCommands(typeof(ClearTransactionCommand))
                    .To(NeoClaimTransactionsExecutorBoundedContext.Name)
                    .With(commandsPipeline)

                    .ListeningEvents(typeof(TransactionClearedEvent))
                    .From(NeoClaimTransactionsExecutorBoundedContext.Name)
                    .On(defaultRoute)
                    .PublishingCommands(typeof(ReleaseLockCommand))
                    .To(NeoClaimTransactionsExecutorBoundedContext.Name)
                    .With(commandsPipeline)

                    .ListeningEvents(typeof(LockReleasedEvent))
                    .From(NeoClaimTransactionsExecutorBoundedContext.Name)
                    .On(defaultRoute)

                    .ProcessingOptions(defaultRoute).MultiThreaded(8).QueueCapacity(1024)
            );
        }
    }
}
