using System;
using System.Threading.Tasks;
using DynamicData;

namespace Showroom.Scroll
{
    public interface IOrderService : IDisposable
    {
        /// <summary>
        /// Gets an observable cache of orders.
        /// </summary>
        IObservableCache<OrderDetailDto, Guid> Orders { get; }

        /// <summary>
        /// Gets orders from the client.
        /// </summary>
        Task GetOrders();
    }
}