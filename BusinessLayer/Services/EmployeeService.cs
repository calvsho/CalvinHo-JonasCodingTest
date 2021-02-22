using BusinessLayer.Model.Interfaces;
using System.Collections.Generic;
using AutoMapper;
using BusinessLayer.Model.Models;
using DataAccessLayer.Model.Interfaces;
using DataAccessLayer.Model.Models;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;

        public EmployeeService(IEmployeeRepository employeeRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<EmployeeInfo>> GetAllEmployees()
        {
            IEnumerable<Employee> res = await _employeeRepository.GetAll();
            return _mapper.Map<IEnumerable<EmployeeInfo>>(res);
        }

        public EmployeeInfo GetEmployeeByCode(string companyCode)
        {
            var result = _employeeRepository.GetByCode(companyCode);
            return _mapper.Map<EmployeeInfo>(result);
        }

        public void InsertEmployee(EmployeeInfo comp)
        {
            _employeeRepository.SaveEmployee(_mapper.Map<Employee>(comp));
        }
        public void InsertEmployee(int id, EmployeeInfo comp)
        {
            _employeeRepository.SaveEmployee(id, _mapper.Map<Employee>(comp));
        }
        public void DeleteEmployee(int id)
        {
            _employeeRepository.DeleteEmployee(id);
        }
    }
}
