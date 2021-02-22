using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessLayer.Model.Models;

namespace BusinessLayer.Model.Interfaces
{
    public interface ICompanyService
    {
        Task<IEnumerable<CompanyInfo>> GetAllCompanies();
        CompanyInfo GetCompanyByCode(string companyCode);

        void InsertCompany(CompanyInfo comp);
        void InsertCompany(int id, CompanyInfo comp);
        void DeleteCompany(int id);
    }
}
