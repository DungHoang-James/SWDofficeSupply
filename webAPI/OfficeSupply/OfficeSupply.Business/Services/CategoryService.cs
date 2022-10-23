using AutoMapper;
using OfficeSupply.Business.IServices;
using OfficeSupply.Business.Ultility.common;
using OfficeSupply.Business.Utility.common;
using OfficeSupply.Data.Entities;
using OfficeSupply.Data.Helper;
using OfficeSupply.Data.IRepository;
using OfficeSupply.Data.Models.DTOs;
using OfficeSupply.Data.Models.DTOs.statistics;
using OfficeSupply.Data.Models.Requests;
using OfficeSupply.Data.Models.ResourceParams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfficeSupply.Business.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
        {
            this._categoryRepository = categoryRepository;
            this._mapper = mapper;
        }

        public async Task<GenericResult> GetCategories(CategoryResourceParam resourceParams)
        {
            if (resourceParams.All)
            {
                var cate = _categoryRepository.GetAll();
                return ApiResponse.Ok(resData: _mapper.Map<List<CategoryMinDTO>>(cate));
            }

            var categories = _categoryRepository.GetCategories(resourceParams);
            Pagination<CategoryDTO> result = new(_mapper.Map<List<CategoryDTO>>(categories.Items),
                                                categories.TotalCount,
                                                categories.CurrentPage,
                                                categories.PageSize);
            return ApiResponse.Ok(resData: result, totalCount: result.TotalCount);
        }

        public async Task<GenericResult> GetCategory(int id)
        {
            var category = _categoryRepository.GetById(id);

            if (category == null) return ApiResponse.Error(messCode: OfficeException.CommonException.RecordNotFound);
            var result = _mapper.Map<CategoryDTO>(category);
            return ApiResponse.Ok(result);
        }

        public async Task<GenericResult> AddCategory(CategoryPayload categoryPayload)
        {
            var category = _mapper.Map<Category>(categoryPayload);
            _categoryRepository.Create(category);
            var result = _mapper.Map<CategoryDTO>(category);
            return ApiResponse.Ok(result);
        }

        public async Task<GenericResult> UpdateCategory(int id, CategoryDTO categoryPayload)
        {
            var category = _categoryRepository.GetById(id);

            if (category == null)
            {
                return ApiResponse.Error(messCode: OfficeException.CommonException.RecordNotFound);
            }

            _mapper.Map(categoryPayload, category);

            _categoryRepository.Update(category);

            return ApiResponse.Ok();
        }

        public async Task<GenericResult> DeleteCategory(int id)
        {
            var category = _categoryRepository.GetById(id);

            if (category == null)
            {
                return ApiResponse.Error(messCode: OfficeException.CommonException.RecordNotFound);
            }

            category.IsDelete = true;

            _categoryRepository.Update(category);
            return ApiResponse.Ok();
        }

        public List<CategoryStatisticsDTO> GetCategoryStatistics()
        {
            return _categoryRepository.GetCategoryStatistics();
        }
    }
}
