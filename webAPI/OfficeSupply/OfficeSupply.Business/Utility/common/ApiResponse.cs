using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfficeSupply.Business.Ultility.common
{
    public class ApiResponse
    {
        public static GenericResult Ok(object resData = null, string messCode = "", string messDetail = "", long totalCount = 0)
        {
            return new GenericResult(messCode, messDetail, resData, totalCount, true);
        }
        public static GenericResult Error(object resData = null, string messCode = "", string messDetail = "")
        {
            return new GenericResult(messCode, messDetail, resData, 0, false);
        }
    }
}