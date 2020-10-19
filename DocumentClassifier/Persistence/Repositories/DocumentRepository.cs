using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DocumentClassifier.API.Domain.Models;
using DocumentClassifier.API.Domain.Repositories;
using DocumentClassifier.API.Persistence.Contexts;
namespace DocumentClassifier.API.Persistence.Repositories
{
    public class DocumentRepository : BaseRepository, IDocumentRepository
    {
        public DocumentRepository(AppDbContext context) : base(context)
        { }
        public async Task<IEnumerable<Document>> ListAsync()
        {
            return await _context.Documents.ToListAsync();
        }
        public async Task<Document> AddAsync(Document document)
        {
            await _context.Documents.AddAsync(document);
            return document;
        }
        public async Task<Document> FindByIdAsync(int id)
        {
            return await _context.Documents.FindAsync(id);
        }       
        public void Remove(Document document)
        {
            _context.Documents.Remove(document);
        }
        public void Update(Document document)
        {
            _context.Documents.Update(document);
        }     
    }
}