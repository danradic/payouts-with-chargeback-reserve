namespace App;

internal class Program
{
    static void Main(string[] args)
    {
        var transactions = new List<Transaction>
    {
        new() { Amount = 100m, CreatedUtc = DateTime.Parse("2024-11-20 10:00:00") },
        new() { Amount = 200m, CreatedUtc = DateTime.Parse("2024-11-20 11:00:00") },
        new() { Amount = 300m, CreatedUtc = DateTime.Parse("2024-11-20 12:00:00") }
    };

        decimal chargebackReserve = 400m;
        var result = TransactionProcessor.CalculatePayoutWithReserve(transactions, chargebackReserve);

        Console.WriteLine("Reserved Transactions:");
        foreach (var t in result.ReservedTransactions)
            Console.WriteLine($"Amount: {t.Amount}, Created: {t.CreatedUtc}");

        Console.WriteLine("\nPayout Transactions:");
        foreach (var t in result.PayoutTransactions)
            Console.WriteLine($"Amount: {t.Amount}, Created: {t.CreatedUtc}");
    }
}
