using System;
using DeepTransaction;
using DeepTransaction.TransactionScope;

namespace Transaction.Test.DataProviders
{
    public class MyContext : IDisposable
    {
        public virtual void Dispose()
        {
        }

        public virtual void BeginTran()
        {
            
        }

        public virtual void Rollback()
        {
            
        }

        public virtual void Commit()
        {
            
        }
    }

    public class TransactionStep : ITransactionStep, IContextStep<MyContext>
    {
        public void Before(dynamic input)
        {
            
        }

        public TransactionContext Execute(dynamic input)
        {
            return input;
        }

        public MyContext Context { get; set; }
    }
}