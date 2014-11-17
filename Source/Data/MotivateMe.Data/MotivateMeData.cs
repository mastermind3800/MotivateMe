using MotivateMe.Data.Common.Models;
using MotivateMe.Data.Common.Repository;
using MotivateMe.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotivateMe.Data
{
    public class MotivateMeData:IMotivateMeData
    {
        private readonly IApplicationDbContext context;

        private readonly Dictionary<Type, object> repositories = new Dictionary<Type, object>();

        public MotivateMeData(IApplicationDbContext context)
        {
            this.context = context;
        }

        public IApplicationDbContext Context
        {
            get
            {
                return this.context;
            }
        }

        //public IRepository<T> GetGenericRepository<T>() where T : class
        //{
        //    if (typeof(T).IsAssignableFrom(typeof(DeletableEntity)))
        //    {
        //        return this.GetDeletableEntityRepository<T>();
        //    }

        //    return this.GetRepository<T>();
        //}

        public IRepository<ApplicationUser> Users
        {
            get { return this.GetRepository<ApplicationUser>(); }
        }

        public IDeletableEntityRepository<Article> Articles
        {
            get { return this.GetDeletableEntityRepository<Article>(); }
        }

        public IDeletableEntityRepository<Campaign> Campaigns
        {
            get { return this.GetDeletableEntityRepository<Campaign>(); }
        }

        public IDeletableEntityRepository<Comment> Comments
        {
            get { return this.GetDeletableEntityRepository<Comment>(); }
        }

        public IDeletableEntityRepository<ForumPost> ForumPosts
        {
            get { return this.GetDeletableEntityRepository<ForumPost>(); }
        }

        public IDeletableEntityRepository<Story> Stories
        {
            get { return this.GetDeletableEntityRepository<Story>(); }
        }

        public IDeletableEntityRepository<Tag> Tags
        {
            get { return this.GetDeletableEntityRepository<Tag>(); }
        }

        public IDeletableEntityRepository<Tip> Tips
        {
            get { return this.GetDeletableEntityRepository<Tip>(); }
        }

        /// <summary>
        /// Saves all changes made in this context to the underlying database.
        /// </summary>
        /// <returns>
        /// The number of objects written to the underlying database.
        /// </returns>
        /// <exception cref="T:System.InvalidOperationException">Thrown if the context has been disposed.</exception>
        public int SaveChanges()
        {
            return this.context.SaveChanges();
        }

        public void Dispose()
        {
            this.Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this.context != null)
                {
                    this.context.Dispose();
                }
            }
        }

        private IRepository<T> GetRepository<T>() where T : class
        {
            if (!this.repositories.ContainsKey(typeof(T)))
            {
                var type = typeof(GenericRepository<T>);
                this.repositories.Add(typeof(T), Activator.CreateInstance(type, this.context));
            }

            return (IRepository<T>)this.repositories[typeof(T)];
        }

        private IDeletableEntityRepository<T> GetDeletableEntityRepository<T>() where T : class, IDeletableEntity
        {
            if (!this.repositories.ContainsKey(typeof(T)))
            {
                var type = typeof(DeletableEntityRepository<T>);
                this.repositories.Add(typeof(T), Activator.CreateInstance(type, this.context));
            }

            return (IDeletableEntityRepository<T>)this.repositories[typeof(T)];
        }

    }
}
