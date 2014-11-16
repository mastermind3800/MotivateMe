namespace MotivateMe.Web.ViewModels.Comments
{
    using MotivateMe.Data.Models;
    using MotivateMe.Web.Infrastructure.Mapping;
    using System.ComponentModel.DataAnnotations;
    
    public class PostCommentViewModel: IMapFrom<Comment>
    {
        public PostCommentViewModel()
        {

        }

        public PostCommentViewModel(int campaignId)
        {
            this.CampaignId = campaignId;
        }
        public int CampaignId { get; set; }

        [Required]
        [MaxLength(250)]
        [UIHint("MultiLineText")]
        public string Content { get; set; }

    }
}