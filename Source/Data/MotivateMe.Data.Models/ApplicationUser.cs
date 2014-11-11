namespace MotivateMe.Data.Models
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using MotivateMe.Data.Common.Models;
    using System;
    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Threading.Tasks;
    public class ApplicationUser : IdentityUser, IAuditInfo, IDeletableEntity
    {
        private ICollection<Campaign> campaigns;
        private ICollection<Article> articles;
        private ICollection<Tip> tips;
        private ICollection<Story> stories;

        public ApplicationUser()
        {
            //this prevents UserManager.CreateAsync from causing exception
            this.CreatedOn = DateTime.Now;
            this.campaigns = new HashSet<Campaign>();
            this.stories = new HashSet<Story>();
            this.tips = new HashSet<Tip>();
            this.articles = new HashSet<Article>();
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

        public DateTime CreatedOn { get; set; }

        public bool PreserveCreatedOn { get; set; }

        public System.DateTime? ModifiedOn { get; set; }

        public bool IsDeleted { get; set; }

        public System.DateTime? DeletedOn { get; set; }

        public virtual ICollection<Article> Articles
        {
            get { return this.articles; }
            set { this.articles = value; }
        }
        
        public virtual ICollection<Campaign> Campaigns
        {
            get { return this.campaigns; }
            set { this.campaigns = value; }
        }

        public virtual ICollection<Story> Stories
        {
            get { return this.stories; }
            set { this.stories = value; }
        }

        public virtual ICollection<Tip> Tips
        {
            get { return this.tips; }
            set { this.tips = value; }
        }
    }

}
