using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Repository;

namespace DAL.Data
{
    public class AdminRepository : IAdminRepository
    {
        private readonly InsuranceDbContext _context;

        public AdminRepository(InsuranceDbContext context)
        {
            _context = context;
        }

        // Create
        public Admin CreateAdmin(Admin admin)
        {
            _context.Admins.Add(admin);
            _context.SaveChanges();
            return admin;
        }

        // Read
        public Admin GetAdminById(int adminId)
        {
            return _context.Admins.Find(adminId);
        }
        public bool AdminExists(string userName)
        {
            return _context.Admins.Any(a => a.UserName == userName);
        }

        public bool AdminExistsEmail(string UserEmail)
        {
            return _context.Admins.Any(a => a.UserName == UserEmail);
        }
        public IEnumerable<Admin> GetAllAdmins()
        {
            return _context.Admins.ToList();
        }

        // Update
        public Admin UpdateAdmin(Admin admin)
        {
            var existingAdmin = _context.Admins.Find(admin.Id);

            if (existingAdmin != null)
            {
                // Update the properties of the existing admin with the values from the input admin
                existingAdmin.FirstName = admin.FirstName;
                existingAdmin.LastName = admin.LastName;
                existingAdmin.Email = admin.Email;
                existingAdmin.PhoneNumber = admin.PhoneNumber;
                existingAdmin.UserName = admin.UserName;
                existingAdmin.Password = admin.Password;
                existingAdmin.RoleId = admin.RoleId;

                _context.SaveChanges();
            }

            return existingAdmin;
        }

        // Delete
        public Admin DeleteAdmin(int adminId)
        {
            var admin = _context.Admins.Find(adminId);

            if (admin != null)
            {
                _context.Admins.Remove(admin);
                _context.SaveChanges();
            }

            return admin;
        }

        public Admin GetAdminByUserName(string userName)
        {
            return _context.Admins.FirstOrDefault(x => x.UserName == userName);
        }
        public Admin GetAdminByUserNamePhone(string userName, string phoneNumber)
        {
            return _context.Admins.FirstOrDefault(a => a.UserName == userName && a.PhoneNumber == phoneNumber);
        }
        public void SaveAdminchages()
        {
            _context.SaveChanges();
        }
    }
}
