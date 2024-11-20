# Payout calculation with chargeback reserve

This project is a simple C# program that calculates how to handle payouts while keeping a chargeback reserve. It ensures that enough money is set aside (reserve, aka deposit) to handle chargebacks, and the rest can be paid out.

---

## 📖 How It Works

1. **What is a chargeback reserve?**  
   A chargeback reserve is money set aside to handle disputes or refunds. This means not all money from transactions can be paid out immediately.

2. **How does it work?**
- The **newest transactions** are used first to meet the reserv
- Older transactions are paid out, but only after the reserve is covered
- Transactions are **never split**. If one transaction is needed for the reserve, it is kept whole
- If all transactions are needed for the reserve, no payout is made

---

## 🧮 Examples

### **Example 1**
- **Transactions**:  
  - $100  
  - $200  
  - $300  
- **Reserve**: $400  

**Result**:  
- **Reserve**: $300 (newest) + $100 (next newest) = $400  
- **Payout**: $200  

---

### **Example 2**
- **Transactions**:  
  - $50  
  - $150  
  - $250  
- **Reserve**: $250  

**Result**:  
- **Reserve**: $250  
- **Payout**: $200 ($50 + $150)

---

### **Example 3**
- **Transactions**:  
  - $200  
  - $300  
  - $400  
- **Reserve**: $900  

**Result**:  
- **Reserve**: $200 + $300 + $400 = $900  
- **Payout**: None  

---

## 🚀 How to Use

### 1. **Steps**
   - Clone this project:
     ```bash
     git clone https://github.com/danradic/payouts-with-chargeback-reserve.git
     ```
   - Open it in your editor and run the program.

### 2. **Example Code**
Here’s how to use the program:

```csharp
var transactions = new List<Transaction>
{
    new Transaction { Amount = 100m, CreatedUtc = DateTime.Parse("2024-11-20 10:00:00") },
    new Transaction { Amount = 200m, CreatedUtc = DateTime.Parse("2024-11-20 11:00:00") },
    new Transaction { Amount = 300m, CreatedUtc = DateTime.Parse("2024-11-20 12:00:00") }
};

decimal chargebackReserve = 400m;
var result = TransactionProcessor.CalculatePayoutWithReserve(transactions, chargebackReserve);

Console.WriteLine("Reserved Transactions:");
foreach (var t in result.ReservedTransactions)
    Console.WriteLine($"Amount: {t.Amount}, Created: {t.CreatedUtc}");

Console.WriteLine("\nPayout Transactions:");
foreach (var t in result.PayoutTransactions)
    Console.WriteLine($"Amount: {t.Amount}, Created: {t.CreatedUtc}");
