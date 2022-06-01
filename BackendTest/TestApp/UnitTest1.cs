using BackEndTest.API.Controllers;
using BackEndTest.Domain.Entities;
using BackEndTest.Infrastructure.Context;
using BackEndTest.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using Xunit;

namespace TestApp
{
    public class UnitTest1
    {
        private readonly CustomerController _customerController;
        private readonly IRepository<Customer, CustomerDBContext> _CustomerRepo;

        public UnitTest1(CustomerDBContext context, IRepository<Customer, CustomerDBContext> CustomerRepo)
        {
            _CustomerRepo = CustomerRepo;
        _customerController = new CustomerController(context, CustomerRepo, null, null);
        }

       
        [Fact]
        public void Get_WhenCalled_ReturnsOkResult()
        {
            Random random = new Random((int)DateTime.Now.Ticks);
            int Id = Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65));
            var okResult = _customerController.GetCustomerById(Id);
            // Assert
            Assert.IsType<object>(okResult);
        }

        [Fact]
        public void GetById_UnknownIdPassed_ReturnsNotFoundResult()
        {
            // Act
            var notFoundResult = _customerController.GetCustomerById(0);
            // Assert
            Assert.IsType<NotFoundResult>(notFoundResult);
        }

        [Fact]
        public void GetById_ExistingIdPassed_ReturnsOkResult()
        {
            // Arrange
            int Id = 1;
            // Act
            var okResult = _customerController.GetCustomerById(Id);
            // Assert
            Assert.IsType<Customer>(okResult);
        }

        [Fact]
        public void Get_WhenCalled_ReturnsAllItems()
        {
            //Act
            var okResult = _customerController.GetCustomers();
            // Assert
            var items = Assert.IsType<List<Customer>>(okResult);
            Assert.Equal(3, items.Count);
        }

        [Fact]
        public void GetById_ExistingGuidPassed_ReturnsRightItem()
        {
            // Arrange
            int Id = 1;
            // Act
            var okResult = _customerController.GetCustomerById(Id);
            // Assert
            Assert.IsType<Customer>(okResult);
            
        }

        [Fact]
        public void Add_InvalidCustomerPassed_ReturnsBadRequest()
        {
            // Arrange
            var phoneNumberMissingItem = new Customer()
            {
                Firstname = "Tola",
                Lastname = "Sunday"
            };
            var res = _customerController.CreateCustomer(phoneNumberMissingItem);
            _customerController.ModelState.AddModelError("PhoneNumber", "Required");
            // Act
            var badResponse = _customerController.CreateCustomer(phoneNumberMissingItem);
            // Assert
            Assert.IsType<BadRequestObjectResult>(badResponse);
        }

        [Fact]
        public void Add_InvalidCustomerPassedWithLGANotMapped_ReturnsBadRequest()
        {
            // Arrange
            var LGAandStateNotMapped = new Customer()
            {
                Firstname = "Tola",
                Lastname = "Sunday",
                StateofResidence="Lagos",
                LGA = "Laafia"
            };
            var res = _customerController.CreateCustomer(LGAandStateNotMapped);
            _customerController.ModelState.AddModelError("LGA", "LGA must map to State");
            // Act
            var badResponse = _customerController.CreateCustomer(LGAandStateNotMapped);
            // Assert
            Assert.IsType<BadRequestObjectResult>(badResponse);
        }

        [Fact]
        public void Add_ValidCustomerPassed_ReturnsCreatedResponse()
        {
            // Arrange
            var testItem = new Customer()
            {
                Firstname = "Sunday",
                Lastname = "Oladiran",
                PhoneNumber = "08032229622",
                LGA ="Mushin",
                StateofResidence="Lagos"
            };
            // Act
            var createdResponse = _customerController.CreateCustomer(testItem);
            // Assert
            Assert.IsType<CreatedAtActionResult>(createdResponse);
        }
      

        [Fact]
        public void Test1()
        {
            DataTable dt = new DataTable();
            Assert.Throws<InvalidOperationException>(() => dt.Rows.Count > 0);
        }
    }
}
