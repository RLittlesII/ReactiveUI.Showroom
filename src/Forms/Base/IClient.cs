using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Showroom
{
    public interface IClient
    {
        Task<T> Get<T>(Guid id) where T : Dto;

        Task<IEnumerable<T>> GetAll<T>() where T : Dto;

        Task<T> Post<T>(T entity) where T : Dto;

        Task<T> Delete<T>(Guid id) where T : Dto;

        Task<T> Delete<T>(T entity) where T : Dto;
    }
}