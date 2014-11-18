namespace MotivateMe.Web.ViewModels.Home
{
   
    using AutoMapper;
    using MotivateMe.Data.Models;
    using MotivateMe.Web.Infrastructure.Mapping;
    using System;

    public class IndexTipViewModel : IMapFrom<Tip>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string  Content { get; set; }

        public string AuthorName { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Tip, IndexTipViewModel>()
                .ForMember(m => m.AuthorName, opt => opt.MapFrom(m => m.Author.UserName))
                .ReverseMap();
        }
    }
}