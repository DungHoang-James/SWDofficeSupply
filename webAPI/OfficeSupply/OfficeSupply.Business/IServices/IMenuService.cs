using OfficeSupply.Business.Ultility.common;
using OfficeSupply.Data.Models.DTOs;
using OfficeSupply.Data.Models.Requests;
using OfficeSupply.Data.Models.ResourceParams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfficeSupply.Business.IServices
{
    public interface IMenuService
    {
        public Task<GenericResult> GetMenu(int id);
        public Task<GenericResult> DeleteMenu(int id);
        public Task<GenericResult> AddMenu(MenuPayload menuPayload);
        public Task<GenericResult> UpdateMenu(int id, MenuDTO menuPayload);
        public Task<GenericResult> GetMenus(MenuResourceParam resourceParams);
        Task<GenericResult> GetProductInMenu(int menuId, ProductInMenuResourceParam resourceParam);
        Task<GenericResult> AddProductInMenu(int menuId, ProductInMenuPayload productInMenuPayload);
        Task<GenericResult> UpdateProductInMenu(int menuId, ProductInMenuUpdatePayload productInMenuPayload);
    }
}
