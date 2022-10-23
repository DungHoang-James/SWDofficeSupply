using OfficeSupply.Data.Entities;
using OfficeSupply.Data.Helper;
using OfficeSupply.Data.Models.Requests;
using OfficeSupply.Data.Models.ResourceParams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfficeSupply.Data.IRepository
{
    public interface ICompanyRepository : IBaseRepository<Company>
    {
        Pagination<Company> GetCompanies(CompanyResourceParam resourceParams);
        Company GetCompanyByIDMin(int id);
    }
}
