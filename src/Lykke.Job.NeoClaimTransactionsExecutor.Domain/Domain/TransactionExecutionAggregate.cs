using System;

namespace Lykke.Job.NeoClaimTransactionsExecutor.Domain.Domain
{
    public class TransactionExecutionAggregate
    {
        private TransactionExecutionAggregate(
            string version,
            Guid transactionId, 
            string address, 
            string neoAssetId,
            string gasAssetId,
            string neoBlockchainType, 
            string neoBlockchainAssetId,
            string gasBlockchainType,
            string gasBlockchainAssetId,
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
            DateTime? claimableGasNotAvailableReportedAt,
            DateTime? transactionSignedAt, 
            DateTime? transactionBroadcastedAt, 
            DateTime? transactionExecutedAt,
            DateTime? transactionClearedAt,
            DateTime? lockReleasedAt)
        {
            TransactionId = transactionId;
            Address = address;
            NeoAssetId = neoAssetId;
            GasAssetId = gasAssetId;
            NeoBlockchainType = neoBlockchainType;
            NeoBlockchainAssetId = neoBlockchainAssetId;
            GasBlockchainType = gasBlockchainType;
            GasBlockchainAssetId = gasBlockchainAssetId;
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
            ClaimableGasNotAvailableReportedAt = claimableGasNotAvailableReportedAt;
            TransactionSignedAt = transactionSignedAt;
            TransactionBroadcastedAt = transactionBroadcastedAt;
            TransactionExecutedAt = transactionExecutedAt;
            TransactionClearedAt = transactionClearedAt;
            LockReleasedAt = lockReleasedAt;
            Version = version;
        }

        public string Version { get; }

        public Guid TransactionId { get; }

        public string Address { get; }

        public string NeoAssetId { get; }

        public string GasAssetId { get; }

        public string NeoBlockchainType { get; private set; }

        public string NeoBlockchainAssetId { get; private set; }

        public string GasBlockchainType { get; private set; }

        public string GasBlockchainAssetId { get; private set; }

        public string UnsignedTransactionContext { get; private set; }

        public decimal? ClaimedGas { get; private set; }

        public decimal? AllGas { get; private set; }

        public string SignedTransactionContext { get; private set; }

        public string TransactionHash { get; private set; }

        public long? BroadcastingBlock { get; private set; }

        public DateTime? LockAcquiredAt { get; private set; }
        public bool LockAcquired => LockAcquiredAt != null;

        public DateTime? LockRejectedAt { get; private set; }
        public bool LockRejected => LockRejectedAt != null;

        public DateTime? AssetInfoRetrievedAt { get; private set; }
        public bool AssetInfoRetrieved => AssetInfoRetrievedAt != null;

        public DateTime? ClaimableGasNotAvailableReportedAt { get; private set; }
        public bool ClaimableGasNotAvailable => ClaimableGasNotAvailableReportedAt != null;

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
            return new TransactionExecutionAggregate(
                version: "*",
                transactionId: transactionId, 
                address: address,
                neoAssetId: neoAssetId,
                gasAssetId: gasAssetId,
                neoBlockchainType: null,
                neoBlockchainAssetId: null,
                gasBlockchainType: null,
                gasBlockchainAssetId: null,
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
                claimableGasNotAvailableReportedAt: null,
                transactionSignedAt:null,
                transactionBroadcastedAt: null, 
                transactionExecutedAt: null,
                transactionClearedAt: null,
                lockReleasedAt: null);
        }

        public static TransactionExecutionAggregate Restore(
            string version,
            Guid transactionId,
            string address,
            string neoAssetId,
            string gasAssetId,
            string neoBlockchainType,
            string neoBlockchainAssetId,
            string gasBlockchainType,
            string gasBlockchainAssetId,
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
            DateTime? claimableGasNotAvailableReportedAt,
            DateTime? transactionSignedAt,
            DateTime? transactionBroadcastedAt,
            DateTime? transactionExecutedAt,
            DateTime? transactionClearedAt,
            DateTime? lockReleasedAt)
        {
            return new TransactionExecutionAggregate(
                version: version,
                transactionId: transactionId,
                address: address,
                neoAssetId: neoAssetId,
                gasAssetId: gasAssetId,
                neoBlockchainType: neoBlockchainType,
                neoBlockchainAssetId: neoBlockchainAssetId,
                gasBlockchainType: gasBlockchainType,
                gasBlockchainAssetId: gasBlockchainAssetId,
                unsignedTransactionContext: unsignedTransactionContext,
                claimedGas: claimedGas,
                allGas: allGas,
                signedTransactionContext: signedTransactionContext,
                transactionHash: transactionHash,
                broadcastingBlock: broadcastingBlock,
                lockAcquiredAt: lockAcquiredAt,
                lockRejectedAt: lockRejectedAt,
                assetInfoRetrievedAt: assetInfoRetrievedAt,
                transactionBuiltAt: transactionBuiltAt,
                claimableGasNotAvailableReportedAt: claimableGasNotAvailableReportedAt,
                transactionSignedAt: transactionSignedAt,
                transactionBroadcastedAt: transactionBroadcastedAt,
                transactionExecutedAt: transactionExecutedAt,
                transactionClearedAt: transactionClearedAt,
                lockReleasedAt: lockReleasedAt);
        }

        public void OnLockAcquired(DateTime time)
        {
            if (!LockAcquired)
            {
                LockAcquiredAt = time;
            }
        }
        public void OnLockRejected(DateTime time)
        {
            if (!LockRejected)
            {
                LockRejectedAt = time;
            }
        }

        public void OnAssetInfoRetrieved(DateTime time, 
            string neoBlockchainAssetId,
            string neoBlockchainType,
            string gasBlockchainAssetId,
            string gasBlockchainType)
        {
            if (!LockAcquired)
            {
                throw new ArgumentException($"Invalid switch at {nameof(OnAssetInfoRetrieved)} for {TransactionId}");
            }

            if (!AssetInfoRetrieved)
            {
                AssetInfoRetrievedAt = time;

                NeoBlockchainAssetId = neoBlockchainAssetId;
                NeoBlockchainType = neoBlockchainType;

                GasBlockchainAssetId = gasBlockchainAssetId;
                GasBlockchainType = gasBlockchainType;
            }
        }


        public void OnClaimableGasNotAvailable(DateTime time)
        {
            if (!AssetInfoRetrieved || TransactionBuilt)
            {
                throw new ArgumentException($"Invalid switch at {nameof(OnTransactionBuilt)} for {TransactionId}");
            }

            if (!ClaimableGasNotAvailable)
            {
                ClaimableGasNotAvailableReportedAt = time;
            }
        }

        public void OnTransactionBuilt(DateTime time, string unsignedTransactionContext, decimal allGas, decimal claimedGas)
        {
            if (!AssetInfoRetrieved || ClaimableGasNotAvailable)
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
            if (!TransactionBuilt || ClaimableGasNotAvailable)
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
            if (!TransactionSigned || ClaimableGasNotAvailable)
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
            if (!TransactionCleared && !ClaimableGasNotAvailable)
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
