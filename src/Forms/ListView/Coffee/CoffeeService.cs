using System.Reactive.Linq;
using Showroom.Base;
using DynamicData;

namespace Showroom.Coffee
{
    public class CoffeeService : DataServiceBase<CoffeeDto>, ICoffeeService
    {
        public CoffeeService(IClient client)
            : base(client)
        {
        }
    }
}