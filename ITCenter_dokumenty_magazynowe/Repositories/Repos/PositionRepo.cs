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

        public async Task<Position> Create(Position entity)
        {
            var result = await _context.Positions.AddAsync(entity);
            await Save();
            return result.Entity;
        }

        public async Task<bool> Delete(Position entity)
        {
            _context.Positions.Remove(entity);
            return await Save();
        }

        public IQueryable<Position> GetAll()
        {
            var positions = _context.Positions;
            return positions;
        }

        public async Task<ICollection<Position>> GetAllByParentId(int parentId)
        {
            var positions = await _context.Positions.Where(q => q.WarehouseDocId == parentId).ToListAsync();
            return positions;
        }

        public async Task<Position> GetById(int id)
        {
            var position = await _context.Positions.FirstOrDefaultAsync(q => q.Id == id);
            return position;
        }

        public async Task<bool> RemoveByParentId(int parentId)
        {
            _context.Positions.RemoveRange(_context.Positions.Where(q=>q.WarehouseDocId == parentId));
            return await Save();
        }

        public async Task<bool> Save()
        {
            var changes = await _context.SaveChangesAsync();
            return changes > 0;
        }

        public Task<bool> Update(Position entity)
        {
            throw new NotImplementedException();
        }


    }
}
