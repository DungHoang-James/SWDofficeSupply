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
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        private readonly OfficeSupplyDB _officeSupplyDB;
        private readonly IPropertyMappingService _propertyMappingService;

        public ProductRepository(OfficeSupplyDB officeSupplyDB, IPropertyMappingService propertyMappingService) : base(officeSupplyDB)
        {
            this._officeSupplyDB = officeSupplyDB;
            this._propertyMappingService = propertyMappingService;
        }

        public Pagination<Product> GetProducts(ProductResourceParam resourceParams)
        {
            var productPropertyMappingDictionary = _propertyMappingService.GetPropertyMapping<ProductDTO, Product>();
            var productQuery = _officeSupplyDB.Products.Where(p => p.Name.Contains(resourceParams.Name));
            productQuery = productQuery.ApplySort(resourceParams.OrderBy, productPropertyMappingDictionary);

            var pageList = Pagination<Product>.Create(productQuery, resourceParams.PageNumber, resourceParams.PageSize);

            return pageList;
        }
    }
}
