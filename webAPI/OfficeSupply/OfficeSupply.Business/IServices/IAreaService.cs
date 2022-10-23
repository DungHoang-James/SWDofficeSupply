using OfficeSupply.Business.Ultility.common;
using OfficeSupply.Data.Entities;
using OfficeSupply.Data.Models.DTOs;
using OfficeSupply.Data.Models.DTOs.statistics;
using OfficeSupply.Data.Models.Requests;
using OfficeSupply.Data.Models.ResourceParams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfficeSupply.Business.IServices
{
    public interface IAreaService
    {
        public Task<GenericResult> GetArea(int id);
        public Task<GenericResult> CreateArea(AreaPayload areaPayload);
        public Task<GenericResult> UpdateArea(int id, AreaDTO areaPayload);
        public Task<GenericResult> DeleteArea(int id);
        public Task<GenericResult> GetAreas(AreaResourceParam resourceParams);
        List<AreaStatisticsDTO> GetAreaStatistics();
    }
}
