namespace MotivateMe.Web.ViewModels.Comments
{
    using AutoMapper;
    using MotivateMe.Data.Models;
    using MotivateMe.Web.Infrastructure.Mapping;

    public class CommentViewModel : IMapFrom<Comment>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string AuthorName { get; set; }

        public string Content { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Comment, CommentViewModel>()
                .ForMember(c => c.AuthorName, opt => opt.MapFrom(c => c.Author.UserName));
        }
    }
}