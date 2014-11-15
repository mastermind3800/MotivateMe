namespace MotivateMe.Data.Models
{
    using MotivateMe.Data.Common.Models;
    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Campaign : AuditInfo, IDeletableEntity
    {
        //private ICollection<ApplicationUser> participants;

        public Campaign()
        {
            //this.participants = new HashSet<ApplicationUser>();
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

        //public virtual ICollection<ApplicationUser> Participants
        //{
        //    get
        //    {
        //        return this.participants;
        //    }
        //    set
        //    {
        //        this.participants = value;
        //    }
        //}

        [DefaultValue(false)]
        [Index]
        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}
