using System;
using DeepTransaction.DI;
using DeepTransaction.TransactionScope;

namespace DeepTransaction
{
    public class ContextualTransaction<TContext> : TransactionEngine where TContext : class, IDisposable
    {
        private readonly TContext _context;
        private readonly bool _isParent;
        private readonly ITransactionScope<TContext> _transactionScope;

        internal ContextualTransaction(string name, TContext context, bool isParent = false) : base(name)
        {
            _context = context;
            _isParent = isParent;
            _transactionScope = (ITransactionScope<TContext>)DeepBootstrapper.GetContext();
        }

        private void ExecuteIfParent(Action action)
        {
            if (_isParent) action();
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

            ExecuteIfParent(() => _transactionScope.BeginTran(_context));
           
            TransactionContext outputContext;

            try
            {
                using (_context)
                {
                    outputContext = base.Process(input);
                    ExecuteIfParent(() => _transactionScope.Commit(_context));
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