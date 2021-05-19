using Rocket.Surgery.Airframe.Synthetic;

namespace Showroom.CollectionView
{
    public class DrinkDto : Dto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public double Price { get; set; }
        public DrinkType Type { get; set; }
    }
}