using FastCommerce.Web.API.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FastCommerce.Web.API.Models
{
    public class Response<T> : BaseResponse, IResponse<T> where T : class
    {
        public T Data { get; set; }
        public List<T> DataList { get; set; }
        public int EntityCount { get; set; }
        public Response()
        {
            ErrorList = new List<ApiException>();
            DataList = new List<T>();
        }
    }
}
