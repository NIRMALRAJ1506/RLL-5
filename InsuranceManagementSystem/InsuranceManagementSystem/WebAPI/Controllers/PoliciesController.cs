using System.Collections.Generic;
using System.Web.Http;
using DAL;
using WebAPI.Services;

namespace WebAPI.Controllers
{
    public class PoliciesController : ApiController
    {
        private InsuranceService _insuranceService;

        public PoliciesController()
        {
            _insuranceService = new InsuranceService(new InsuranceDAL(new InsuranceDbContext()));
        }

        public IEnumerable<Policy> GetPolicies()
        {
            return _insuranceService.GetAllPolicies();
        }

        public IHttpActionResult GetPolicy(int id)
        {
            Policy policy = _insuranceService.GetPolicyById(id);
            if (policy == null)
            {
                return NotFound();
            }

            return Ok(policy);
        }

        public IHttpActionResult PostPolicy(Policy policy)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _insuranceService.AddPolicy(policy);

            return CreatedAtRoute("DefaultApi", new { id = policy.PolicyId }, policy);
        }

        public IHttpActionResult PutPolicy(int id, Policy policy)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != policy.PolicyId)
            {
                return BadRequest();
            }

            _insuranceService.UpdatePolicy(policy);

            return StatusCode(System.Net.HttpStatusCode.NoContent);
        }

        public IHttpActionResult DeletePolicy(int id)
        {
            Policy policy = _insuranceService.GetPolicyById(id);
            if (policy == null)
            {
                return NotFound();
            }

            _insuranceService.DeletePolicy(id);

            return Ok(policy);
        }
    }
}