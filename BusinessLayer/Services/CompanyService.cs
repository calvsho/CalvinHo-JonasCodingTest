using BusinessLayer.Model.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using BusinessLayer.Model.Models;
using DataAccessLayer.Model.Interfaces;
using DataAccessLayer.Model.Models;
namespace BusinessLayer.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IMapper _mapper;

        public CompanyService(ICompanyRepository companyRepository, IMapper mapper)
        {
            _companyRepository = companyRepository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<CompanyInfo>> GetAllCompanies()
        {
            IEnumerable<Company> res = await _companyRepository.GetAll();
            return _mapper.Map<IEnumerable<CompanyInfo>>(res);
        }

        public CompanyInfo GetCompanyByCode(string companyCode)
        {
            var result = _companyRepository.GetByCode(companyCode);
            return _mapper.Map<CompanyInfo>(result);
        }

        public void InsertCompany(CompanyInfo comp)
        {
            _companyRepository.SaveCompany(_mapper.Map<Company>(comp));
        }
        public void InsertCompany(int id, CompanyInfo comp)
        {
            _companyRepository.SaveCompany(id,_mapper.Map<Company>(comp));
        }
        public void DeleteCompany(int id)
        {
            _companyRepository.DeleteCompany(id);
        }
    }
}
