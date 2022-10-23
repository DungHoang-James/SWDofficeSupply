using OfficeSupply.Business.Ultility.common;
using OfficeSupply.Data.Entities;
using OfficeSupply.Data.Models.DTOs;
using OfficeSupply.Data.Models.DTOs.statistics;
using OfficeSupply.Data.Models.Requests;
using OfficeSupply.Data.Models.ResourceParams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfficeSupply.Business.IServices
{
    public interface ICategoryService
    {
        public Task<GenericResult> GetCategory(int id);
        public Task<GenericResult> AddCategory(CategoryPayload categoryPayload);
        public Task<GenericResult> DeleteCategory(int id);
        public Task<GenericResult> UpdateCategory(int id, CategoryDTO categoryPayload);
        public Task<GenericResult> GetCategories(CategoryResourceParam resourceParams);
        List<CategoryStatisticsDTO> GetCategoryStatistics();
    }
}
