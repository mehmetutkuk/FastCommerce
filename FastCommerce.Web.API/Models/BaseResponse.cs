using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace FastCommerce.Web.API.Models
{
    public abstract class BaseResponse : HttpResponseMessage
    {
            public List<ApiException> ErrorList { get; set; }
            public bool ErrorState { get; set; }
            public bool RequestState { get; set; }
    }
    
}
