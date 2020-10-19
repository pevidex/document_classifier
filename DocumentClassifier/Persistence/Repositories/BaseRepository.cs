using DocumentClassifier.API.Persistence.Contexts;

namespace DocumentClassifier.API.Persistence.Repositories
{
    public class BaseRepository
    {
        protected readonly AppDbContext _context;

        public BaseRepository(AppDbContext context){
            _context = context;
        }
    }
}