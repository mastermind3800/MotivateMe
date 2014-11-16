using AutoMapper;
using MotivateMe.Data.Models;
using MotivateMe.Web.Infrastructure.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MotivateMe.Web.ViewModels.Home
{
    public class IndexStoryViewModel: IMapFrom<Story>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string ShortContent { get; set; }

        public string AuthorName { get; set; }

        public DateTime CreatedOn { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Story, IndexStoryViewModel>()
                .ForMember(m => m.AuthorName, opt => opt.MapFrom(m => m.Author.UserName))
                .ForMember(m => m.ShortContent, opt => opt.MapFrom(m => m.StoryContent.Conclusion))
                .ReverseMap();
        }
    }
}