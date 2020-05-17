using System;
using System.Reactive.Disposables;
using System.Reactive.Linq;
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