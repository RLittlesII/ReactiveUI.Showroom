using System;
using System.Collections.Generic;
using System.Linq;
using Bogus;

namespace Showroom.Scroll
{
    public class CoffeeInventoryMock : ClientMock<InventoryDto>
    {
        private IEnumerable<RoastType> RoastType = Enum.GetValues(typeof(RoastType)).Cast<RoastType>();
        private IEnumerable<PackagedAs> PackagedAs = Enum.GetValues(typeof(PackagedAs)).Cast<PackagedAs>();
        private readonly IEnumerable<string> BrandNames = new List<string>
        {
            "Dunkin Donuts",
            "Starbucks",
            "Community Coffee",
            "Lion",
            "Neighborhood Social",
            "Maxwell House",
            "Peet's Coffee",
            "Green Mountain",
            "Ethical Bean",
            "Green Mountain"
        };
        private readonly IEnumerable<string> CoffeeNames = new List<string>
        {
            "Sweet Espresso Roast",
            "Light Roast",
            "Blond Roast",
            "Medium Roast",
            "Italian Roast",
            "Kona Coffee",
            "Sumatra",
            "Espresso Roast",
            "Dark Roast"
        };

        public CoffeeInventoryMock()
        {
            var faker =
                new Faker<InventoryDto>()
                    .RuleFor(x => x.Id, y => Guid.NewGuid())
                    .RuleFor(x => x.BrandName, y => y.PickRandom(BrandNames))
                    .RuleFor(x => x.CoffeeName, y => y.PickRandom(CoffeeNames))
                    .RuleFor(x => x.Packaging, y => y.PickRandom<PackagedAs>())
                    .RuleFor(x => x.Roast, y => y.PickRandom<RoastType>());

            Items = faker.Generate(1000).ToList();
        }
    }
}