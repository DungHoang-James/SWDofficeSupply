using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfficeSupply.Data.Models.ResourceParams
{
    public class ResourceParams
    {
        const int maxPageSize = 20;
        private int _pageSize = 10;
        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > maxPageSize) ? maxPageSize : value;
        }
        public int PageNumber { get; set; } = 1;
        public string OrderBy { get; set; }
    }
}
