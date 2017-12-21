using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace ETohumProject.DAL.Models
{
    public class ProjectContext: DbContext
    {
        public ProjectContext():base("name=ETProject")
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<ProjectContext>());
        }

        public DbSet<User> Users { get; set; }
    }
}
