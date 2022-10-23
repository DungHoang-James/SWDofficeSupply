using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using OfficeSupply.Business.Ultility.common;
using OfficeSupply.Business.Utility.common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OfficeSupply.API.ActionFilters
{
    public class ApiAuthenticateFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            try
            {
                if(context.Result is ObjectResult objectResult)
                {
                    if(objectResult.Value is GenericResult result && !result.IsSuccess)
                    {
                        switch (result.MessageCode)
                        {
                            case OfficeException.CommonException.RecordNotFound:
                                context.Result = new NotFoundObjectResult("the record not found");
                                break;
                            case OfficeException.CommonException.Unauthorized:
                                context.Result = new UnauthorizedObjectResult("Permission denied, user not allow access");
                                break;
                        }
                    }
                }

            }
            catch
            {

            }
        }
    }
}
