using FastCommerce.Business.ObjectDtos.Product;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FastCommerce.Business.ProductManager.Abstract
{
    public interface IProductManager
    {
        public  Task<bool> CreateIndexes(ProductElasticIndexDto productElasticIndexDto);
    }
}
