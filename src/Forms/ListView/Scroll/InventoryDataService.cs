using Showroom.Base;

namespace Showroom.Scroll
{
    public class InventoryDataService : DataServiceBase<InventoryDto>, IInventoryDataService
    {
        public InventoryDataService(IClient client)
            : base(client)
        {
        }
    }
}