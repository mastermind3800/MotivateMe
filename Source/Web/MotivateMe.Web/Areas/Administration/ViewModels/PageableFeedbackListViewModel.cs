namespace MotivateMe.Web.Areas.Administration.ViewModels
{
    using MotivateMe.Data.Models;
    using MotivateMe.Web.Infrastructure.Mapping;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    public class PageableFeedbackListViewModel : IMapFrom<Feedback>, IHaveCustomMappings
    {
        public string Title { get; set; }

        public string Content { get; set; }

        public string AuthorName { get; set; }

        public DateTime CreatedOn { get; set; }

        public void CreateMappings(AutoMapper.IConfiguration configuration)
        {
            configuration.CreateMap<Feedback, PageableFeedbackListViewModel>()
                .ForMember(t => t.AuthorName, opt => opt.MapFrom(t => t.AuthorId == null ? "Anonymous" : t.Author.UserName))
                .ReverseMap();
        }
    }
}