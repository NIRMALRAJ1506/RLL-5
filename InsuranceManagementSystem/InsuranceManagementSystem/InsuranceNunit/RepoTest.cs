using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using DAL.Data;
using DAL.Repository;
using Moq;
using NUnit.Framework;


namespace InsuranceNunit
{
    [TestFixture]
    public class AdminRepositoryTests
    {
        private Mock<InsuranceDbContext> mockContext;
        private IAdminRepository adminRepository;

        [SetUp]
        public void Setup()
        {
            mockContext = new Mock<InsuranceDbContext>();
            adminRepository = new AdminRepository(mockContext.Object);
        }

        [Test]
        public void CreateAdmin_ShouldAddAdminToContext()
        {
            // Arrange
            var admin = new Admin { Id=2, FirstName = "Suresh", LastName="R", Email="suresh@gmail.com",  UserName="adminsur", Password="Password@142", PhoneNumber="9977553311", RoleId=1};

            // Act
            adminRepository.CreateAdmin(admin);

            // Assert
            mockContext.Verify(c => c.Admins.Add(It.IsAny<Admin>()), Times.Once);
            mockContext.Verify(c => c.SaveChanges(), Times.Once);
        }


        [Test]
        public void GetAdminById_ShouldReturnAdminFromContext()
        {
            // Arrange
            var adminId = 2;
            var expectedAdmin = new Admin { Id = adminId, /* set other admin properties */ };
            mockContext.Setup(c => c.Admins.Find(adminId)).Returns(expectedAdmin);

            // Act
            var result = adminRepository.GetAdminById(adminId);

            // Assert
            Assert.AreEqual(expectedAdmin, result);
        }

        // Similar test cases for other AdminRepository methods...
    }

    [TestFixture]
    public class CustomerRepositoryTests
    {
        private Mock<InsuranceDbContext> mockContext;
        private ICustomerRepository customerRepository;

        [SetUp]
        public void Setup()
        {
            mockContext = new Mock<InsuranceDbContext>();
            customerRepository = new CustomerRepository(mockContext.Object);
        }

        [Test]
        public void CreateCustomer_ShouldAddCustomerToContext()
        {
            // Arrange
            var customer = new Customer { Id = 2, FirstName = "Ramesh", LastName = "R", Email = "ramesh@gmail.com", UserName = "ramesh15", Password = "Ramesh@123", PhoneNumber = "9977552211", RoleId = 2 };

            // Act
            customerRepository.CreateCustomer(customer);

            // Assert
            mockContext.Verify(c => c.Customers.Add(It.IsAny<Customer>()), Times.Once);
            mockContext.Verify(c => c.SaveChanges(), Times.Once);
        }

        [Test]
        public void GetCustomerById_ShouldReturnCustomerFromContext()
        {
            // Arrange
            var customerId = 2;
            var expectedCustomer = new Customer { Id = customerId, /* set other customer properties */ };
            mockContext.Setup(c => c.Customers.Find(customerId)).Returns(expectedCustomer);

            // Act
            var result = customerRepository.GetCustomerById(customerId);

            // Assert
            Assert.AreEqual(expectedCustomer, result);
        }

        // Similar test cases for other CustomerRepository methods...
    }
}
