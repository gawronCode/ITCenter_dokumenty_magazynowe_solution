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
    public class WarehouseDocRepo : IWarehouseDocRepo
    {
        private readonly ApplicationDbContext _context;
        public WarehouseDocRepo(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<WarehouseDoc> Create(WarehouseDoc entity)
        {
            var result = await _context.WarehouseDocs.AddAsync(entity);
            await Save();
            return result.Entity;
        }

        public async Task<bool> Delete(WarehouseDoc entity)
        {
            _context.WarehouseDocs.Remove(entity);
            return await Save();
        }

        public IQueryable<WarehouseDoc> GetAll()
        {
            var docs =  _context.WarehouseDocs;
            return docs;
        }

        public async Task<WarehouseDoc> GetById(int id)
        {
            var warehouseDoc = await _context.WarehouseDocs.FirstOrDefaultAsync(q => q.Id == id);
            return warehouseDoc;
        }

        public async Task<bool> Save()
        {
            var changes = await _context.SaveChangesAsync();
            return changes > 0;
        }

        public async Task<bool> Update(WarehouseDoc entity)
        {
            _context.Update(entity);
            return await Save();
        }
    }
}
