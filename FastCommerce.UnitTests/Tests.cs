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
using System.Reflection;
using FastCommerce.Business.ObjectDtos.Product;
using Mapster;

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

            var products = GetFakeProductData(26);

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
        public async Task GetByCategories()
        {
            var products = GetFakeProductData(5);
            var categories = GetFakeCategoryData(2);
            GetByCategoriesRequest getByCategoriesRequest = new GetByCategoriesRequest();
            getByCategoriesRequest.Categories.Adapt(categories);

            var mockedService = new Mock<IProductManager>();
            mockedService.Setup(x => x.GetByCategories(getByCategoriesRequest)).Returns(products);
            var controller = new ProductController(mockedService.Object);

            var result = await controller.GetByCategories(getByCategoriesRequest);

            Assert.AreEqual(5, result.DataList.Count);
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

        private List<Category> GetFakeCategoryData(int count)
        {
            var i = 1;
            var categories= A.ListOf<Category>(count);
            categories.ForEach(x => x.CategoryID = i++);
            return categories.Select(_ => _).ToList();
        }
        private async Task<List<Product>> GetFakeProductData(int count)
        {
            var i = 1;
            var persons = A.ListOf<Product>(count);
            persons.ForEach(x => x.ProductId = i++);
            List<Product> lists = persons.Select(_ => _).ToList();
            return await Task.FromResult(lists);
        }
    }
}