using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Model.Models;

namespace DataAccessLayer.Model.Interfaces
{
    public interface ICompanyRepository
    {
        Task<IEnumerable<Company>> GetAll();
        Company GetByCode(string companyCode);
        Task<bool> SaveCompany(Company company);
        Task<bool> SaveCompany(int i, Company company);
        Task<bool> DeleteCompany(int i);
    }
}
