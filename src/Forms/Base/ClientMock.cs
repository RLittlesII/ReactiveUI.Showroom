using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Showroom
{
    public class ClientMock<T> : IClient
        where T : Dto
    {
        protected List<T> Items = new List<T>();

        public Task<T> Get<T>(Guid id) where T : Dto => Task.FromResult((T) (object) Items.FirstOrDefault(x => x.Id == id));

        public Task<IEnumerable<T>> GetAll<T>() where T : Dto => Task.FromResult((IEnumerable<T>) Items);

        public Task<T> Post<T>(T entity) where T : Dto => Task.FromResult(entity);

        public Task<T> Delete<T>(Guid id) where T : Dto => Task.FromResult((T)(object)Items.FirstOrDefault(x => x.Id == id));

        public Task<T> Delete<T>(T entity) where T : Dto => Task.FromResult(entity);
    }
}