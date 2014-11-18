namespace MotivateMe.Web.Controllers
{
    using MotivateMe.Data;
    using MotivateMe.Data.Models;
    using MotivateMe.Web.ViewModels.Home;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    public class FeedbacksController : BaseController
    {
        public FeedbacksController(IMotivateMeData data)
            : base(data)
        {
            
        }

        [HttpGet]
        public ActionResult Create()
        {
            var model = new CreateFeedbackInputModel();
            return RedirectToAction("Contact", "Home", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateFeedbackInputModel input)
        {
            if (ModelState.IsValid)
            {
                var feedback = new Feedback
                {
                    Title = input.Title,
                    Content = input.Content,
                };

                if (this.User.Identity.IsAuthenticated)
                {
                    var userId = this.CurrentUser.Id;
                    feedback.AuthorId = userId;
                }

                this.Data.Feedbacks.Add(feedback);
                this.Data.SaveChanges();

                TempData["SuccessMessage"] = "Your feedback was sent successfully";

                return this.RedirectToAction("Index", "Home");
            }
            TempData["ErrorMessage"] = "Something went wrong ;(";
            return RedirectToAction("Contact", "Home", input);
        }
    }
}