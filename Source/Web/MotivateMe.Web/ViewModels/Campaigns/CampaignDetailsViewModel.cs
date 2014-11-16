namespace MotivateMe.Web.ViewModels.Campaigns
{
    using AutoMapper;
    using MotivateMe.Data.Models;
    using MotivateMe.Web.Infrastructure.Mapping;
    using System;
    using System.Collections.Generic;

    public class CampaignDetailsViewModel : IMapFrom<Campaign>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string AuthorName { get; set; }

        public string Title { get; set; }

        public string Goal { get; set; }

        public string Info { get; set; }

        public virtual ICollection<CommentViewModel> Comments { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Campaign,CampaignDetailsViewModel>()
                .ForMember(m => m.AuthorName, opt => opt.MapFrom(m=> m.Author.UserName))
                .ReverseMap();
        }
    }
}