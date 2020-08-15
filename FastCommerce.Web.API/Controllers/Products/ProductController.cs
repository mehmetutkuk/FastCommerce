using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FastCommerce.Business.ProductManager;
using FastCommerce.Entities.Entities;
using FastCommerce.Web.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FastCommerce.Web.API.Controllers.Products
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductController : ControllerBase
    {

        [ActionName("Save"), Route("Save")]
        [HttpPost]
        public async Task<HttpResponseMessage> SaveAsync(Product product) {
            Response<Product> httpResponse = new Response<Product>();
            try
            {
                httpResponse.RequestState= true;
           //     await ProductManager.SaveProduct(product);
                httpResponse.ErrorState = false;
            }
            catch (Exception ex)
            {
                httpResponse.ErrorState = true;
                httpResponse.ErrorList.Add(ex);
            }
            return httpResponse;
        }    
    
  
    }
}
