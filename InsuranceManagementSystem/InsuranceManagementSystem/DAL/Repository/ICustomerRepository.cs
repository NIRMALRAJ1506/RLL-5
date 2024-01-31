using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public interface ICustomerRepository
    {
        // Create

        int customerSAveChanges();

        Customer GetCustomerByUserName(string userName);
        Customer CreateCustomer(Customer customer);
        bool CustomerExistsEmail(string Email);
        // Read
        Customer GetCustomerById(int customerId);
        IEnumerable<Customer> GetAllCustomers();
        bool CustomerExists(string userName);
        // Update
        Customer UpdateCustomer(Customer customer);
        //int GetCurrentCustomerId(string userName);
        // Delete
        Customer DeleteCustomer(int customerId);

        Customer GetCustomerByUserNamePhone(string UserName, string PhoneNumber);

    }
}
