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
        public int EntitiesCount { get; set; }
        public List<Exception> Errors { get; set; }
        public Response()
        {
            Errors = new List<Exception>();
            DataList = new List<T>();
        }

    }

    public interface IResponse<T> : IBaseResponse where T : class
    {

        public T Data { get; set; }
        public List<T> DataList { get; set; }
        public int EntitiesCount { get; set; }
        public List<Exception> Errors { get; set; }
    }

}
