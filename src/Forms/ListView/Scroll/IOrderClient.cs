using System;

namespace Showroom.Scroll
{
    public interface IOrderClient : IHubClient
    {
        IObservable<OrderDetailDto> Orders { get; }
    }
}