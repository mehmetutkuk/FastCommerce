using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FastCommerce.Web.API.Models
{
    public class Response<T> : BaseResponse
    {

        public T Data { get; set; }
        public List<T> DataList { get; set; }
        public int EntitiesCount { get; set; }

        public Response()
        {
            Errors = new List<Exception>();
            DataList = new List<T>();
        }

    }
}
