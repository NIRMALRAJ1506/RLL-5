using System.Collections.Generic;
using DAL;

namespace WebAPI.Services
{
    public class InsuranceService
    {
        private InsuranceDAL _insuranceDAL;

        public InsuranceService(InsuranceDAL insuranceDAL)
        {
            _insuranceDAL = insuranceDAL;
        }

        // Admin operations
        public List<Admin> GetAllAdmins()
        {
            return _insuranceDAL.GetAdmins();
        }

        public Admin GetAdminById(int adminId)
        {
            return _insuranceDAL.GetAdminById(adminId);
        }

        public void AddAdmin(Admin admin)
        {
            _insuranceDAL.AddAdmin(admin);
        }

        public void UpdateAdmin(Admin admin)
        {
            _insuranceDAL.UpdateAdmin(admin);
        }

        public void DeleteAdmin(int adminId)
        {
            _insuranceDAL.DeleteAdmin(adminId);
        }

        // Customer operations
        public List<Customer> GetAllCustomers()
        {
            return _insuranceDAL.GetCustomers();
        }

        public Customer GetCustomerById(int customerId)
        {
            return _insuranceDAL.GetCustomerById(customerId);
        }

        public void AddCustomer(Customer customer)
        {
            _insuranceDAL.AddCustomer(customer);
        }

        public void UpdateCustomer(Customer customer)
        {
            _insuranceDAL.UpdateCustomer(customer);
        }

        public void DeleteCustomer(int customerId)
        {
            _insuranceDAL.DeleteCustomer(customerId);
        }

        // Policy operations
        public List<Policy> GetAllPolicies()
        {
            return _insuranceDAL.GetPolicies();
        }

        public Policy GetPolicyById(int policyId)
        {
            return _insuranceDAL.GetPolicyById(policyId);
        }

        public void AddPolicy(Policy policy)
        {
            _insuranceDAL.AddPolicy(policy);
        }

        public void UpdatePolicy(Policy policy)
        {
            _insuranceDAL.UpdatePolicy(policy);
        }

        public void DeletePolicy(int policyId)
        {
            _insuranceDAL.DeletePolicy(policyId);
        }
    }
}
