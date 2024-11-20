namespace App;

internal class PayoutResult
{
    public List<Transaction> ReservedTransactions { get; set; } = [];
    public List<Transaction> PayoutTransactions { get; set; } = [];
}