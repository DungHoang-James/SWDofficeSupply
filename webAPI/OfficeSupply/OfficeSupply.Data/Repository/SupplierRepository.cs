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
    public class SupplierRepository : BaseRepository<Supplier>, ISupplierRepository
    {
        private readonly OfficeSupplyDB _officeSupplyDB;
        private readonly IPropertyMappingService _propertyMappingService;

        public SupplierRepository(OfficeSupplyDB officeSupplyDB, IPropertyMappingService propertyMappingService) : base(officeSupplyDB)
        {
            this._officeSupplyDB = officeSupplyDB;
            this._propertyMappingService = propertyMappingService;
        }

        public Pagination<Supplier> GetSuppliers(SupplierResourceParam resourceParams)
        {
            var supplierPropertyMappingDictionary = _propertyMappingService.GetPropertyMapping<SupplierDTO, Supplier>();
            var supplierQuery = _officeSupplyDB.Suppliers.Where(s => s.Name.Contains(resourceParams.Name));
            supplierQuery = supplierQuery.ApplySort(resourceParams.OrderBy, supplierPropertyMappingDictionary);
            Pagination<Supplier> pageList = Pagination<Supplier>.Create(supplierQuery, resourceParams.PageNumber, resourceParams.PageSize);
            return pageList;
        }

        public List<Supplier> GetAll()
        {
            return _officeSupplyDB.Suppliers.Where(s => s.IsDelete == false).ToList();
        }
    }
}
