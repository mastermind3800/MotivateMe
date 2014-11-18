namespace MotivateMe.Web.Areas.Forum.ViewModels
{
    using AutoMapper;
    using MotivateMe.Data.Models;
    using MotivateMe.Web.Infrastructure.Mapping;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class ForumPostViewModel : IMapFrom<ForumPost>, IHaveCustomMappings
    {
        public int Id { get; set; }

        [MaxLength(100)]
        [Required]
        public string Title { get; set; }

        [Required]
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