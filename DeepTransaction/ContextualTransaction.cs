using System;
using DeepTransaction.DI;
using DeepTransaction.TransactionScope;

namespace DeepTransaction
{
    public class ContextualTransaction<TContext> : TransactionEngine where TContext : class, IDisposable
    {
        private readonly TContext _context;
        private readonly ITransactionScope<TContext> _transactionScope;

        internal ContextualTransaction(string name, TContext context) : base(name)
        {
            _context = context;
            _transactionScope = (ITransactionScope<TContext>)DeepBootstrapper.GetContext();
        }

        public override TransactionContext Process(TransactionContext input)
        {
            foreach (var transactionStep in _steps)
            {
                if (transactionStep is IContextStep<TContext> step)
                {
                    step.Context = _context;
                }
            }

            _transactionScope.BeginTran(_context);
            TransactionContext outputContext;

            try
            {
                using (_context)
                {
                    outputContext = base.Process(input);
                    _transactionScope.Commit(_context);
                }
            }
            catch (Exception)
            {
                _transactionScope.Rollback(_context);
                throw;
            }
            return outputContext;
        }
    }
}