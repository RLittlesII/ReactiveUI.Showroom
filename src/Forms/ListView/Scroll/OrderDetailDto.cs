using System;

namespace Showroom.Scroll
{
    public class OrderDetailDto : Dto
    {
        public string Name { get; set; }

        public string DrinkName { get; set; }

        public string Details { get; set; }

        public DateTime OrderTime { get; set; }

        public DrinkSize Size { get; set; }
    }
}