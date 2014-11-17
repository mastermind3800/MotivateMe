using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MotivateMe.Web.Areas.Forum.InputModel
{
    public class ForumPostInputModel
    {
        [Required]
        [Display(Name = "Title")]
        public string Title { get; set; }

        [Required]
        //[AllowHtml]
        [Display(Name = "Content")]
        //[DataType("tinymce_full")]
        //[UIHint("tinymce_full")]
        public string Content { get; set; }

        // TODO: Create custon validation for the tags
        [Required]
        [Display(Name = "Tags")]
        public string Tags { get; set; }
    }
}