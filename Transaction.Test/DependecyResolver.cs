using Trsanction.Core.DI;

namespace Transaction.Test
{
    public class DependecyResolver : IDependencyResolver
    {
        public TOut Get<TOut>()
        {
            return Container.Instance.Resolve<TOut>();
        }
    }
}