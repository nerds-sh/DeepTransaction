namespace DeepTransaction.DI
{
    public interface IDependencyResolver
    {
        TOut Get<TOut>();
    }
}