using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ITCenter_dokumenty_magazynowe.Models.DbModels;
using ITCenter_dokumenty_magazynowe.Repositories.IRepos;

namespace ITCenter_dokumenty_magazynowe.Repositories.Repos
{
    public class OperationLogRepo : IOperationLogRepo
    {
        public Task<bool> Create(OperationLog entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(OperationLog entity)
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<OperationLog>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<OperationLog> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Save(OperationLog entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(OperationLog entity)
        {
            throw new NotImplementedException();
        }
    }
}
