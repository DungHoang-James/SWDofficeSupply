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
    public interface ISupplierService
    {
        public Task<GenericResult> GetSupplier(int id);
        public Task<GenericResult> CreateSupplier(SupplierPayload supplierPayload);
        public Task<GenericResult> UpdateSupplier(int id, SupplierDTO supplierPayload);
        public Task<GenericResult> DeleteSupplier(int id);
        public Task<GenericResult> GetSuppliers(SupplierResourceParam resourceParams);
    }
}
