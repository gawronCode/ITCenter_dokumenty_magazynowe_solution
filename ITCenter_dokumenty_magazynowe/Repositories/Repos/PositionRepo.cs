using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ITCenter_dokumenty_magazynowe.Models.DbModels;
using ITCenter_dokumenty_magazynowe.Repositories.IRepos;

namespace ITCenter_dokumenty_magazynowe.Repositories.Repos
{
    public class PositionRepo : IPositionRepo
    {
        public Task<bool> Create(Position entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(Position entity)
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<Position>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<Position> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Save()
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(Position entity)
        {
            throw new NotImplementedException();
        }
    }
}
