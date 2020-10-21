using Microsoft.EntityFrameworkCore;
using DocumentClassifier.API.Domain.Models;
using System.Linq;

namespace DocumentClassifier.API.Persistence.Contexts
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {    }
        public DbSet<Document> Documents {get; set;}
        public DbSet<Word> Words {get; set;}

        public void Reset()
        {
            Words.RemoveRange(Words);
            Documents.RemoveRange(Documents);
            SaveChanges();
        }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }

        protected override void OnModelCreating (ModelBuilder builder){
            base.OnModelCreating(builder);

            /*builder.Entity<Document> ().ToTable ("Documents");
            builder.Entity<Word> ().ToTable ("Words");
            builder.Entity<Document> ().HasKey (p => p.Id);
            builder.Entity<Document> ().Property (p => p.Id).IsRequired ().ValueGeneratedOnAdd ();
            builder.Entity<Document> ().Property (p => p.Id).IsRequired ().HasMaxLength (30);
            builder.Entity<Document> ().HasData (
                new Document { Id = 100, Text = "document1"},
                new Document { Id = 101, Text = "document2"}
            );*/
        }
    }
}