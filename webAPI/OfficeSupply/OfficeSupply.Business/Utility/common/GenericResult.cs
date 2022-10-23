using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfficeSupply.Business.Ultility.common
{
    public class GenericResult 
    {
        public string MessageCode { get; set; }

        public string MessageDetail { get; set; }

        public object ResponseData { get; set; }

        public long TotatlDataCount { get; set; }

        public bool IsSuccess { get; set; }

        public GenericResult(string messageCode, string messageDetail, object responseData, long totatlDataCount, bool isSuccess)
        {
            MessageCode = messageCode;
            MessageDetail = messageDetail;
            ResponseData = responseData;
            TotatlDataCount = totatlDataCount;
            IsSuccess = isSuccess;
        }


    }
}