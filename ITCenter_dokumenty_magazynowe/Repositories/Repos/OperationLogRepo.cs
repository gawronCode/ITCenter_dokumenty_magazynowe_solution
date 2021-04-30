using System;
using System.Linq;
using System.Threading.Tasks;
using ITCenter_dokumenty_magazynowe.Data;
using ITCenter_dokumenty_magazynowe.Models.DbModels;
using ITCenter_dokumenty_magazynowe.Repositories.IRepos;

namespace ITCenter_dokumenty_magazynowe.Repositories.Repos
{
    public class OperationLogRepo : IOperationLogRepo
    {
        private readonly ApplicationDbContext _context;

        public OperationLogRepo(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<OperationLog> Create(OperationLog entity)
        {
            var result =await _context.OperationLogs.AddAsync(entity);
            await Save();
            return result.Entity;
        }

        public Task<bool> Delete(OperationLog entity)
        {
            throw new NotImplementedException();
        }

        public IQueryable<OperationLog> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<OperationLog> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Save()
        { 
            var changes = await _context.SaveChangesAsync();
            return changes > 0;
        }

        public Task<bool> Update(OperationLog entity)
        {
            throw new NotImplementedException();
        }
    }
}
