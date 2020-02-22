using System;
using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;
using System.Threading.Tasks;
using DynamicData;
using Showroom.Base;

namespace Showroom.ListView
{
    public class CoffeeService : DataServiceBase<CoffeeDto>, ICoffeeService
    {
        public CoffeeService(IClient client)
            : base(client)
        {
        }
    }
}