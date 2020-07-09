using Showroom.Base;

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