using System;
using System.Collections.Generic;

namespace Question1
{
    // Record for Transaction
    public record Transaction(int Id, DateTime Date, decimal Amount, string Category);

    // Interface for transaction processing
    public interface ITransactionProcessor
    {
        void Process(Transaction transaction);
    }

    // Concrete implementations of ITransactionProcessor
    public class BankTransferProcessor : ITransactionProcessor
    {
        public void Process(Transaction transaction)
        {
            Console.WriteLine($"Bank Transfer: Processing ${transaction.Amount} for {transaction.Category}");
        }
    }

    public class MobileMoneyProcessor : ITransactionProcessor
    {
        public void Process(Transaction transaction)
        {
            Console.WriteLine($"Mobile Money: Processing ${transaction.Amount} for {transaction.Category}");
        }
    }

    public class CryptoWalletProcessor : ITransactionProcessor
    {
        public void Process(Transaction transaction)
        {
            Console.WriteLine($"Crypto Wallet: Processing ${transaction.Amount} for {transaction.Category}");
        }
    }

    // Base Account class
    public class Account
    {
        public string AccountNumber { get; }
        public decimal Balance { get; protected set; }

        public Account(string accountNumber, decimal initialBalance)
        {
            AccountNumber = accountNumber;
            Balance = initialBalance;
        }

        public virtual void ApplyTransaction(Transaction transaction)
        {
            Balance -= transaction.Amount;
        }
    }

    // Sealed SavingsAccount class
    public sealed class SavingsAccount : Account
    {
        public SavingsAccount(string accountNumber, decimal initialBalance) 
            : base(accountNumber, initialBalance)
        {
        }

        public override void ApplyTransaction(Transaction transaction)
        {
            if (transaction.Amount > Balance)
            {
                Console.WriteLine("Insufficient funds");
            }
            else
            {
                base.ApplyTransaction(transaction);
                Console.WriteLine($"Updated balance: ${Balance:F2}");
            }
        }
    }

    // Finance Application class
    public class FinanceApp
    {
        private List<Transaction> _transactions = new List<Transaction>();

        public void Run()
        {
            // Create SavingsAccount
            var savingsAccount = new SavingsAccount("SAV001", 1000);

            // Create sample transactions
            var transaction1 = new Transaction(1, DateTime.Now, 150, "Groceries");
            var transaction2 = new Transaction(2, DateTime.Now, 200, "Utilities");
            var transaction3 = new Transaction(3, DateTime.Now, 100, "Entertainment");

            // Create processors
            var mobileProcessor = new MobileMoneyProcessor();
            var bankProcessor = new BankTransferProcessor();
            var cryptoProcessor = new CryptoWalletProcessor();

            // Process transactions
            Console.WriteLine("=== Processing Transactions ===");
            mobileProcessor.Process(transaction1);
            savingsAccount.ApplyTransaction(transaction1);
            _transactions.Add(transaction1);

            bankProcessor.Process(transaction2);
            savingsAccount.ApplyTransaction(transaction2);
            _transactions.Add(transaction2);

            cryptoProcessor.Process(transaction3);
            savingsAccount.ApplyTransaction(transaction3);
            _transactions.Add(transaction3);

            Console.WriteLine($"\nTotal transactions processed: {_transactions.Count}");
            Console.WriteLine($"Final account balance: ${savingsAccount.Balance:F2}");
        }

        public static void RunDemo()
        {
            Console.WriteLine("=== Finance Management System ===");
            var app = new FinanceApp();
            app.Run();
        }
    }
}