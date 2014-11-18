namespace MotivateMe.Web.ViewModels.Home
{
    using AutoMapper;
    using MotivateMe.Data.Models;
    using MotivateMe.Web.Infrastructure.Mapping;
    using System;

    public class IndexCampaignViewModel : IMapFrom<Campaign>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Goal { get; set; }

        public string AuthorName { get; set; }

        public DateTime CreatedOn { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Campaign, IndexCampaignViewModel>()
                .ForMember(m => m.AuthorName, opt => opt.MapFrom(m => m.Author.UserName))
                .ReverseMap();
        }
    }
}