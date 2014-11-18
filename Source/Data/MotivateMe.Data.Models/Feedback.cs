namespace MotivateMe.Data.Models
{
    using MotivateMe.Data.Common.Models;
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Feedback : AuditInfo, IDeletableEntity
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(20)]
        [Required]
        public string Title { get; set; }

        public string AuthorId { get; set; }

        public virtual ApplicationUser Author { get; set; }

        [Required]
        [MaxLength(2500)]
        public string Content { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}
