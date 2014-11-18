namespace MotivateMe.Web.Areas.Forum.Controllers
{
    using AutoMapper.QueryableExtensions;
    using MotivateMe.Data;
    using MotivateMe.Data.Models;
    using MotivateMe.Web.Areas.Forum.InputModel;
    using MotivateMe.Web.Areas.Forum.ViewModels;
    using MotivateMe.Web.Controllers;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    public class ForumPostsController : BaseController
    {
        public ForumPostsController(IMotivateMeData data)
            : base(data)
        {

        }

        [HttpGet]
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            if (ModelState.IsValid)
            {
                var post = this.Data.ForumPosts
                    .All()
                    .Project()
                    .To<ForumPostViewModel>()
                    .Where(p=> p.Id == id)
                    .FirstOrDefault();

                if (post == null)
                {
                    return new HttpStatusCodeResult(404);
                }
                
                return View(post);
            }

            return new HttpStatusCodeResult(404);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ForumPostInputModel input)
        {
            if (ModelState.IsValid)
            {
                var userId = this.CurrentUser.Id;

                var tags = input.Tags.Split(' ').ToList();
                var dbTags = new List<Tag>();

                foreach (var tag in tags)
                {
                    dbTags.Add(new Tag { Name = tag });
                }

                var dbForumPost = new ForumPost
                {
                    Title = input.Title,
                    Content = input.Content,
                    AuthorId = userId,
                    Tags = dbTags
                };

                this.Data.ForumPosts.Add(dbForumPost);
                this.Data.SaveChanges(); 
                
                TempData["SuccessMessage"] = "You created a post successfully!";
                return this.RedirectToAction("AllForumPosts");
            }

            return this.View(input);
        }

        public ActionResult AllForumPosts()
        {
            var forumPosts = this.Data.ForumPosts
                .All()
                .Project()
                .To<ForumPostViewModel>()
                .ToList();

            return View(forumPosts);
        }
    }
}