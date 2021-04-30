using System.Collections.Generic;
using System.Threading.Tasks;
using ITCenter_dokumenty_magazynowe.Models.DbModels;

namespace ITCenter_dokumenty_magazynowe.Repositories.IRepos
{
    public interface IPositionRepo : IGeneralRepo<Position>
    {
        public Task<ICollection<Position>> GetAllByParentId(int parentId);
        public Task<bool> RemoveByParentId(int parentId);
    }
}
