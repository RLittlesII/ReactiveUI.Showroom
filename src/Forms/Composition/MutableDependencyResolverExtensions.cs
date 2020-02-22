using Splat;

namespace Showroom.Composition
{
    public static class MutableDependencyResolverExtensions
    {
        public static IMutableDependencyResolver RegisterPlatform(this IMutableDependencyResolver mutableDependencyResolver, IPlatformRegistrar platformRegistrar)
        {
            platformRegistrar.RegisterPlatform(mutableDependencyResolver);
            return mutableDependencyResolver;
        }
    }
}