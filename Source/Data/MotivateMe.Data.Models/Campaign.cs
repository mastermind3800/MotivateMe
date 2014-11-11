using MotivateMe.Data.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotivateMe.Data.Models
{
    public class Campaign : AuditInfo, IDeletableEntity
    {
        private ICollection<ApplicationUser> participants;

        public Campaign()
        {
            this.participants = new HashSet<ApplicationUser>();
        }

        public int Id { get; set; }

        public string InitiatiorId { get; set; }

        public virtual ApplicationUser Initiator { get; set; }

        public string Title { get; set; }

        public string Goal { get; set; }

        public string Info { get; set; }

        public virtual ICollection<ApplicationUser> Participants
        {
            get
            {
                return this.participants;
            }
            set
            {
                this.participants = value;
            }
        }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}
