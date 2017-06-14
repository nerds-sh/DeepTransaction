namespace Trsanction.Core.DI
{
    public interface IDependencyResolver
    {
        TOut Get<TOut>();
    }
}