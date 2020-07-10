using System;
using System.Threading.Tasks;
using DynamicData;

namespace Showroom.Scroll
{
    public class OrderService : IOrderService
    {
        private readonly IOrderClient _client;
        private readonly SourceCache<OrderDetailDto, Guid> _orders =
            new SourceCache<OrderDetailDto, Guid>(x => x.Id);

        public OrderService(IOrderClient client)
        {
            _client = client;
            _client
                .Orders
                .Subscribe(detail => _orders.AddOrUpdate(detail));
        }

        public IObservableCache<OrderDetailDto, Guid> Orders => _orders;

        public async Task GetOrders()
        {
            var orderDetails = await _client.InvokeAsync<OrderDetailDto>("GetOrders").ConfigureAwait(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _client?.Dispose();
                _orders?.Dispose();
            }
        }
    }
}