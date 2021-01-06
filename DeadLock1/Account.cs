using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace DeadLock1
{
    public class Account
    {
        double Balance;
        int _id;

        public Account(int id, double balance)
        {
            this._id = id;
            this.Balance = balance;
        }

        public int ID
        {
            get
            {
                return _id;
            }
        }

        public void Withdraw(double amount)
        {
            Balance -= amount;
        }

        public void Deposit(double amount)
        {
            Balance += amount;
        }
    }

    public class AccountManager
    {
        Account FromAccount;
        Account ToAccount;
        double AmountToTransfer;

        public AccountManager(Account fromAccount, Account toAccount, double amountToTransfer)
 
        {
            this.FromAccount = fromAccount;
            this.ToAccount = toAccount;
            this.AmountToTransfer = amountToTransfer;
        }

        public void Transfer()
        {
            Console.WriteLine(Thread.CurrentThread.Name+ " trying to acquire lock on "+ FromAccount.ID.ToString());
            lock (FromAccount)
            {
                Console.WriteLine(Thread.CurrentThread.Name+ " acquired lock on "+ FromAccount.ID.ToString());

                Console.WriteLine(Thread.CurrentThread.Name + " suspended for 1 second");

                Thread.Sleep(1000);

                Console.WriteLine(Thread.CurrentThread.Name +" back in action and trying to acquire lock on "+ ToAccount.ID.ToString());

                lock (ToAccount)
                {
                    FromAccount.Withdraw(AmountToTransfer);
                    ToAccount.Deposit(AmountToTransfer);
                }
            }
        }
    }
}
    
