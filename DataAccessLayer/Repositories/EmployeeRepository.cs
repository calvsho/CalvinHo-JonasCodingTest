using System.Collections.Generic;
using System.Linq;
using DataAccessLayer.Model.Interfaces;
using DataAccessLayer.Model.Models;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly IDbWrapper<Employee> _employeeDbWrapper;

        public EmployeeRepository(IDbWrapper<Employee> employeeDbWrapper)
        {
            _employeeDbWrapper = employeeDbWrapper;
        }

        public async Task<IEnumerable<Employee>> GetAll()
        {
            return await _employeeDbWrapper.FindAllAsync();
        }

        public Employee GetByCode(string employeeCode)
        {
            return _employeeDbWrapper.Find(t => t.EmployeeCode.Equals(employeeCode))?.FirstOrDefault();
        }

        public async Task<bool> SaveEmployee(Employee employee)
        {
            var itemRepo = _employeeDbWrapper.Find(t =>
                t.SiteId.Equals(employee.SiteId) && t.EmployeeCode.Equals(employee.EmployeeCode))?.FirstOrDefault();
            if (itemRepo != null)
            {
                itemRepo.EmployeeName = employee.EmployeeName;
                itemRepo.Occupation = employee.Occupation;
                itemRepo.EmployeeStatus = employee.EmployeeStatus;
                itemRepo.EmailAddress = employee.EmailAddress;
                itemRepo.Phone = employee.Phone;               
                itemRepo.LastModified = employee.LastModified;
                return await _employeeDbWrapper.UpdateAsync(itemRepo);
            }

            return await _employeeDbWrapper.InsertAsync(employee);
        }

        public async Task<bool> SaveEmployee(int id, Employee employee)
        {

            await _employeeDbWrapper.DeleteAsync(t => t.EmployeeCode.Equals(id.ToString()));


            return await _employeeDbWrapper.InsertAsync(employee);
        }
        public async Task<bool> DeleteEmployee(int id)
        {
            return await _employeeDbWrapper.DeleteAsync(t => t.EmployeeCode.Equals(id.ToString()));
        }

    }
}
