using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Adneom.CoffeMachine.Domain.Repository;

namespace Adneom.CoffeMachine.Domain.UnitOfWork
{
    public interface IUnitOfWork
    {
        IGenericRepository<T> Repository<T>() where T : class;

        Task<int> Commit();

        void Rollback();
    }
}
