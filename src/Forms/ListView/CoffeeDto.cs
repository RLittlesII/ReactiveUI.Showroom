using System.Collections;
using System.Collections.Generic;
using Showroom.Base;

namespace Showroom.ListView
{
    public class CoffeeDto : Dto
    {
        public string Name { get; set; }

        public string Species { get; set; }

        public IEnumerable<string> Regions { get; set; }

        public string Image { get; set; }

        public string Britannica { get; set; }
    }
}