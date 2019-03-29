﻿using System;

namespace Lykke.Job.NeoClaimTransactionsExecutor.Domain.Domain
{
    public class TransactionExecutionAggregate
    {
        private TransactionExecutionAggregate(Guid transactionId, 
            string address, 
            string assetId, 
            string blockchainIntegrationLayerId, 
            string blockchainIntegrationLayerAssetId,
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
            AssetId = assetId;
            BlockchainIntegrationLayerId = blockchainIntegrationLayerId;
            BlockchainIntegrationLayerAssetId = blockchainIntegrationLayerAssetId;
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

        public string AssetId { get; }

        public string BlockchainIntegrationLayerId { get; private set; }

        public string BlockchainIntegrationLayerAssetId { get; private set; }

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

        public static TransactionExecutionAggregate StartNew(Guid transactionId,
            string address,
            string assetId)
        {
            return new TransactionExecutionAggregate(transactionId: transactionId, 
                address: address,
                assetId: assetId,
                blockchainIntegrationLayerId: null,
                blockchainIntegrationLayerAssetId: null, 
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

        public void OnAssetInfoRetrieved(DateTime time, string blockchainIntegrationLayerAssetId, string blockchainIntegrationLayerId)
        {
            if (!LockAcquired)
            {
                throw new ArgumentException("In future state");
            }

            if (!AssetInfoRetrieved)
            {
                AssetInfoRetrievedAt = time;

                BlockchainIntegrationLayerAssetId = blockchainIntegrationLayerAssetId;
                BlockchainIntegrationLayerId = blockchainIntegrationLayerId;
            }
        }


        public void OnTransactionBuilt(DateTime time, string unsignedTransactionContext, decimal allGas, decimal claimedGas)
        {
            if (!AssetInfoRetrieved)
            {
                throw new ArgumentException("In future state");
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
                throw new ArgumentException("In future state");
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
                throw new ArgumentException("In future state");
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
                throw new ArgumentException("In future state");
            }

            if (!TransactionExecuted)
            {
                TransactionExecutedAt = time;
                TransactionHash = transactionHash;
                BroadcastingBlock = block;
            }
        }
    }
}
