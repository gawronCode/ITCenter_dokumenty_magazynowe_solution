using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ITCenter_dokumenty_magazynowe.Models.DbModels;
using ITCenter_dokumenty_magazynowe.Repositories.IRepos;

namespace ITCenter_dokumenty_magazynowe.Repositories.Repos
{
    public class EmployeeRepo : IEmployeeRepo
    {
        public Task<bool> Create(Employee entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(Employee entity)
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<Employee>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<Employee> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Save(Employee entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(Employee entity)
        {
            throw new NotImplementedException();
        }
    }
}
