using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Net.Http;
using AutoMapper;
using BusinessLayer.Model.Interfaces;
using BusinessLayer.Model.Models;
using System.Threading.Tasks;
using WebApi.Models;
using System.IO;
using System.Windows.Forms;

namespace WebApi.Controllers
{
    public class EmployeeController : ApiController
    {
        private readonly IEmployeeService _employeeService;
        private readonly IMapper _mapper;

        public EmployeeController(IEmployeeService employeeService, IMapper mapper)
        {
            _employeeService = employeeService;
            _mapper = mapper;
        }
        // GET api/<controller>
        public async Task<IEnumerable<EmployeeDto>> GetAll()
        {
            try
            {
                IEnumerable<EmployeeInfo> items = await _employeeService.GetAllEmployees();
                return _mapper.Map<IEnumerable<EmployeeDto>>(items);
            }
            catch(Exception e)
            {
                LogErrorMessage(e);
                return null;
            }
            
        }

        // GET api/<controller>/5
        public EmployeeDto Get(string employeeCode)
        {
            try
            {
                var item = _employeeService.GetEmployeeByCode(employeeCode);
                return _mapper.Map<EmployeeDto>(item);
            }
            catch(Exception e)
            {
                LogErrorMessage(e);
                return null;
            }
        }
        public void LogErrorMessage(Exception e)
        {
            if (!File.Exists("C:\\Temp\\exceptionlog.txt"))
                File.Create("C:\\Temp\\exceptionlog.txt");

            using (StreamWriter w = File.AppendText("C:\\Temp\\exceptionlog.txt"))
            {
                w.WriteLine("Exception: " + e.Message);
                w.WriteLine("Occurred at: " + DateTime.Now);
                w.WriteLine("Details:" + e.InnerException + Environment.NewLine);
            }

            MessageBox.Show("Error Found. Please check log for details at C:\\Temp\\exceptionlog.txt.");
        }
    }
}
