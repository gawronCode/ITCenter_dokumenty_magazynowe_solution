using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITCenter_dokumenty_magazynowe.Repositories.IRepos
{
    public interface IGeneralRepo<T> where T : class
    {
        Task<ICollection<T>> GetAll();
        Task<T> GetById(int id);
        Task<bool> Create(T entity);
        Task<bool> Update(T entity);
        Task<bool> Delete(T entity);
        Task<bool> Save();
    }
}
