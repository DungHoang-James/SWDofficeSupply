using AutoMapper;
using OfficeSupply.Business.IServices;
using OfficeSupply.Business.Ultility.common;
using OfficeSupply.Business.Utility.common;
using OfficeSupply.Data.Entities;
using OfficeSupply.Data.Helper;
using OfficeSupply.Data.IRepository;
using OfficeSupply.Data.Models;
using OfficeSupply.Data.Models.DTOs;
using OfficeSupply.Data.Models.Requests;
using OfficeSupply.Data.Models.ResourceParams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfficeSupply.Business.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IUserRepository _userRepository;
        private readonly IDepartmentRepository _departmentRepository;
        private readonly ICompanyRepository _companyRepository;
        private readonly IAreaRepository _areaRepository;
        private readonly IProductInMenuRepository _productInMenuRepository;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository productRepository,
                              IUserRepository userRepository,
                              IDepartmentRepository departmentRepository,
                              ICompanyRepository companyRepository,
                              IAreaRepository areaRepository,
                              IProductInMenuRepository productInMenuRepository,
                              IMapper mapper)
        {
            this._productRepository = productRepository;
            this._userRepository = userRepository;
            this._departmentRepository = departmentRepository;
            this._companyRepository = companyRepository;
            this._areaRepository = areaRepository;
            this._productInMenuRepository = productInMenuRepository;
            this._mapper = mapper;
        }

        public async Task<GenericResult> GetProducts(ProductResourceParam resourceParams)
        {
            var user = _userRepository.GetUserByID(resourceParams.UserID);

            if (user == null) return ApiResponse.Error(messCode: OfficeException.CommonException.RecordNotFound);

            if (user.RoleID == RoleBase.EMPLOYEE || user.RoleID == RoleBase.LEADER)
            {
                int id = (int)user.DepartmentID;
                var department = _departmentRepository.GetById(id);
                var company = _companyRepository.GetById(department.CompanyID);
                var area = _areaRepository.GetById(company.AreaID);
                var productInMenu = _productInMenuRepository.GetProductsInMenu(area.MenuID, resourceParams);

                var productInMenuDTO = new Pagination<ProductInMenuDTO>(_mapper.Map<List<ProductInMenuDTO>>(productInMenu.Items),
                                                                        productInMenu.TotalCount,
                                                                        productInMenu.CurrentPage,
                                                                        productInMenu.PageSize);

                return ApiResponse.Ok(productInMenuDTO);
            }
            else if (user.RoleID == RoleBase.ADMIN)
            {
                var product = _productRepository.GetProducts(resourceParams);

                var productDTO = new Pagination<ProductDTO>(_mapper.Map<List<ProductDTO>>(product.Items),
                                                            product.TotalCount,
                                                            product.CurrentPage,
                                                            product.PageSize);

                return ApiResponse.Ok(productDTO);
            }
            //var result = _mapper.Map<List<ProductDTO>>(_productRepository.GetProducts(resourceParams));
            //return ApiResponse.Ok(result);
            return ApiResponse.Error();
        }

        public async Task<GenericResult> GetProduct(int id)
        {
            var product = _productRepository.GetById(id);
            if (product == null)
            {
                return ApiResponse.Error(messCode: OfficeException.CommonException.RecordNotFound);
            }
            return ApiResponse.Ok(_mapper.Map<ProductDTO>(product));
        }

        public async Task<GenericResult> CreateProduct(ProductPayload productPayload)
        {
            var product = _mapper.Map<Product>(productPayload);
            var result = _productRepository.Create(product);
            if (result == false)
            {
                return ApiResponse.Error(messCode: OfficeException.CommonException.RecordNotFound, messDetail: "Create product fail");
            }
            return ApiResponse.Ok(_mapper.Map<ProductDTO>(product));
        }

        public async Task<GenericResult> UpdateProduct(int id, ProductUpdatePayload productPayload)
        {
            var product = _productRepository.GetById(id);
            if (product == null) return ApiResponse.Error(messCode: OfficeException.CommonException.RecordNotFound);
            _mapper.Map(productPayload, product);
            var result = _productRepository.Update(product);
            if (result == false)
            {
                return ApiResponse.Error(messCode: OfficeException.CommonException.RecordNotFound, messDetail: "update product fail");
            }
            return ApiResponse.Ok();
        }

        public async Task<GenericResult> DeleteProduct(int id)
        {
            var product = _productRepository.GetById(id);
            if (product == null) return ApiResponse.Error(messCode: OfficeException.CommonException.RecordNotFound);
            product.IsDelete = true;
            var result = _productRepository.Update(product);
            if (result == false)
            {
                return ApiResponse.Error(messCode: OfficeException.CommonException.RecordNotFound, messDetail: "delete product fail");
            }
            return ApiResponse.Ok();
        }
    }
}
