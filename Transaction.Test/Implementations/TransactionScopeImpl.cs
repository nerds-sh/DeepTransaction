using DeepTransaction.TransactionScope;
using Transaction.Test.DataProviders;

namespace Transaction.Test.Implementations
{
    public class TransactionScopeImpl : ITransactionScope<MyContext>
    {
        public void BeginTran(MyContext context)
        {
            context.BeginTran();
        }

        public void Commit(MyContext context)
        {
            context.Commit();
        }

        public void Rollback(MyContext context)
        {
            context.Rollback();
        }
    }
}