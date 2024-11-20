namespace App;

internal class TransactionProcessor
{
    /// <summary>
    /// Calculates payout transactions while keeping a reserve amount
    /// </summary>
    /// <param name="transactions">List of transactions ordered oldest to newest.</param>
    /// <param name="chargebackReserve">The reserve amount to retain.</param>
    /// <returns>PayoutResult containing reserved and payout transactions.</returns>
    public static PayoutResult CalculatePayoutWithReserve(IEnumerable<Transaction> transactions, decimal chargebackReserve)
    {
        // Order transactions from newest to oldest for reserve calculation
        var orderedTransactions = transactions.OrderByDescending(t => t.CreatedUtc).ToList();

        var reservedTransactions = new List<Transaction>();
        var payoutTransactions = new List<Transaction>();
        decimal cumulativeReserve = 0;

        foreach (var transaction in orderedTransactions)
        {
            if (cumulativeReserve < chargebackReserve)
            {
                reservedTransactions.Add(transaction);
                cumulativeReserve += transaction.Amount;
            }
            else
            {
                payoutTransactions.Add(transaction);
            }
        }

        // Return reserved and payout transactions
        return new PayoutResult
        {
            ReservedTransactions = reservedTransactions.OrderByDescending(t => t.CreatedUtc).ToList(),
            PayoutTransactions = payoutTransactions.OrderBy(t => t.CreatedUtc).ToList()
        };
    }
}
