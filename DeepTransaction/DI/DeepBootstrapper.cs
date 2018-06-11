using System;
using DeepTransaction.Listeners;

namespace DeepTransaction.DI
{
    public static class DeepBootstrapper
    {
        private static IDependencyResolver _dependencyResolver;
        private static IListener _listener;
        private static object _context;
        
        /// <summary>
        /// This method will register your dependecy resolver in order to take advantage of IOC
        /// </summary>
        /// <param name="dependencyResolver">Your implementation for dependecy resolver</param>
        public static void MapResolver(IDependencyResolver dependencyResolver)
        {
            _dependencyResolver = dependencyResolver;
        }

        /// <summary>
        /// Register a global listener for every transaction that is executed.
        /// </summary>
        /// <param name="listener">IListener instance implementation</param>
        public static void MapListener(IListener listener)
        {
            _listener = listener;
        }

        public static void MapContext(object context)
        {
            _context = context;
        }

        internal static IDependencyResolver Get()
        {
            if (_dependencyResolver == null)
            {
                throw new Exception("Before using Transactions please initilize the Dependecy Resolver. For more details please have a look on https://github.com/nerdscomputing/deeptransaction on Installation header");
            }

            return _dependencyResolver;
        }

        internal static IListener GetListener()
        {
            return _listener;
        }

        internal static object GetContext()
        {
            return _context;
        }
    }
}