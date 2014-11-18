namespace MotivateMe.Data
{
    using MotivateMe.Data.Models;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    public interface IApplicationDbContext
    {
        IDbSet<ApplicationUser> Users { get; set; }

        IDbSet<Article> Articles { get; set; }

        IDbSet<Campaign> Campaigns { get; set; }

        IDbSet<Comment> Comments { get; set; }

        IDbSet<ForumPost> ForumPosts { get; set; }

        IDbSet<Story> Stories { get; set; }

        IDbSet<Feedback> Feedbacks { get; set; }

        IDbSet<Tag> Tags { get; set; }

        IDbSet<Tip> Tips { get; set; }

        DbContext DbContext { get; }

        int SaveChanges();

        void Dispose();

        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;

        IDbSet<T> Set<T>() where T : class;
    }
}
