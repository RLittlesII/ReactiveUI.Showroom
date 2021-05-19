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