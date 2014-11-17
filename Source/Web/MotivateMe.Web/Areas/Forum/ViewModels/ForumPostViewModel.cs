namespace MotivateMe.Web.Areas.Forum.ViewModels
{
    using AutoMapper;
    using MotivateMe.Data.Models;
    using MotivateMe.Web.Infrastructure.Mapping;
    using System.Collections.Generic;

    public class ForumPostViewModel : IMapFrom<ForumPost>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public string  AuthorName { get; set; }

        public ICollection<Tag> Tags { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<ForumPost, ForumPostViewModel>()
                .ForMember(f => f.AuthorName, opt => opt.MapFrom(f => f.Author.UserName));
        }
    }
}