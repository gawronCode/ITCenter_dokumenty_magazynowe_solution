using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ITCenter_dokumenty_magazynowe.Models.DbModels;
using ITCenter_dokumenty_magazynowe.Repositories.IRepos;

namespace ITCenter_dokumenty_magazynowe.Repositories.Repos
{
    public class WarehouseDocRepo : IWarehouseDocRepo
    {
        public Task<bool> Create(WarehouseDoc entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(WarehouseDoc entity)
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<WarehouseDoc>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<WarehouseDoc> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Save(WarehouseDoc entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(WarehouseDoc entity)
        {
            throw new NotImplementedException();
        }
    }
}
