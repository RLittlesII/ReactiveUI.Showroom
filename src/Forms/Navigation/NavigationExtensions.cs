using Sextant;
using Splat;

namespace Showroom.Navigation
{
    public static class NavigationExtensions
    {
        private static IParameterViewStackService _stackService;

        static NavigationExtensions()
        {
            _stackService = Locator.Current.GetService<IParameterViewStackService>();
        }
    }
}