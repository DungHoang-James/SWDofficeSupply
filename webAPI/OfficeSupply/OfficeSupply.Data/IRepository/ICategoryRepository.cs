using OfficeSupply.Data.Entities;
using OfficeSupply.Data.Helper;
using OfficeSupply.Data.Models.DTOs;
using OfficeSupply.Data.Models.DTOs.statistics;
using OfficeSupply.Data.Models.Requests;
using OfficeSupply.Data.Models.ResourceParams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfficeSupply.Data.IRepository
{
    public interface ICategoryRepository : IBaseRepository<Category>
    {
        Pagination<Category> GetCategories(CategoryResourceParam resourceParams);
        List<Category> GetAll();
        List<CategoryStatisticsDTO> GetCategoryStatistics();
    }
}
