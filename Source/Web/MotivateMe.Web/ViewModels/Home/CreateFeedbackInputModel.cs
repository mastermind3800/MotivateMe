namespace MotivateMe.Web.ViewModels.Home
{
    using System.ComponentModel.DataAnnotations;

    public class CreateFeedbackInputModel
    {
        [Required]
        [Display(Name = "Title")]
        [MaxLength(20)]
        public string Title { get; set; }

        [Required]
        [Display(Name = "Content")]
        [MaxLength(2500)]
        public string Content { get; set; }
    }
}