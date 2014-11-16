namespace MotivateMe.Web.Controllers
{
    using AutoMapper;
    using MotivateMe.Data;
    using MotivateMe.Data.Models;
    using MotivateMe.Web.ViewModels.Comments;
    using System;
    using System.Web;
    using System.Web.Mvc;
    
    public class CommentsController : BaseController
    {
        public CommentsController(IMotivateMeData data)
            :base(data)
        {

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PostComment(PostCommentViewModel comment) 
        {
            if (comment != null && ModelState.IsValid)
            {
                var dbComment = Mapper.Map<Comment>(comment);
                dbComment.Author = this.CurrentUser;
                dbComment.CreatedOn = DateTime.Now;
                var campaign = this.Data.Campaigns.GetById(comment.CampaignId);

                if (campaign == null)
                {
                    throw new HttpException(404, "Ticket Not Found");
                }
                campaign.Comments.Add(dbComment);
                this.Data.SaveChanges();

                var viewModel = Mapper.Map<CommentViewModel>(dbComment);
                return PartialView("_CommentPartial", viewModel);
            }

            throw new HttpException(400, "Invalid comment");
        }
    }
}