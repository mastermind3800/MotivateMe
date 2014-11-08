using Microsoft.AspNet.Identity.EntityFramework;
using MotivateMe.Data.Migrations;
using MotivateMe.Data.Models;
using System.Data.Entity;
namespace MotivateMe.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ApplicationDbContext, Configuration>());
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public IDbSet<Tag> Tags { get; set; }
    }
}
