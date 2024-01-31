using System.Collections.Generic;
using System.Web.Http;
using DAL;
using WebAPI.Services;

namespace WebAPI.Controllers
{
    public class AdminsController : ApiController
    {
        private InsuranceService _insuranceService;

        // Parameterless constructor for framework use
        public AdminsController()
        {
            // You can create a default instance or leave it empty based on your requirement
            _insuranceService = new InsuranceService(new InsuranceDAL(new InsuranceDbContext()));
        }

        // Constructor with parameter for dependency injection
        public AdminsController(InsuranceService insuranceService)
        {
            _insuranceService = insuranceService;
        }

        // GET: api/Admins
        public IEnumerable<Admin> GetAdmins()
        {
            return _insuranceService.GetAllAdmins();
        }

        // GET: api/Admins/5
        public IHttpActionResult GetAdmin(int id)
        {
            Admin admin = _insuranceService.GetAdminById(id);
            if (admin == null)
            {
                return NotFound();
            }

            return Ok(admin);
        }

        // POST: api/Admins
        public IHttpActionResult PostAdmin(Admin admin)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _insuranceService.AddAdmin(admin);

            return CreatedAtRoute("DefaultApi", new { id = admin.Id }, admin);
        }

        // PUT: api/Admins/5
        public IHttpActionResult PutAdmin(int id, Admin admin)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != admin.Id)
            {
                return BadRequest();
            }

            _insuranceService.UpdateAdmin(admin);

            return StatusCode(System.Net.HttpStatusCode.NoContent);
        }

        // DELETE: api/Admins/5
        public IHttpActionResult DeleteAdmin(int id)
        {
            Admin admin = _insuranceService.GetAdminById(id);
            if (admin == null)
            {
                return NotFound();
            }

            _insuranceService.DeleteAdmin(id);

            return Ok(admin);
        }
    }
}
