using System;
using System.Reactive;
using DynamicData;

namespace Showroom.Base
{
    public interface IDataService
    {
        IObservable<Unit> Create(object dto);

        IObservable<object> Read();

        IObservable<object> Read(Guid id);

        IObservable<Unit> Update(object dto);

        IObservable<Unit> Delete(object dto);
    }

    public interface IDataService<T> : IDataService
        where T : Dto
    {
        IObservable<IChangeSet<T, Guid>> ChangeSet { get; }

        IObservable<Unit> Create(T dto);

        new IObservable<T> Read();

        new IObservable<T> Read(Guid id);

        IObservable<Unit> Update(T dto);

        IObservable<Unit> Delete(T dto);
    }
}