using AutoMapper;
using OfficeSupply.Business.IServices;
using OfficeSupply.Business.Ultility.common;
using OfficeSupply.Business.Utility.common;
using OfficeSupply.Data.Entities;
using OfficeSupply.Data.Helper;
using OfficeSupply.Data.IRepository;
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
    public class MenuService : IMenuService
    {
        private readonly IMenuRepository _menuRepository;
        private readonly IProductInMenuRepository _productInMenuRepository;
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public MenuService(IMenuRepository menuRepository,
                           IProductInMenuRepository productInMenuRepository,
                           IProductRepository productRepository,
                           IMapper mapper)
        {
            this._menuRepository = menuRepository;
            this._productInMenuRepository = productInMenuRepository;
            this._productRepository = productRepository;
            this._mapper = mapper;
        }

        public async Task<GenericResult> GetMenus(MenuResourceParam resourceParams)
        {
            if (resourceParams.All)
            {
                var menusMin = _menuRepository.GetMenusMin();
                return ApiResponse.Ok(resData: menusMin);
            }

            var menus = _menuRepository.GetMenus(resourceParams);
            Pagination<MenuDTO> result = new(_mapper.Map<List<MenuDTO>>(menus.Items),
                                            menus.TotalCount,
                                            menus.CurrentPage,
                                            menus.PageSize);
            return ApiResponse.Ok(resData: result, totalCount: menus.TotalCount);
        }

        public async Task<GenericResult> GetMenu(int id)
        {
            var menu = _menuRepository.GetById(id);

            if (menu == null) return ApiResponse.Error(messCode: OfficeException.CommonException.RecordNotFound);

            var result = _mapper.Map<MenuDTO>(menu);

            return ApiResponse.Ok(result);
        }

        public async Task<GenericResult> GetProductInMenu(int menuId, ProductInMenuResourceParam resourceParam)
        {
            if (resourceParam.All)
            {
                var productInMenuMin = _productInMenuRepository.GetProductsInMenuMin(menuId);

                return ApiResponse.Ok(_mapper.Map<List<ProductInMenuMinDTO>>(productInMenuMin));
            }

            var productInMenu = _productInMenuRepository.GetProductsInMenuIgnoreStatus(menuId, resourceParam);

            Pagination<ProductInMenuDTO> result = new(_mapper.Map<List<ProductInMenuDTO>>(productInMenu.Items),
                                                      productInMenu.TotalCount,
                                                      productInMenu.CurrentPage,
                                                      productInMenu.PageSize);

            return ApiResponse.Ok(result);
        }

        public async Task<GenericResult> AddProductInMenu(int menuId, ProductInMenuPayload productInMenuPayload)
        {
            var productInMenu = _mapper.Map<ProductInMenu>(productInMenuPayload);
            productInMenu.MenuID = menuId;


            var result = _productInMenuRepository.Create(productInMenu);
            if (!result)
            {
                return ApiResponse.Error(messDetail: "Insert product into menu fail");
            }

            var product = _productRepository.GetById(productInMenu.ProductID);
            product.Quantity -= productInMenu.Quantity;
            _productRepository.Update(product);

            return ApiResponse.Ok();
        }

        public async Task<GenericResult> UpdateProductInMenu(int menuId, ProductInMenuUpdatePayload productInMenuPayload)
        {
            var productInMenu = _productInMenuRepository.GetProductInMenu(menuId, productInMenuPayload.ProductID);

            if (productInMenu == null)
            {
                return ApiResponse.Error(messDetail: "Not found this product in the menu");
            }

            var product = _productRepository.GetById(productInMenu.ProductID);
            var oldQuantity = productInMenu.Quantity;
            product.Quantity += oldQuantity;

            if(product.Quantity < productInMenuPayload.Quantity)
            {
                return ApiResponse.Error(messDetail: $"Quantity is not enough. The remaining amount {product.Quantity}");
            }

            _mapper.Map(productInMenuPayload, productInMenu);

            var result = _productInMenuRepository.Update(productInMenu);

            if (!result)
            {
                return ApiResponse.Error(messDetail: "Update product in menu fail");
            }

            product.Quantity -= productInMenu.Quantity;
            _productRepository.Update(product);

            return ApiResponse.Ok();
        }

        public async Task<GenericResult> AddMenu(MenuPayload menuPayload)
        {
            var menu = _mapper.Map<Menu>(menuPayload);

            var checkAdd = _menuRepository.Create(menu);
            if (!checkAdd) return ApiResponse.Error(messCode: OfficeException.CommonException.RecordNotFound, messDetail: "error Add menu");

            var result = _mapper.Map<MenuDTO>(menu);

            return ApiResponse.Ok(result);
        }

        public async Task<GenericResult> UpdateMenu(int id, MenuDTO menuPayload)
        {
            var menu = _menuRepository.GetById(id);

            if (menu == null) return ApiResponse.Error(messCode: OfficeException.CommonException.RecordNotFound); ;

            _mapper.Map(menuPayload, menu);

            var result = _menuRepository.Update(menu);
            if (!result)
            {
                return ApiResponse.Error(messCode: OfficeException.CommonException.RecordNotFound, messDetail: "error update menu");
            }
            return ApiResponse.Ok();
        }

        public async Task<GenericResult> DeleteMenu(int id)
        {
            var menu = _menuRepository.GetById(id);

            if (menu == null) return ApiResponse.Error(messCode: OfficeException.CommonException.RecordNotFound);

            menu.IsDelete = true;
            var result = _menuRepository.Update(menu);
            if (!result)
            {
                return ApiResponse.Error(messCode: OfficeException.CommonException.RecordNotFound, messDetail: "error delete menu");
            }
            return ApiResponse.Ok();
        }
    }
}
