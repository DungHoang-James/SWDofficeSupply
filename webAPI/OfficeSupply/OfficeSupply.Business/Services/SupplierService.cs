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
    public class SupplierService : ISupplierService
    {
        private readonly ISupplierRepository _supplierRepository;
        private readonly IMapper _mapper;

        public SupplierService(ISupplierRepository supplierRepository, IMapper mapper)
        {
            this._supplierRepository = supplierRepository;
            this._mapper = mapper;
        }

        public async Task<GenericResult> GetSuppliers(SupplierResourceParam resourceParams)
        {
            if (resourceParams.All)
            {
                var sup = _supplierRepository.GetAll();
                return ApiResponse.Ok(resData: _mapper.Map<List<SupplierMinDTO>>(sup));
            }

            var suppliers = _supplierRepository.GetSuppliers(resourceParams);
            Pagination<SupplierDTO> result = new(_mapper.Map<List<SupplierDTO>>(suppliers.Items),
                                            suppliers.TotalCount,
                                            suppliers.CurrentPage,
                                            suppliers.PageSize);
            return ApiResponse.Ok(resData: result, totalCount: suppliers.TotalCount);
        }

        public async Task<GenericResult> GetSupplier(int id)
        {
            var supplier = _supplierRepository.GetById(id);
            if (supplier == null)
            {
                return ApiResponse.Error(messCode: OfficeException.CommonException.RecordNotFound);
            }
            var result = _mapper.Map<SupplierDTO>(supplier);
            return ApiResponse.Ok(result);
        }

        public async Task<GenericResult> CreateSupplier(SupplierPayload supplierPayload)
        {
            var supplier = _mapper.Map<Supplier>(supplierPayload);
            var result = _supplierRepository.Create(supplier);
            if (result == false)
            {
                return ApiResponse.Error(messCode: OfficeException.CommonException.RecordNotFound, messDetail: "Create supplier fail");
            }
            return ApiResponse.Ok(_mapper.Map<SupplierDTO>(supplier));
        }

        public async Task<GenericResult> UpdateSupplier(int id, SupplierDTO supplierPayload)
        {
            var supplier = _supplierRepository.GetById(id);
            if (supplier == null) return ApiResponse.Error(messCode: OfficeException.CommonException.RecordNotFound);
            _mapper.Map(supplierPayload, supplier);
            var result = _supplierRepository.Update(supplier);
            if (result == false)
            {
                return ApiResponse.Error(messCode: OfficeException.CommonException.RecordNotFound, messDetail: "Update supplier fail");
            }
            return ApiResponse.Ok();
        }

        public async Task<GenericResult> DeleteSupplier(int id)
        {
            var supplier = _supplierRepository.GetById(id);
            if (supplier == null) return ApiResponse.Error(messCode: OfficeException.CommonException.RecordNotFound);
            supplier.IsDelete = true;
            var result = _supplierRepository.Update(supplier);
            if (result == false)
            {
                return ApiResponse.Error(messCode: OfficeException.CommonException.RecordNotFound, messDetail: "Delete supplier fail");
            }
            return ApiResponse.Ok();
        }
    }
}
