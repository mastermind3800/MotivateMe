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
                    // TODO: Tags
                    // TODO: Author
                };

                this.Data.ForumPosts.Add(dbForumPost);
                this.Data.SaveChanges(); 
                
                TempData["SuccessMessage"] = "You created a post successfully!";
                return this.RedirectToAction("AllForumPosts");
            
                //return this.RedirectToAction("Display", new { id = post.Id });
            }

            return this.View(input);
        }

        // GET: Forum/ForumPosts
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