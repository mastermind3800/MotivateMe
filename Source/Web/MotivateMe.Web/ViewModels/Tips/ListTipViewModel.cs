namespace MotivateMe.Web.ViewModels.Tips
{
    using AutoMapper;
    using MotivateMe.Data.Models;
    using MotivateMe.Web.Infrastructure.Mapping;

    public class ListTipViewModel : IMapFrom<Tip>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public string AuthorName { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Tip, ListTipViewModel>()
                .ForMember(t => t.AuthorName, opt => opt.MapFrom(t => t.Author.UserName))
                .ReverseMap();

        }
    }
}