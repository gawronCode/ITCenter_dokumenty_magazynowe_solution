using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ITCenter_dokumenty_magazynowe.Data;
using ITCenter_dokumenty_magazynowe.Models.DbModels;
using ITCenter_dokumenty_magazynowe.Repositories.IRepos;
using Microsoft.EntityFrameworkCore;

namespace ITCenter_dokumenty_magazynowe.Repositories.Repos
{
    public class PositionRepo : IPositionRepo
    {

        private readonly ApplicationDbContext _context;

        public PositionRepo(ApplicationDbContext context)
        {
            _context = context;
        }

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

        public async Task<ICollection<Position>> GetAllByParentId(int parentId)
        {
            var positions = await _context.Positions.Where(q => q.WarehouseDocId == parentId).ToListAsync();
            return positions;
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
