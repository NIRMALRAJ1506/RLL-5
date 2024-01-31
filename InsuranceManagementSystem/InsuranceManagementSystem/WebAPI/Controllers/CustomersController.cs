using System.Collections.Generic;
using System.Web.Http;
using DAL;
using WebAPI.Services;

namespace WebAPI.Controllers
{
    public class CustomersController : ApiController
    {
        private InsuranceService _insuranceService;

        public CustomersController()
        {
            _insuranceService = new InsuranceService(new InsuranceDAL(new InsuranceDbContext()));
        }

        public IEnumerable<Customer> GetCustomers()
        {
            return _insuranceService.GetAllCustomers();
        }

        public IHttpActionResult GetCustomer(int id)
        {
            Customer customer = _insuranceService.GetCustomerById(id);
            if (customer == null)
            {
                return NotFound();
            }

            return Ok(customer);
        }

        public IHttpActionResult PostCustomer(Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _insuranceService.AddCustomer(customer);

            return CreatedAtRoute("DefaultApi", new { id = customer.Id }, customer);
        }

        public IHttpActionResult PutCustomer(int id, Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != customer.Id)
            {
                return BadRequest();
            }

            _insuranceService.UpdateCustomer(customer);

            return StatusCode(System.Net.HttpStatusCode.NoContent);
        }

        public IHttpActionResult DeleteCustomer(int id)
        {
            Customer customer = _insuranceService.GetCustomerById(id);
            if (customer == null)
            {
                return NotFound();
            }

            _insuranceService.DeleteCustomer(id);

            return Ok(customer);
        }
    }
}