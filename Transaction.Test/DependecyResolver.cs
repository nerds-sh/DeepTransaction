using DeepTransaction.DI;

namespace Transaction.Test
{
    public class DependecyResolver : IDependencyResolver
    {
        public TOut Resolve<TOut>()
        {
            return Container.Instance.Resolve<TOut>();
        }
    }
}