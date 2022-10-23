using Microsoft.EntityFrameworkCore;
using OfficeSupply.Data.DatabaseContext;
using OfficeSupply.Data.Entities;
using OfficeSupply.Data.Helper;
using OfficeSupply.Data.IRepository;
using OfficeSupply.Data.Models.DTOs;
using OfficeSupply.Data.Models.Requests;
using OfficeSupply.Data.Models.ResourceParams;
using OfficeSupply.Data.PropertyMapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfficeSupply.Data.Repository
{
    public class CompanyRepository : BaseRepository<Company>, ICompanyRepository
    {
        private readonly OfficeSupplyDB _officeSupplyDB;
        private readonly IPropertyMappingService _propertyMappingService;

        public CompanyRepository(OfficeSupplyDB officeSupplyDB, IPropertyMappingService propertyMappingService) : base(officeSupplyDB)
        {
            this._officeSupplyDB = officeSupplyDB;
            this._propertyMappingService = propertyMappingService;
        }

        public Pagination<Company> GetCompanies(CompanyResourceParam resourceParams)
        {
            var companyPropertyMappingDictionary = _propertyMappingService.GetPropertyMapping<CompanyDTO, Company>();
            var companyQuery = _officeSupplyDB.Companies.Where(c => c.Name.Contains(resourceParams.Name)
                                                               && c.Address.Contains(resourceParams.Address)
                                                               && (resourceParams.WalletFrom <= c.Wallet && c.Wallet <= resourceParams.WalletTo));
            companyQuery = companyQuery.ApplySort(resourceParams.OrderBy, companyPropertyMappingDictionary);
            var pageList = Pagination<Company>.Create(companyQuery, resourceParams.PageNumber, resourceParams.PageSize);
            return pageList;
        }

        public Company GetCompanyByIDMin(int id)
        {
            return _officeSupplyDB.Companies.FirstOrDefault(c => c.ID == id);
        }
    }
}
