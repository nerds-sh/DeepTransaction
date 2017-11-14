using System;
using DeepTransaction.TransactionScope;

namespace DeepTransaction
{
    public class BaseContextualTransaction<TContext> : BaseTransaction, IContextStep<TContext> where TContext : class, IDisposable
    {
        protected BaseContextualTransaction(string name, TContext context) : base(name)
        {
            _tran = new ContextualTransaction<TContext>(name, context);
        }

        public TContext Context { get; set; }
    }
}