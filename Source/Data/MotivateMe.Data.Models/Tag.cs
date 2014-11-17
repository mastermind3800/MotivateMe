namespace MotivateMe.Data.Models
{
    using MotivateMe.Data.Common.Models;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Tag : AuditInfo, IDeletableEntity
    {
        public ICollection<ForumPost> posts;

        public Tag()
        {
            this.posts = new HashSet<ForumPost>();
        }

        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<ForumPost> Posts
        {
            get { return this.posts; }
            set { this.posts = value; }
        }

        [Index]
        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}
