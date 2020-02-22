using Serilog;
using Showroom.Composition;
using Splat;
using Splat.Serilog;

namespace Showroom.Android
{
    public class AndroidRegistrar : IPlatformRegistrar
    {
        public void RegisterPlatform(IMutableDependencyResolver mutableDependencyResolver)
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo
                .AndroidLog()
                .CreateLogger();
            mutableDependencyResolver.UseSerilogFullLogger();
        }
    }
}