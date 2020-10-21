using Microsoft.EntityFrameworkCore;
using DocumentClassifier.API.Domain.Models;
using System.Linq;

namespace DocumentClassifier.API.Persistence.Contexts
{
    /*
    Two tables were created:
     a) Documents: maintains data about the trained documents. 
        This table is usefull if we want to add new functionalities in the future and need to 
        keep track of the documents. Otherwise we could discard after processing them.
     b) Words: maintains data about the word counting present in the documents.
        This table is useful for classifying incoming documents. 
        It allows us to classify new documents in a acceptable amount of time and also let us
        keep training the model if we want to.
    */
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {    }
        public DbSet<Document> Documents {get; set;}
        public DbSet<Word> Words {get; set;}

        /*
        Test function to reset db data
        */
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

            builder.Entity<Document>().HasIndex(b => b.Text);
            builder.Entity<Word>().HasIndex(b => b.Text);
        }
    }
}