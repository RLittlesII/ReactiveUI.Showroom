namespace Showroom.CollectionView
{
    public class CollectionOptionViewModel : ItemViewModelBase
    {
        public CollectionOption Option { get; set; }
    }

    public enum CollectionOption
    {
        Search,

        DetailNavigation,

        InfiniteScroll
    }
}