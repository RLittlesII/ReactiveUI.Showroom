using Serilog;
using Showroom.Composition;
using Splat;

namespace Showroom.iOS
{
    public class iOSRegistrar : IPlatformRegistrar
    {
        public void RegisterPlatform(IMutableDependencyResolver mutableDependencyResolver)
        {
            Log.Logger =
                new LoggerConfiguration()
                    .WriteTo
                    .NSLog()
                    .MinimumLevel
                    .Debug()
                    .CreateLogger();
        }
    }
}