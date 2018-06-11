using System;

namespace DeepTransaction.TransactionScope
{
    public interface ITransactionScope<in TContext> where TContext : IDisposable
    {
        void BeginTran(TContext context);

        void Commit(TContext context);

        void Rollback(TContext context);
    }
}