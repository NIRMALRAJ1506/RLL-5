using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UI.Models;

namespace UI.Controllers
{
    public class PolicyController : Controller
    {
        private InsuranceDbContext dbContext; 

        public PolicyController()
        {
            dbContext = new InsuranceDbContext(); 
        }

        // Action method to show all policies
        public ActionResult ShowAllPolicy()
        {
            var policies = dbContext.Policies.ToList();

            return View(policies);
        }


        public ActionResult AddPolicy()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddPolicy(PolicyViewModel policyViewModel)
        {
            if (ModelState.IsValid)
            {
                // Convert the view model to the data model before saving to the database
                Policy newPolicy = new Policy
                {
                    PolicyNumber = policyViewModel.PolicyNumber,

                    AppliedDate = policyViewModel.AppliedDate,
                    Category = policyViewModel.Category,
                    PolicyType = policyViewModel.PolicyType,
                    Price = policyViewModel.Price
                };

                // Add logic to save the new policy to the database
                dbContext.Policies.Add(newPolicy);
                dbContext.SaveChanges();

                // Redirect to a success view or another action
                return RedirectToAction("AddPolicySuccess");
            }

            // If the model is not valid, redisplay the form with validation errors
            return View(policyViewModel);
        }
        public ActionResult AddPolicySuccess()
        {
            return View();
        }

        /// <summary>
        /// /////////////////////////////////////////////////////////////////

        public ActionResult ShowAllPoliciesEdit()
        {
            // Retrieve a list of all policies from the database
            List<Policy> allPolicies = dbContext.Policies.ToList();

            // Map the list of policies to a list of view models
            List<PolicyViewModel> viewModels = allPolicies.Select(policy => new PolicyViewModel
            {
                PolicyNumber = policy.PolicyNumber,
                AppliedDate = policy.AppliedDate,
                Category = policy.Category,
                PolicyType = policy.PolicyType,
                Price = policy.Price
            }).ToList();

            return View(viewModels);
        }


        public ActionResult EditPolicy(string policyNumber)
        {
            // Check if policyNumber is null or empty
            if (string.IsNullOrEmpty(policyNumber))
            {
                // Handle the case where policyNumber is not provided or is invalid
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }

            // Retrieve the policy from the database based on the provided policyNumber
            Policy policyToEdit = dbContext.Policies.FirstOrDefault(p => p.PolicyNumber == policyNumber);

            // Check if the policy exists
            if (policyToEdit == null)
            {
                return HttpNotFound();
            }

            // Map the data from the data model to the view model
            PolicyViewModel viewModel = new PolicyViewModel
            {
                PolicyNumber = policyToEdit.PolicyNumber,
                AppliedDate = policyToEdit.AppliedDate,
                Category = policyToEdit.Category,
                PolicyType = policyToEdit.PolicyType,
                Price = policyToEdit.Price
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditPolicy(PolicyViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                // Retrieve the policy from the database based on the provided policyNumber
                Policy policyToEdit = dbContext.Policies.FirstOrDefault(p => p.PolicyNumber == viewModel.PolicyNumber);

                // Check if the policy exists
                if (policyToEdit == null)
                {
                    return HttpNotFound();
                }

                // Update the policy with the new values
                policyToEdit.AppliedDate = viewModel.AppliedDate;
                policyToEdit.Category = viewModel.Category;
                policyToEdit.PolicyType = viewModel.PolicyType;
                policyToEdit.Price = viewModel.Price;
               

                // Save changes to the database
                dbContext.SaveChanges();

                // Redirect to a success view or another action
                return RedirectToAction("EditPolicySuccess");
            }

            // If the model state is not valid, return to the edit view with validation errors
            return View(viewModel);
        }
        public ActionResult EditPolicySuccess()
        {
            return View();
        }




        /////////////////////////////////////////////////////////////////////////////
        ///




        public ActionResult ShowAllPoliciesDelete()
        {
            List<Policy> allPolicies = dbContext.Policies.ToList();
            List<PolicyViewModel> viewModels = allPolicies.Select(policy => new PolicyViewModel
            {
                PolicyNumber = policy.PolicyNumber,
                AppliedDate = policy.AppliedDate,
                Category = policy.Category,
                PolicyType = policy.PolicyType,
                Price = policy.Price
            }).ToList();

            return View(viewModels);
        }

        public ActionResult DeletePolicy(string policyNumber)
        {
            if (string.IsNullOrEmpty(policyNumber))
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }

            Policy policyToDelete = dbContext.Policies.FirstOrDefault(p => p.PolicyNumber == policyNumber);

            if (policyToDelete == null)
            {
                return HttpNotFound();
            }

            PolicyViewModel viewModel = new PolicyViewModel
            {
                PolicyNumber = policyToDelete.PolicyNumber,
                AppliedDate = policyToDelete.AppliedDate,
                Category = policyToDelete.Category,
                PolicyType = policyToDelete.PolicyType,
                Price = policyToDelete.Price
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ConfirmDeletePolicy(string policyNumber)
        {
            if (string.IsNullOrEmpty(policyNumber))
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }

            Policy policyToDelete = dbContext.Policies.FirstOrDefault(p => p.PolicyNumber == policyNumber);

            if (policyToDelete == null)
            {
                return HttpNotFound();
            }

            dbContext.Policies.Remove(policyToDelete);
            dbContext.SaveChanges();

            return RedirectToAction("DeletePolicySuccess");
        }

        public ActionResult DeletePolicySuccess()
        {
            return View();
        }

    }
}
