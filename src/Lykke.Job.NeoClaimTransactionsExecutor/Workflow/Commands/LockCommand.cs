﻿using System;

namespace Lykke.Job.NeoClaimTransactionsExecutor.Workflow.Commands
{
    public class LockCommand
    {
        public Guid TransactionId { get; set; }
    }
}
