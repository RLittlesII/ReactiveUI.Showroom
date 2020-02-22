using Splat;

namespace Showroom.Composition
{
    public interface IPlatformRegistrar
    {
        void RegisterPlatform(IMutableDependencyResolver mutableDependencyResolver);
    }
}