using Microsoft.EntityFrameworkCore;
using DocumentClassifier.API.Domain.Models;

namespace DocumentClassifier.API.Persistence.Contexts
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {    }
        public DbSet<Document> Documents {get; set;}
        
        protected override void OnModelCreating (ModelBuilder builder){
            base.OnModelCreating(builder);

            builder.Entity<Document> ().ToTable ("Documents");
            builder.Entity<Document> ().HasKey (p => p.Id);
            builder.Entity<Document> ().Property (p => p.Id).IsRequired ().ValueGeneratedOnAdd ();
            builder.Entity<Document> ().Property (p => p.Id).IsRequired ().HasMaxLength (30);
            builder.Entity<Document> ().HasData (
                new Document { Id = 100, Text = "document1"},
                new Document { Id = 101, Text = "document2"}
            );
        }
    }
}