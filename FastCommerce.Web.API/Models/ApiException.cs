using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FastCommerce.Web.API.Models
{
    public class ApiException
    {
        public string Message { get; set; }
        public string Detail { get; set; }
    }
}
