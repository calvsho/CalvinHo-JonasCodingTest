using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using System.Net.Http;
using AutoMapper;
using BusinessLayer.Model.Interfaces;
using BusinessLayer.Model.Models;
using WebApi.Models;
using System.IO;
using System.Windows.Forms;


namespace WebApi.Controllers
{
    public class CompanyController : ApiController
    {
        private readonly ICompanyService _companyService;
        private readonly IMapper _mapper;

        public CompanyController(ICompanyService companyService, IMapper mapper)
        {
            _companyService = companyService;
            _mapper = mapper;
        }
        // GET api/<controller>
        public async Task<IEnumerable<CompanyDto>> GetAll()
        {
            try
            {
                IEnumerable<CompanyInfo> items = await _companyService.GetAllCompanies();
                return _mapper.Map<IEnumerable<CompanyDto>>(items);
            }
            catch(Exception e)
            {
                LogErrorMessage(e);
                return null;
            }
        }

        // GET api/<controller>/5
        public CompanyDto Get(string companyCode)
        {
            try
            {
                var item = _companyService.GetCompanyByCode(companyCode);
                return _mapper.Map<CompanyDto>(item);
            }
            catch (Exception e)
            {
                LogErrorMessage(e);
                return null;
            }
        }

        // POST api/<controller>
        public void Post([FromBody]CompanyDto value)
        {
            //_companyService
            try
            {
                var comp = _mapper.Map<CompanyInfo>(value);
                _companyService.InsertCompany(comp);
            }
            catch (Exception e)
            {
                LogErrorMessage(e);
            }

        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]CompanyDto value)
        {
            try
            {
                var comp = _mapper.Map<CompanyInfo>(value);
                _companyService.InsertCompany(id, comp);
            }
        
            catch(Exception e)
            {
                LogErrorMessage(e);
             }
   
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
            try
            {
                _companyService.DeleteCompany(id);
            }
            catch(Exception e)
            {
                LogErrorMessage(e);
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