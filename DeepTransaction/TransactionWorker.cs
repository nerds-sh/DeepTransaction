using System.Collections.Generic;
using System.Transactions;
using Trsanction.Core.DI;

namespace Trsanction.Core
{
    public class TransactionWorker
    {
        private string _name;
        private readonly Queue<ITransactionStep> _steps;
        private readonly IDependencyResolver _dependencyResolver;
        private static Queue<int> _transactionDeepneess;
        private TransactionScope _tranScope;

        public static TransactionWorker Define(string name)
        {
            _transactionDeepneess = new Queue<int>();
            return new TransactionWorker(name);
        }

        public TransactionWorker(string name)
        {
            _steps = new Queue<ITransactionStep>();
            _dependencyResolver = SetupResolver.Get();
            this._name = name;
        }

        public TransactionWorker AddStep<T>() where T : ITransactionStep
        {
            var step = _dependencyResolver.Get<T>();
            this._steps.Enqueue(step);

            return this;
        }

        public TransactionContext Process(TransactionContext input)
        {
            _transactionDeepneess.Enqueue(1);
            if (_transactionDeepneess.Count <= 1)
            {
                _tranScope = new TransactionScope();
            }

            dynamic previousOutput = null;
            while (_steps.Count > 0)
            {
                previousOutput = _steps.Dequeue().Execute(input);
            }

            if (_transactionDeepneess.Count <= 1)
            {
                _tranScope.Complete();
                _tranScope.Dispose();
                _tranScope = null;
            }

            _transactionDeepneess.Dequeue();

            return previousOutput;
        }

    }
}
