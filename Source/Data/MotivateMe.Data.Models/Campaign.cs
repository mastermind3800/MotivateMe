namespace MotivateMe.Data.Models
{
    using MotivateMe.Data.Common.Models;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Campaign : AuditInfo, IDeletableEntity
    {
        private ICollection<Comment> comments;

        public Campaign()
        {
            this.comments = new HashSet<Comment>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public string AuthorId { get; set; }

        [Required]
        public virtual ApplicationUser Author { get; set; }

        [Required]
        [MaxLength(250)]
        public string Title { get; set; }

        [Required]
        [MaxLength(2500)]
        public string Goal { get; set; }

        [Required]
        [MaxLength(2500)]
        public string Info { get; set; }

        public virtual ICollection<Comment> Comments
        {
            get { return this.comments; }
            set { this.comments = value; }
        }

        [DefaultValue(false)]
        [Index]
        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}
