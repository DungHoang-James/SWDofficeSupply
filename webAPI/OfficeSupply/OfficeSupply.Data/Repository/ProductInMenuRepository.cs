using Microsoft.EntityFrameworkCore;
using OfficeSupply.Data.DatabaseContext;
using OfficeSupply.Data.Entities;
using OfficeSupply.Data.Helper;
using OfficeSupply.Data.IRepository;
using OfficeSupply.Data.Models.DTOs;
using OfficeSupply.Data.Models.ResourceParams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfficeSupply.Data.Repository
{
    public class ProductInMenuRepository : BaseRepository<ProductInMenu>, IProductInMenuRepository
    {
        private readonly OfficeSupplyDB _officeSupplyDB;

        public ProductInMenuRepository(OfficeSupplyDB officeSupplyDB) : base(officeSupplyDB)
        {
            this._officeSupplyDB = officeSupplyDB;
        }

        public Pagination<ProductInMenu> GetProductsInMenu(int menuId, ProductResourceParam resourceParam)
        {
            var productQuery = _officeSupplyDB.ProductInMenus.Include(p => p.Product)
                                                             .Where(p => p.MenuID == menuId && p.IsDelete == false && p.Product.Name.Contains(resourceParam.Name));

            var pageList = Pagination<ProductInMenu>.Create(productQuery, resourceParam.PageNumber, resourceParam.PageSize);

            return pageList;
        }

        public Pagination<ProductInMenu> GetProductsInMenuIgnoreStatus(int menuId, ProductInMenuResourceParam resourceParams)
        {
            var productQuery = _officeSupplyDB.ProductInMenus.Include(p => p.Product)
                                                             .Where(p => p.MenuID == menuId && p.Product.Name.Contains(resourceParams.Name));

            var pageList = Pagination<ProductInMenu>.Create(productQuery, resourceParams.PageNumber, resourceParams.PageSize);

            return pageList;
        }

        public List<ProductInMenu> GetProductsInMenuMin(int menuId)
        {
            return _officeSupplyDB.ProductInMenus.Where(p => p.MenuID == menuId).ToList();
        }

        public ProductInMenu GetProductInMenu(int menuId, int productId)
        {
            return _officeSupplyDB.ProductInMenus.FirstOrDefault(p => p.MenuID == menuId && p.ProductID == productId);
        }
    }
}
