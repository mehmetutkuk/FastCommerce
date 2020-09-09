using FastCommerce.Web.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FastCommerce.Web.API.Interfaces
{
    public interface IBaseResponse
    {
        public List<ApiException> ErrorList { get; set; }
        public bool ErrorState { get; set; }
        public bool RequestState { get; set; }
    }
}
