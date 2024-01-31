using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Repository;

namespace DAL.Data
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly InsuranceDbContext _context;

        public CustomerRepository(InsuranceDbContext context)
        {
            _context = context;
        }

        public Customer GetCustomerByUserName(string userName)
        {
            return _context.Customers.FirstOrDefault(x => x.UserName == userName);
        }
        // Create
        public Customer CreateCustomer(Customer customer)
        {
            _context.Customers.Add(customer);
            _context.SaveChanges();
            return customer;
        }

        // Read
        public Customer GetCustomerById(int customerId)
        {
            return _context.Customers.Find(customerId);
        }

        public IEnumerable<Customer> GetAllCustomers()
        {
            return _context.Customers.ToList();
        }

        // Update
        public Customer UpdateCustomer(Customer customer)
        {
            var existingCustomer = _context.Customers.Find(customer.Id);

            if (existingCustomer != null)
            {
                // Update the properties of the existing customer with the values from the input customer
                existingCustomer.FirstName = customer.FirstName;
                existingCustomer.LastName = customer.LastName;
                existingCustomer.Email = customer.Email;
                existingCustomer.PhoneNumber = customer.PhoneNumber;
                existingCustomer.UserName = customer.UserName;
                existingCustomer.Password = customer.Password;
                existingCustomer.RoleId = customer.RoleId;

                _context.SaveChanges();
            }

            return existingCustomer;
        }

        // Delete
        public Customer DeleteCustomer(int customerId)
        {
            var customer = _context.Customers.Find(customerId);

            if (customer != null)
            {
                _context.Customers.Remove(customer);
                _context.SaveChanges();
            }

            return customer;
        }

        public int customerSAveChanges()
        {
            return _context.SaveChanges();
        }
        public bool CustomerExists(string userName)
        {
            return _context.Customers.Any(a => a.UserName == userName);
        }

        //public int GetCurrentCustomerId(string userName)
        //{
        //    // Implement the logic to get the customer's ID based on the username
        //    var customer = _context.Customers.FirstOrDefault(c => c.UserName == userName);

        //    return customer?.CustomerId ?? 0; // Return 0 or another default value if not found
        //}
        public Customer GetCustomerByUserNamePhone(string UserName, string PhoneNumber)
        {
            return _context.Customers.FirstOrDefault(a => a.UserName == UserName && a.PhoneNumber == PhoneNumber);
        }


        public bool CustomerExistsEmail(string Email)
        {
            return _context.Customers.Any(a => a.Email == Email);
        }
    }

}
