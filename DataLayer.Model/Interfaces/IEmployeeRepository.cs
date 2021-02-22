using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Model.Models;

namespace DataAccessLayer.Model.Interfaces
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<Employee>> GetAll();
        Employee GetByCode(string employeeCode);
        Task<bool> SaveEmployee(Employee employee);
        Task<bool> SaveEmployee(int i, Employee employee);
        Task<bool> DeleteEmployee(int i);
    }
}
