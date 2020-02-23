using Splat;

namespace Showroom
{
    public interface IPlatformRegistrar
    {
        void RegisterPlatform(IMutableDependencyResolver mutableDependencyResolver);
    }
}