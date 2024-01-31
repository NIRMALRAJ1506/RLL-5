using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Identity;
using System.Data.Entity;
using PasswordVerificationResult = Microsoft.AspNetCore.Identity.PasswordVerificationResult;

namespace DAL.Service
{
    public static class Authentication
    {
        private static readonly InsuranceDbContext context = new InsuranceDbContext();


        public static bool VerifyAdminCredentials(string username, string password)
        {
            // Your logic to check username and password in the database
            var admin = context.Admins.FirstOrDefault(a => a.UserName == username);

            if (admin != null)
            {
                if(admin.Password == password)
                {
                    return true;
                }
            }

            return false; // Invalid username or password
        }
        public static bool VerifyCustomerCredentials(string username, string password)
        {
            // Your logic to check username and password in the database
            var cutomer = context.Customers.FirstOrDefault(a => a.UserName == username);

            if (cutomer != null)
            {
                if(cutomer.Password == password)
                {
                    return true;
                }
            }

            return false; // Invalid username or password
        }
    }
}
