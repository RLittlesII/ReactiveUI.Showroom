using System.Collections.Generic;
using Showroom.Constants;

namespace Showroom.ListView
{
    public class CoffeeClientMock : ClientMock<CoffeeDto>
    {
        public CoffeeClientMock()
        {
            Items = new List<CoffeeDto>
            {
                new CoffeeDto
                {
                    Name = "Benguet",
                    Species = Species.Arabica,
                    Regions = new[]
                    {
                        Regions.Philippines
                    },
                    Image = "Benguet-Bean.jpg",
                    Britannica = Resources.CoffeeFacts.Benguet
                },
                new CoffeeDto
                {
                    Name = "Bergendal",
                    Species = Species.Arabica,
                    Regions = new[]
                    {
                        Regions.Indonesia
                    },
                    Image = "Bergendal-Bean.jpg"
                },
                new CoffeeDto
                {
                    Name = "Bernardina",
                    Species = Species.Arabica,
                    Regions = new[]
                    {
                        Regions.ElSalvador
                    },
                    Image = "Bernardina-Bean.jpg"
                },
                new CoffeeDto
                {
                    Name = "Blue Mountain",
                    Species = Species.Arabica,
                    Regions = new[]
                    {
                        "Blue Mountains, Jamaica",
                        Regions.Kenya,
                        Regions.Hawaii,
                        Regions.PapuNewGuinea,
                        Regions.Cameroon
                    },
                    Image = "Blue-Mountains-Jamaica.jpg"
                },
                new CoffeeDto
                {
                    Name = "Bourbon",
                    Species = Species.Arabica,
                    Regions = new[]
                    {
                        Regions.Reunion,
                        Regions.Rwanda,
                        Regions.LatinAmerica
                    },
                    Image = "Bourbon-Bean.jpg",
                    Britannica = Resources.CoffeeFacts.Bourbon
                },
                new CoffeeDto
                {
                    Name = "Catuai",
                    Species = Species.Arabica,
                    Regions = new[]
                    {
                        Regions.LatinAmerica
                    },
                    Image = "Catuai-Bean.jpg",
                    Britannica = Resources.CoffeeFacts.Catuai
                },
                new CoffeeDto
                {
                    Name = "Catimor", Species = Species.Arabica,
                    Regions = new[]
                    {
                        Regions.LatinAmerica,
                        Regions.Indonesia,
                        Regions.India,
                        Regions.China
                    },
                    Image = "Catimor-Bean.jpg"
                },
                new CoffeeDto // https://varieties.worldcoffeeresearch.org/varieties/caturra
                {
                    Name = "Caturra",
                    Species = Species.Arabica,
                    Regions = new[]
                    {
                        Regions.LatinAmerica
                    },
                    Image = "Caturra-Bean.jpg",
                    Britannica = Resources.CoffeeFacts.Caturra
                },
                new CoffeeDto
                {
                    Name = "Charrier",
                    Species = Species.Charrieriana,
                    Regions = new[]
                    {
                        Regions.Cameroon
                    },
                    Image = "Charrieriana-Bean.jpg",
                    Britannica = Resources.CoffeeFacts.Charrier
                },
                new CoffeeDto
                {
                    Name = "Geisha",
                    Species = Species.Arabica,
                    Regions = new[]
                    {
                        Regions.Ethipoia,
                        Regions.CostaRica,
                        Regions.Colombia,
                        Regions.Panama,
                        Regions.Peru,
                        Regions.Tanzania
                    },
                    Image = "Geisha-Bean.jpg",
                    Britannica = Resources.CoffeeFacts.Geisha
                },
                new CoffeeDto
                {
                    Name = "Kona",
                    Species = Species.Arabica,
                    Regions = new[]
                    {
                        Regions.Hawaii
                    },
                    Image = "Kona-Bean-3.jpg",
                    Britannica = Resources.CoffeeFacts.Geisha
                }
            };
        }
    }
}