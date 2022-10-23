using Microsoft.EntityFrameworkCore;
using OfficeSupply.Data.DatabaseContext;
using OfficeSupply.Data.Entities;
using OfficeSupply.Data.Helper;
using OfficeSupply.Data.IRepository;
using OfficeSupply.Data.Models.DTOs;
using OfficeSupply.Data.Models.DTOs.statistics;
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
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        private readonly OfficeSupplyDB _officeSupplyDB;
        private readonly IPropertyMappingService _propertyMappingService;

        public CategoryRepository(OfficeSupplyDB officeSupplyDB, IPropertyMappingService propertyMappingService) : base(officeSupplyDB)
        {
            this._officeSupplyDB = officeSupplyDB;
            this._propertyMappingService = propertyMappingService;
        }

        public Pagination<Category> GetCategories(CategoryResourceParam resourceParams)
        {
            var categoryPropertyMappingDictionary = _propertyMappingService.GetPropertyMapping<CategoryDTO, Category>();
            var categoriesQuery = _officeSupplyDB.Categories.Where(c => c.Name.Contains(resourceParams.Name));
            categoriesQuery = categoriesQuery.ApplySort(resourceParams.OrderBy, categoryPropertyMappingDictionary);
            var pageList = Pagination<Category>.Create(categoriesQuery, resourceParams.PageNumber, resourceParams.PageSize);
            return pageList;
        }

        public List<Category> GetAll()
        {
            return _officeSupplyDB.Categories.Where(c => c.IsDelete == false).ToList();
        }

        public List<CategoryStatisticsDTO> GetCategoryStatistics()
        {
            return _officeSupplyDB.Categories.Include(c => c.Products)
                                             .Select(c => new CategoryStatisticsDTO { ID = c.ID, NumOfProduct = c.Products.Count }).ToList();
        }
    }
}
