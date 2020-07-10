namespace Showroom
{
    public class OptionViewModel : ItemViewModelBase
    {
        public ListOption Option { get; set; }
    }

    public enum ListOption
    {
        Search,

        DetailNavigation,

        InfiniteLoad
    }
}