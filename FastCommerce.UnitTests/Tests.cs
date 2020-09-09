using FastCommerce.Business.ProductManager.Abstract;
using FastCommerce.Entities.Entities;
using FastCommerce.Web.API.Controllers.Products;
using GenFu;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace FastCommerce.UnitTests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task GetProducts()
        {

            var products = GetFakeData(26);

            var mockedService = new Mock<IProductManager>();
            mockedService.Setup(x => x.Get()).Returns(products);
            var controller = new ProductController(mockedService.Object);
            controller.ModelState.AddModelError("error", "some error");

            // Act
            var result = await controller.Get();

            // Assert
            Assert.IsInstanceOf<HttpResponseMessage>(result);
            Assert.IsInstanceOf<List<Product>>(result.DataList);
            Assert.AreEqual(26, result.DataList.Count);

        }


        [Test]
        public async Task AddProduct()
        {
            var product = A.New<Product>();

            var mockedService = new Mock<IProductManager>();
            mockedService.Setup(x => x.AddProduct(product)).Returns(Task.FromResult(product));
            var controller = new ProductController(mockedService.Object);

            var result = await controller.AddProduct(product);
            Assert.AreEqual(result.Data, product);

        }

        private async Task<List<Product>> GetFakeData(int count)
        {
            var i = 1;
            var persons = A.ListOf<Product>(count);
            persons.ForEach(x => x.ProductId = i++);
            List<Product> lists = persons.Select(_ => _).ToList();
            return await Task.FromResult(lists);
        }
    }
}