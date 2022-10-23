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
    public class AreaService : IAreaService
    {

        private readonly IAreaRepository _areaRepository;
        private readonly ISupplierInAreaRepository _supplierInAreaRepository;
        private readonly IMapper _mapper;

        public AreaService(IAreaRepository areaRepository,
                           ISupplierInAreaRepository supplierInAreaRepository,
                           IMenuRepository menuRepository,
                           IMapper mapper)
        {
            this._areaRepository = areaRepository;
            this._supplierInAreaRepository = supplierInAreaRepository;
            this._mapper = mapper;
        }

        public async Task<GenericResult> GetAreas(AreaResourceParam resourceParams)
        {
            if (resourceParams.All)
            {
                var areasMin = _areaRepository.GetAreasMin();
                return ApiResponse.Ok(resData: _mapper.Map<List<AreaMinDTO>>(areasMin));
            }

            var areas = _areaRepository.GetAreas(resourceParams);
            Pagination<AreaDTO> result = new(_mapper.Map<List<AreaDTO>>(areas.Items),
                                            areas.TotalCount,
                                            areas.CurrentPage,
                                            areas.PageSize);
            return ApiResponse.Ok(resData: result, totalCount: areas.TotalCount);
        }

        public async Task<GenericResult> GetArea(int id)
        {
            Area area = _areaRepository.GetById(id);
            if (area == null)
            {
                return ApiResponse.Error(messCode: OfficeException.CommonException.RecordNotFound);
            }
            var result = _mapper.Map<AreaDTO>(area);
            return ApiResponse.Ok(result);
        }

        public async Task<GenericResult> CreateArea(AreaPayload areaPayload)
        {
            var areaDTO = _mapper.Map<AreaDTO>(areaPayload);
            var area = _mapper.Map<Area>(areaDTO);
            var result = _areaRepository.Create(area);
            if (!result)
            {
                return ApiResponse.Error(messCode: OfficeException.CommonException.RecordNotFound, messDetail: "Add Area fail");
            }

            return ApiResponse.Ok(_mapper.Map<AreaDTO>(area));
        }

        public async Task<GenericResult> DeleteArea(int id)
        {
            Area area = _areaRepository.GetById(id);
            if (area == null) return ApiResponse.Error(messCode: OfficeException.CommonException.RecordNotFound);
            area.IsDelete = true;
            var result = _areaRepository.Update(area);
            if (!result)
            {
                return ApiResponse.Error(messCode: OfficeException.CommonException.RecordNotFound, messDetail: "delete Area fail");
            }
            return ApiResponse.Ok();
        }

        public async Task<GenericResult> UpdateArea(int id, AreaDTO areaPayload)
        {
            var area = _areaRepository.GetById(id);

            if (area == null) return ApiResponse.Error(messCode: OfficeException.CommonException.RecordNotFound);

            _mapper.Map(areaPayload, area);
            var result = _areaRepository.Update(area);
            if (!result)
            {
                return ApiResponse.Error(messCode: OfficeException.CommonException.RecordNotFound, messDetail: "Update Area fail");
            }
            return ApiResponse.Ok();
        }

        public List<AreaStatisticsDTO> GetAreaStatistics()
        {
            return _areaRepository.GetAreaStatistics();
        }
    }
}
