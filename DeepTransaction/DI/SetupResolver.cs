namespace DeepTransaction.DI
{
    public static class SetupResolver
    {
        private static IDependencyResolver _dependencyResolver;

        public static void Setup(IDependencyResolver dependencyResolver)
        {
            _dependencyResolver = dependencyResolver;
        }

        public static IDependencyResolver Get()
        {
            return _dependencyResolver;
        }
    }
}