using System;

namespace DeepTransaction
{
    public class TransactionWorker
    {
        public static TransactionEngine Define(string name)
        {
            return new TransactionEngine(name);
        }

        public static ContextualTransaction<TContext> Define<TContext>(string name, TContext context) where TContext : class, IDisposable
        {
            return new ContextualTransaction<TContext>(name, context, true);
        }
    }
}
