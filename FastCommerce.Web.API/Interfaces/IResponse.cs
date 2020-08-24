using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FastCommerce.Web.API.Interfaces
{
    public interface IResponse<T> : IBaseResponse where T : class
    {
        public T Data { get; set; }
        public List<T> DataList { get; set; }
        public int EntityCount { get; set; }
    }
}
