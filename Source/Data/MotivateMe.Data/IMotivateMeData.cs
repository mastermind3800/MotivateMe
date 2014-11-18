using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotivateMe.Data
{
    using MotivateMe.Data.Common.Repository;
    using MotivateMe.Data.Models;

    public interface IMotivateMeData
    {
        IApplicationDbContext Context { get; }

        IDeletableEntityRepository<Article> Articles { get; }

        IDeletableEntityRepository<Campaign> Campaigns { get; }

        IDeletableEntityRepository<Feedback> Feedbacks { get; }

        IDeletableEntityRepository<Comment> Comments { get; }

        IDeletableEntityRepository<Story> Stories { get; }

        IDeletableEntityRepository<ForumPost> ForumPosts { get; }

        IDeletableEntityRepository<Tag> Tags { get; }

        IDeletableEntityRepository<Tip> Tips { get; }

        IRepository<ApplicationUser> Users { get; }

        int SaveChanges();
    }
}
