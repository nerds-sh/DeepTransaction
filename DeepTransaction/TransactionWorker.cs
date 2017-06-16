using System.Collections.Generic;
using System.Transactions;
using DeepTransaction.DI;

namespace DeepTransaction
{
    public class TransactionWorker
    {
        private string _name;
        private readonly Queue<ITransactionStep> _steps;
        private readonly IDependencyResolver _dependencyResolver;
        private static Queue<int> _transactionDeepness;
        private TransactionScope _tranScope;

        public static TransactionWorker Define(string name)
        {
            _transactionDeepness = new Queue<int>();
            return new TransactionWorker(name);
        }

        private TransactionWorker(string name)
        {
            _steps = new Queue<ITransactionStep>();
            _dependencyResolver = DeepBootstrapper.Get();
            this._name = name;
        }

        /// <summary>
        /// This method is using for chaning steps for the current transaction
        /// </summary>
        /// <typeparam name="TStep">The type of the step</typeparam>
        /// <returns>A base transaction used for chainging</returns>
        public TransactionWorker AddStep<TStep>() where TStep : ITransactionStep
        {
            var step = _dependencyResolver.Resolve<TStep>();
            this._steps.Enqueue(step);

            return this;
        }

        /// <summary>
        /// This method is for passing the context and execute the current transaction
        /// </summary>
        /// <param name="input">Input Context which has to have the type Transaction Context</param>
        /// <returns></returns>
        public TransactionContext Process(TransactionContext input)
        {
            _transactionDeepness.Enqueue(1);
            if (_transactionDeepness.Count <= 1)
            {
                _tranScope = new TransactionScope();
            }

            dynamic previousOutput = null;
            while (_steps.Count > 0)
            {
                var step = _steps.Dequeue();
                step.Before(input);
                previousOutput = step.Execute(input);
            }

            if (_transactionDeepness.Count <= 1)
            {
                _tranScope.Complete();
                _tranScope.Dispose();
                _tranScope = null;
            }

            _transactionDeepness.Dequeue();

            return previousOutput;
        }

    }
}
