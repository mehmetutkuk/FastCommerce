using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FastCommerce.Business.ProductManager;
using FastCommerce.Web.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FastCommerce.Web.API.Controllers.Products
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {

        [ActionName("Save"), Route("Save")]
        [HttpPost]
        public async Task<HttpResponseMessage> SaveAsync(Entities.Entities.Product product) {
            Response<int> httpResponse = new Response<int>();
            try
            {
                httpResponse.RequestState= true;
           //     await ProductManager.SaveProduct(product);
                httpResponse.ErrorState = false;
            }
            catch (Exception ex)
            {
                httpResponse.ErrorState = true;
                httpResponse.Errors.Add(ex);
            }
            return httpResponse;
        }    
    
  
    }
}
