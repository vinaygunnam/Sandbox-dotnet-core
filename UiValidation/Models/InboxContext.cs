using Microsoft.EntityFrameworkCore;

namespace UiValidation.Models
{
    public class InboxContext : DbContext
    {
        public InboxContext() : base(new DbContextOptionsBuilder<InboxContext>()
                .UseSqlServer("Server=.\\SQLEXPRESS;Database=school;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=true")
                .Options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Inbox>().ToTable("Inbox").HasNoKey();
        }

        public DbSet<Inbox> Inboxes { get; set; }
    }
}
