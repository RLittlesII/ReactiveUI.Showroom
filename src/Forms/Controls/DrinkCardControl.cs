using Xamarin.Forms;

namespace Showroom
{
    /// <summary>
    /// Represents reusable control to display a drink card.
    /// </summary>
    /// <remarks>Original code from https://github.com/jsuarezruiz/RelativeSourcePlayground</remarks>
    public class DrinkCardControl : ContentView
    {
        public static readonly BindableProperty DrinkTitleProperty = BindableProperty.Create(nameof(DrinkTitle), typeof(string), typeof(DrinkCardControl), string.Empty);
        public static readonly BindableProperty DrinkDescriptionProperty = BindableProperty.Create(nameof(DrinkDescription), typeof(string), typeof(DrinkCardControl), string.Empty);
        public static readonly BindableProperty DrinkColorProperty = BindableProperty.Create(nameof(DrinkColor), typeof(Color), typeof(DrinkCardControl), Color.Default);
        public static readonly BindableProperty DrinkImageSourceProperty = BindableProperty.Create(nameof(DrinkImageSource), typeof(ImageSource), typeof(DrinkCardControl), default(ImageSource));

        public string DrinkTitle
        {
            get => (string)GetValue(DrinkTitleProperty);
            set => SetValue(DrinkTitleProperty, value);
        }

        public string DrinkDescription
        {
            get => (string)GetValue(DrinkDescriptionProperty);
            set => SetValue(DrinkDescriptionProperty, value);
        }

        public Color DrinkColor
        {
            get => (Color)GetValue(DrinkColorProperty);
            set => SetValue(DrinkColorProperty, value);
        }

        public ImageSource DrinkImageSource
        {
            get => (ImageSource)GetValue(DrinkImageSourceProperty);
            set => SetValue(DrinkImageSourceProperty, value);
        }
    }
}