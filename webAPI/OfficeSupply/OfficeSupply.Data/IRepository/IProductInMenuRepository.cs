using OfficeSupply.Data.Entities;
using OfficeSupply.Data.Helper;
using OfficeSupply.Data.Models.DTOs;
using OfficeSupply.Data.Models.ResourceParams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfficeSupply.Data.IRepository
{
    public interface IProductInMenuRepository : IBaseRepository<ProductInMenu>
    {
        Pagination<ProductInMenu> GetProductsInMenu(int menuId, ProductResourceParam resourceParam);
        Pagination<ProductInMenu> GetProductsInMenuIgnoreStatus(int menuId, ProductInMenuResourceParam resourceParams);
        List<ProductInMenu> GetProductsInMenuMin(int menuId);
        ProductInMenu GetProductInMenu(int menuId, int productId);
    }
}
