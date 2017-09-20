using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using HiringFunnel.Data.Models;

namespace HiringFunnel.Data.DAL
{
    public class HFContext : DbContext
    {

        public HFContext()
            : base("DefaultConnection") { }

        public DbSet<User> Users { get; set; }

        public DbSet<Contact> Contacts { get; set; }

        public DbSet<Process> Processes { get; set; }

        public DbSet<ProcessInstance> ProcessInstances { get; set; }

        public DbSet<Technology> Technologies { get; set; }

        public DbSet<Annotation> Annotations { get; set; }

        public DbSet<Interviewer> Interviewers { get; set; }

        public DbSet<ToDoItem> ToDoItems { get; set; }

        protected override void OnModelCreating(DbModelBuilder mBuilder)
        {
            mBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            mBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        }

    }
}
