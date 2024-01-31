using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UI.Controllers
{
    
    public class AdminController : Controller
    {
        private InsuranceDbContext dbContext;
        public AdminController()
        {
            dbContext = new InsuranceDbContext(); //initialize your DbContext
        }
        // GET: Admin
        public ActionResult Dashboard()
        {
            if (Session["AdminUserId"] != null)
            {
                // User is authenticated, proceed with the action
                return View();
            }
            else
            {
                // User is not authenticated, redirect to login or unauthorized page
                return RedirectToAction("AdminLogin", "Validation");
            }
        }


        public ActionResult GetAllCustomers()
        {
            if (Session["AdminUserId"] != null)
            {
                // User is authenticated, proceed with the action
                var customers = dbContext.Customers.ToList();
                return View(customers);
            }
            else
            {
                // User is not authenticated, redirect to login or unauthorized page
                return RedirectToAction("AdminLogin", "Validation");
            }
        }

        //Action method to get all users
        public ActionResult GetAllUsers()
        {
            if (Session["AdminUserId"] != null)
            {
                // User is authenticated, proceed with the action
                var users = dbContext.Customers.ToList();
                return View(users);
            }
            else
            {
                // User is not authenticated, redirect to login or unauthorized page
                return RedirectToAction("AdminLogin", "Validation");
            }
        }


        public ActionResult PoliciesList()
        {
            if (Session["AdminUserId"] != null)
            {
                // User is authenticated, proceed with the action
                var policies = dbContext.Policies.ToList();
                return View(policies);
            }
            else
            {
                // User is not authenticated, redirect to login or unauthorized page
                return RedirectToAction("AdminLogin", "Validation");
            }
        }

        public ActionResult Categories()
        {
            if (Session["AdminUserId"] != null)
            {
                // User is authenticated, proceed with the action
                var categories = dbContext.Categories.ToList();
                return View(categories);
            }
            else
            {
                // User is not authenticated, redirect to login or unauthorized page
                return RedirectToAction("AdminLogin", "Validation");
            }
        }

        public ActionResult AllAppliedPolicies()
        {
            if (Session["AdminUserId"] != null)
            {
                // User is authenticated, proceed with the action
                var appliedPolicies = dbContext.AppliedPolicies.ToList();
                return View(appliedPolicies);
            }
            else
            {
                // User is not authenticated, redirect to login or unauthorized page
                return RedirectToAction("AdminLogin", "Validation");
            }
        }



        [HttpPost]
        public ActionResult ApprovePolicy(int policyId)
        {
            if (Session["AdminUserId"] != null)
            {
                var policy = dbContext.AppliedPolicies.Find(policyId);
                if (policy != null && policy.StatusCode == PolicyStatus.Pending)
                {
                    policy.StatusCode = PolicyStatus.Approved;
                    dbContext.SaveChanges();
                }
                return RedirectToAction("AllAppliedPolicies");
            }
            else
            {
                return RedirectToAction("AdminLogin", "Validation");
            }
        }

        [HttpPost]
        public ActionResult DisapprovePolicy(int policyId)
        {
            if (Session["AdminUserId"] != null)
            {
                var policy = dbContext.AppliedPolicies.Find(policyId);
                if (policy != null && policy.StatusCode == PolicyStatus.Pending)
                {
                    policy.StatusCode = PolicyStatus.Disapproved;
                    dbContext.SaveChanges();
                }
                return RedirectToAction("AllAppliedPolicies");
            }
            else
            {
                return RedirectToAction("AdminLogin", "Validation");
            }
        }

        public ActionResult ApprovedPolicies()
        {
            if (Session["AdminUserId"] != null)
            {
                var approvedPolicies = dbContext.AppliedPolicies.Where(p => p.StatusCode == PolicyStatus.Approved).ToList();
                return View(approvedPolicies);
            }
            else
            {
                return RedirectToAction("AdminLogin", "Validation");
            }
        }

        public ActionResult DisapprovedPolicies()
        {
            if (Session["AdminUserId"] != null)
            {
                var disapprovedPolicies = dbContext.AppliedPolicies.Where(p => p.StatusCode == PolicyStatus.Disapproved).ToList();
                return View(disapprovedPolicies);
            }
            else
            {
                return RedirectToAction("AdminLogin", "Validation");
            }
        }

        public ActionResult PendingPolicies()
        {
            if (Session["AdminUserId"] != null)
            {
                var pendingPolicies = dbContext.AppliedPolicies.Where(p => p.StatusCode == PolicyStatus.Pending).ToList();
                return View(pendingPolicies);
            }
            else
            {
                return RedirectToAction("AdminLogin", "Validation");
            }
        }

        public ActionResult Policy()
        {
            if (Session["AdminUserId"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("AdminLogin", "Validation");
            }
        }

        //YourController.cs

        public ActionResult Question()
        {
            if (Session["AdminUserId"] != null)
            {
                var questions = dbContext.Questions.ToList();
                return View(questions);
            }
            else
            {
                return RedirectToAction("AdminLogin", "Validation");
            }
        }

        public ActionResult Reply(int id)
        {
            if (Session["AdminUserId"] != null)
            {
                var question = dbContext.Questions.Find(id);
                return View(question);
            }
            else
            {
                return RedirectToAction("AdminLogin", "Validation");
            }
        }

        [HttpPost]
        public ActionResult Reply(Questions model)
        {
            if (Session["AdminUserId"] != null && ModelState.IsValid)
            {
                var existingQuestion = dbContext.Questions.Find(model.QuestionId);
                if (existingQuestion != null)
                {
                    existingQuestion.Answer = model.Answer;
                    dbContext.SaveChanges();
                    return RedirectToAction("Index");
                }
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult SaveAnswer(int questionId, string answer)
        {
            if (Session["AdminUserId"] != null)
            {
                var existingQuestion = dbContext.Questions.Find(questionId);
                if (existingQuestion != null)
                {
                    existingQuestion.Answer = answer;
                    dbContext.SaveChanges();
                    return Json(new { success = true });
                }
            }

            return Json(new { success = false, error = "Question not found" });
        }


    }
}