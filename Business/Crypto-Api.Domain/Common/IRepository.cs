using System;

namespace Crypto_Api.Domain.Common
{
    public interface IRepository<T>
    {
        IUnitOfWork UnitOfWork { get; }
    }
}