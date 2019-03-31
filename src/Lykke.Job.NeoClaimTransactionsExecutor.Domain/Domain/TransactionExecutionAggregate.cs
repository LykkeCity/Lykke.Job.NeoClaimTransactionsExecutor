using System;

namespace Lykke.Job.NeoClaimTransactionsExecutor.Domain.Domain
{
    public class TransactionExecutionAggregate
    {
        private TransactionExecutionAggregate(Guid transactionId, 
            string address, 
            string neoAssetId,
            string gasAssetId,
            string neoBlockchainIntegrationLayerId, 
            string neoBlockchainIntegrationLayerAssetId,
            string gasBlockchainIntegrationLayerId,
            string gasBlockchainIntegrationLayerAssetId,
            string unsignedTransactionContext, 
            decimal? claimedGas, 
            decimal? allGas, 
            string signedTransactionContext, 
            string transactionHash,
            long? broadcastingBlock,
            DateTime? lockAcquiredAt,
            DateTime? lockRejectedAt, 
            DateTime? assetInfoRetrievedAt,
            DateTime? transactionBuiltAt, 
            DateTime? transactionSignedAt, 
            DateTime? transactionBroadcastedAt, 
            DateTime? transactionExecutedAt)
        {
            TransactionId = transactionId;
            Address = address;
            NeoAssetId = neoAssetId;
            GasAssetId = gasAssetId;
            NeoBlockchainIntegrationLayerId = neoBlockchainIntegrationLayerId;
            NeoBlockchainIntegrationLayerAssetId = neoBlockchainIntegrationLayerAssetId;
            GasBlockchainIntegrationLayerId = gasBlockchainIntegrationLayerId;
            GasBlockchainIntegrationLayerAssetId = gasBlockchainIntegrationLayerAssetId;
            UnsignedTransactionContext = unsignedTransactionContext;
            ClaimedGas = claimedGas;
            AllGas = allGas;
            SignedTransactionContext = signedTransactionContext;
            TransactionHash = transactionHash;
            BroadcastingBlock = broadcastingBlock;
            LockAcquiredAt = lockAcquiredAt;
            LockRejectedAt = lockRejectedAt;
            AssetInfoRetrievedAt = assetInfoRetrievedAt;
            TransactionBuiltAt = transactionBuiltAt;
            TransactionSignedAt = transactionSignedAt;
            TransactionBroadcastedAt = transactionBroadcastedAt;
            TransactionExecutedAt = transactionExecutedAt;
        }

        public Guid TransactionId { get; }

        public string Address { get; }

        public string NeoAssetId { get; }

        public string GasAssetId { get; }

        public string NeoBlockchainIntegrationLayerId { get; private set; }

        public string NeoBlockchainIntegrationLayerAssetId { get; private set; }

        public string GasBlockchainIntegrationLayerId { get; private set; }

        public string GasBlockchainIntegrationLayerAssetId { get; private set; }

        public string UnsignedTransactionContext { get; private set; }

        public decimal? ClaimedGas { get; private set; }

        public decimal? AllGas { get; private set; }

        public string SignedTransactionContext { get; private set; }

        public string TransactionHash { get; private set; }

        public long? BroadcastingBlock { get; private set; }

        public DateTime? LockAcquiredAt { get; private set; }
        public bool LockAcquired => LockRejectedAt != null;

        public DateTime? LockRejectedAt { get; private set; }
        public bool LockRejected => LockRejectedAt != null;

        public DateTime? AssetInfoRetrievedAt { get; private set; }
        public bool AssetInfoRetrieved => AssetInfoRetrievedAt != null;

        public DateTime? TransactionBuiltAt { get; private set; }
        public bool TransactionBuilt => TransactionBuiltAt != null;

        public DateTime? TransactionSignedAt { get; private set; }
        public bool TransactionSigned => TransactionSignedAt != null;

        public DateTime? TransactionBroadcastedAt { get; private set; }
        public bool TransactionBroadcasted => TransactionBroadcastedAt != null;

        public DateTime? TransactionExecutedAt { get; private set; }
        public bool TransactionExecuted => TransactionExecutedAt != null;

        public DateTime? TransactionClearedAt { get; private set; }
        public bool TransactionCleared => TransactionClearedAt != null;

        public DateTime? LockReleasedAt { get; private set; }
        public bool LockReleased => LockReleasedAt != null;

        public static TransactionExecutionAggregate StartNew(Guid transactionId,
            string address,
            string neoAssetId,
            string gasAssetId)
        {
            return new TransactionExecutionAggregate(transactionId: transactionId, 
                address: address,
                neoAssetId: neoAssetId,
                gasAssetId: gasAssetId,
                neoBlockchainIntegrationLayerId: null,
                neoBlockchainIntegrationLayerAssetId: null,
                gasBlockchainIntegrationLayerId: null,
                gasBlockchainIntegrationLayerAssetId: null,
                unsignedTransactionContext: null,
                claimedGas: null,
                allGas: null, 
                signedTransactionContext: null, 
                transactionHash: null, 
                broadcastingBlock: null, 
                lockAcquiredAt: null,
                lockRejectedAt: null, 
                assetInfoRetrievedAt: null,
                transactionBuiltAt: null, 
                transactionSignedAt:null,
                transactionBroadcastedAt: null, 
                transactionExecutedAt: null);
        }

        public void OnLockAcquired(DateTime time)
        {
            if (LockRejected)
            {
                throw new ArgumentException($"Invalid switch at {nameof(OnLockAcquired)} for {TransactionId}");
            }

            if (!LockAcquired)
            {
                LockAcquiredAt = time;
            }
        }
        public void OnLockRejected(DateTime time)
        {
            if (LockAcquired)
            {
                throw new ArgumentException($"Invalid switch at {nameof(OnLockRejected)} for {TransactionId}");
            }

            if (!LockRejected)
            {
                LockRejectedAt = time;
            }
        }

        public void OnAssetInfoRetrieved(DateTime time, 
            string neoBlockchainIntegrationLayerAssetId,
            string neoBlockchainIntegrationLayerId,
            string gasBlockchainIntegrationLayerAssetId,
            string gasBlockchainIntegrationLayerId)
        {
            if (!LockAcquired)
            {
                throw new ArgumentException($"Invalid switch at {nameof(OnAssetInfoRetrieved)} for {TransactionId}");
            }

            if (!AssetInfoRetrieved)
            {
                AssetInfoRetrievedAt = time;

                NeoBlockchainIntegrationLayerAssetId = neoBlockchainIntegrationLayerAssetId;
                NeoBlockchainIntegrationLayerId = neoBlockchainIntegrationLayerId;

                GasBlockchainIntegrationLayerAssetId = gasBlockchainIntegrationLayerAssetId;
                GasBlockchainIntegrationLayerId = gasBlockchainIntegrationLayerId;
            }
        }


        public void OnTransactionBuilt(DateTime time, string unsignedTransactionContext, decimal allGas, decimal claimedGas)
        {
            if (!AssetInfoRetrieved)
            {
                throw new ArgumentException($"Invalid switch at {nameof(OnTransactionBuilt)} for {TransactionId}");
            }

            if (!TransactionBuilt)
            {
                TransactionBuiltAt = time;

                UnsignedTransactionContext = unsignedTransactionContext;
                AllGas = allGas;
                ClaimedGas = claimedGas;
            }
        }


        public void OnTransactionSigned(DateTime time, string signedTransactionContext)
        {
            if (!TransactionBuilt)
            {
                throw new ArgumentException($"Invalid switch at {nameof(OnTransactionSigned)} for {TransactionId}");
            }

            if (!TransactionSigned)
            {
                TransactionSignedAt = time;

                SignedTransactionContext = signedTransactionContext;
            }
        }

        public void OnTransactionBroadcasted(DateTime time)
        {
            if (!TransactionSigned)
            {
                throw new ArgumentException($"Invalid switch at {nameof(OnTransactionBroadcasted)} for {TransactionId}");
            }

            if (!TransactionBroadcasted)
            {
                TransactionBroadcastedAt = time;
            }
        }

        public void OnTransactionExecuted(DateTime time, string transactionHash, long block)
        {
            if (!TransactionBroadcasted)
            {
                throw new ArgumentException($"Invalid switch at {nameof(OnTransactionExecuted)} for {TransactionId}");
            }

            if (!TransactionExecuted)
            {
                TransactionExecutedAt = time;
                TransactionHash = transactionHash;
                BroadcastingBlock = block;
            }
        }

        public void OnTransactionCleared(DateTime time)
        {
            if (!TransactionExecuted)
            {
                throw new ArgumentException($"Invalid switch at {nameof(OnTransactionCleared)} for {TransactionId}");
            }

            if (!TransactionCleared)
            {
                TransactionClearedAt = time;
            }
        }

        public void OnLockReleased(DateTime time)
        {
            if (!TransactionCleared)
            {
                throw new ArgumentException($"Invalid switch at {nameof(OnLockReleased)} for {TransactionId}");
            }

            if (!LockReleased)
            {
                LockReleasedAt = time;
            }
        }
    }
}
