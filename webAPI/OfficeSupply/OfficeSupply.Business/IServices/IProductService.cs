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
    public interface IProductService
    {
        public Task<GenericResult> GetProduct(int id);
        public Task<GenericResult> CreateProduct(ProductPayload productPayload);
        public Task<GenericResult> UpdateProduct(int id, ProductUpdatePayload productPayload);
        public Task<GenericResult> DeleteProduct(int id);
        public Task<GenericResult> GetProducts(ProductResourceParam resourceParams);
    }
}
