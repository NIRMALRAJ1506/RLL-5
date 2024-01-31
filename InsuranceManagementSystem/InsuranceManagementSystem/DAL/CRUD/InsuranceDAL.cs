using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace DAL
{
    public class InsuranceDAL
    {
        private readonly InsuranceDbContext _context;

        public InsuranceDAL(InsuranceDbContext context)
        {
            _context = context;
        }

        // Admin operations
        public List<Admin> GetAdmins()
        {
            return _context.Admins.ToList();
        }

        public Admin GetAdminById(int adminId)
        {
            return _context.Admins.Find(adminId);
        }

        public void AddAdmin(Admin admin)
        {
            _context.Admins.Add(admin);
            _context.SaveChanges();
        }

        public void UpdateAdmin(Admin admin)
        {
            _context.Entry(admin).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void DeleteAdmin(int adminId)
        {
            Admin admin = _context.Admins.Find(adminId);
            if (admin != null)
            {
                _context.Admins.Remove(admin);
                _context.SaveChanges();
            }
        }

        // Customer operations
        public List<Customer> GetCustomers()
        {
            return _context.Customers.ToList();
        }

        public Customer GetCustomerById(int customerId)
        {
            return _context.Customers.Find(customerId);
        }

        public void AddCustomer(Customer customer)
        {
            _context.Customers.Add(customer);
            _context.SaveChanges();
        }

        public void UpdateCustomer(Customer customer)
        {
            _context.Entry(customer).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void DeleteCustomer(int customerId)
        {
            Customer customer = _context.Customers.Find(customerId);
            if (customer != null)
            {
                _context.Customers.Remove(customer);
                _context.SaveChanges();
            }
        }

        // Policy operations
        public List<Policy> GetPolicies()
        {
            return _context.Policies.ToList();
        }

        public Policy GetPolicyById(int policyId)
        {
            return _context.Policies.Find(policyId);
        }

        public void AddPolicy(Policy policy)
        {
            _context.Policies.Add(policy);
            _context.SaveChanges();
        }

        public void UpdatePolicy(Policy policy)
        {
            _context.Entry(policy).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void DeletePolicy(int policyId)
        {
            Policy policy = _context.Policies.Find(policyId);
            if (policy != null)
            {
                _context.Policies.Remove(policy);
                _context.SaveChanges();
            }
        }
    }
}
