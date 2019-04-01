using System;
using JetBrains.Annotations;
using Lykke.AzureStorage.Tables;
using Lykke.Job.NeoClaimTransactionsExecutor.Domain.Domain;

namespace Lykke.Job.NeoClaimTransactionsExecutor.AzureRepositories.TransactionExecution
{
    internal class TransactionExecutionEntity:AzureTableEntity
    {
        public Guid TransactionId { get; set; }

        public string Address { get; set; }

        public string NeoAssetId { get; set; }

        public string GasAssetId { get; set; }

        public string NeoBlockchainIntegrationLayerId { get; set; }

        public string NeoBlockchainIntegrationLayerAssetId { get; set; }

        public string GasBlockchainIntegrationLayerId { get;  set; }

        public string GasBlockchainIntegrationLayerAssetId { get; set; }
        
        public decimal? ClaimedGas { get; set; }

        public decimal? AllGas { get; set; }

        public string TransactionHash { get; set; }

        public long? BroadcastingBlock { get; set; }

        public DateTime? LockAcquiredAt { get; set; }

        public DateTime? LockRejectedAt { get; set; }

        public DateTime? AssetInfoRetrievedAt { get; set; }

        public DateTime? ClaimableGasNotAvailableReportedAt { get; set; }
        public DateTime? TransactionBuiltAt { get; set; }

        public DateTime? TransactionSignedAt { get; set; }

        public DateTime? TransactionBroadcastedAt { get; set; }

        public DateTime? TransactionExecutedAt { get; set; }

        public DateTime? TransactionClearedAt { get; set; }

        public DateTime? LockReleasedAt { get; set; }

        public static TransactionExecutionEntity FromDomain(TransactionExecutionAggregate aggregate)
        {
            return new TransactionExecutionEntity
            {
                TransactionId = aggregate.TransactionId,
                Address = aggregate.Address,
                ClaimedGas = aggregate.ClaimedGas,
                GasAssetId = aggregate.GasAssetId,
                LockRejectedAt = aggregate.LockRejectedAt,
                LockAcquiredAt = aggregate.LockAcquiredAt,
                BroadcastingBlock = aggregate.BroadcastingBlock,
                AllGas = aggregate.AllGas,
                LockReleasedAt = aggregate.LockReleasedAt,
                TransactionExecutedAt = aggregate.TransactionExecutedAt,
                AssetInfoRetrievedAt = aggregate.AssetInfoRetrievedAt,
                GasBlockchainIntegrationLayerAssetId = aggregate.GasBlockchainIntegrationLayerAssetId,
                GasBlockchainIntegrationLayerId = aggregate.GasBlockchainIntegrationLayerId,
                NeoAssetId = aggregate.NeoAssetId,
                NeoBlockchainIntegrationLayerAssetId = aggregate.NeoBlockchainIntegrationLayerAssetId,
                NeoBlockchainIntegrationLayerId = aggregate.NeoBlockchainIntegrationLayerId,
                TransactionBroadcastedAt = aggregate.TransactionBroadcastedAt,
                TransactionBuiltAt = aggregate.TransactionBuiltAt,
                ClaimableGasNotAvailableReportedAt = aggregate.ClaimableGasNotAvailableReportedAt,
                TransactionClearedAt = aggregate.TransactionClearedAt,
                TransactionHash = aggregate.TransactionHash,
                TransactionSignedAt = aggregate.TransactionSignedAt,
                ETag = aggregate.Version,
                PartitionKey = AggregateKeysBuilder.BuildPartitionKey(aggregate.TransactionId),
                RowKey = AggregateKeysBuilder.BuildRowKey()
            };
        }

        public TransactionExecutionAggregate ToDomain([CanBeNull] TransactionExecutionBlobEntity blobData)
        {
            return TransactionExecutionAggregate.Restore(
                version: ETag,
                transactionId: TransactionId,
                address: Address,
                neoAssetId: NeoAssetId,
                gasAssetId: GasAssetId,
                neoBlockchainIntegrationLayerId: NeoBlockchainIntegrationLayerId,
                neoBlockchainIntegrationLayerAssetId: NeoBlockchainIntegrationLayerAssetId,
                gasBlockchainIntegrationLayerId: GasBlockchainIntegrationLayerId,
                gasBlockchainIntegrationLayerAssetId: GasBlockchainIntegrationLayerAssetId,
                unsignedTransactionContext: blobData?.UnsignedTransactionContext,
                claimedGas: ClaimedGas,
                allGas: AllGas,
                signedTransactionContext: blobData?.SignedTransactionContext,
                transactionHash: TransactionHash,
                broadcastingBlock: BroadcastingBlock,
                lockAcquiredAt: LockAcquiredAt,
                lockRejectedAt: LockRejectedAt,
                assetInfoRetrievedAt: AssetInfoRetrievedAt,
                transactionBuiltAt: TransactionBuiltAt,
                claimableGasNotAvailableReportedAt: ClaimableGasNotAvailableReportedAt,
                transactionSignedAt: TransactionSignedAt,
                transactionBroadcastedAt: TransactionBroadcastedAt,
                transactionExecutedAt: TransactionExecutedAt);
        }

    }
}
