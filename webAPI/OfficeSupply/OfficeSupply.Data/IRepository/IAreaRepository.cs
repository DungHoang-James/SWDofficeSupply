using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OfficeSupply.Data.Entities;
using OfficeSupply.Data.Helper;
using OfficeSupply.Data.Models.DTOs;
using OfficeSupply.Data.Models.DTOs.statistics;
using OfficeSupply.Data.Models.Requests;
using OfficeSupply.Data.Models.ResourceParams;

namespace OfficeSupply.Data.IRepository
{
    public interface IAreaRepository : IBaseRepository<Area>
    {
        Pagination<Area> GetAreas(AreaResourceParam resourceParams);
        List<Area> GetAreasMin();
        List<AreaStatisticsDTO> GetAreaStatistics();
    }
}
