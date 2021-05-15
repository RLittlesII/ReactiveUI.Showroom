namespace Showroom.Scroll
{
    public class InventoryDto : Dto
    {
        public string BrandName { get; set; }

        public string CoffeeName { get; set; }

        public RoastType Roast { get; set; }

        public PackagedAs Packaging { get; set; }
    }
}