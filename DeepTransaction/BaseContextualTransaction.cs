using System;

namespace DeepTransaction
{
    public class BaseContextualTransaction<TContext> : BaseTransaction where TContext : class, IDisposable
    {
        public BaseContextualTransaction(string name, TContext context) : base(name)
        {
            _tran = new ContextualTransaction<TContext>(name, context);

        }
    }
}