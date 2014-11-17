using MotivateMe.Data.Common.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotivateMe.Data.Models
{
    public class ForumPost : AuditInfo, IDeletableEntity
    {
        public ICollection<Tag> tags;
        //public ICollection<Vote> votes;

        public ForumPost()
        {
            this.tags = new HashSet<Tag>();
            //this.votes = new HashSet<Vote>();
        }

        [Key]
        public int Id { get; set; }

        [MaxLength(100)]
        public string Title { get; set; }

        public string AuthorId { get; set; }

        public virtual ApplicationUser Author { get; set; }

        public virtual ICollection<Tag> Tags
        {
            get { return this.tags; }
            set { this.tags = value; }
        }

        //public virtual ICollection<Vote> Votes
        //{
        //    get { return this.votes; }
        //    set { this.votes = value; }
        //}

        public string Content { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}
