namespace DeepTransaction.DI
{
    public interface IDependencyResolver
    {
        TOut Resolve<TOut>();
    }
}