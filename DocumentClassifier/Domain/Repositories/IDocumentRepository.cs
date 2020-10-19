using System.Collections.Generic;
using System.Threading.Tasks;
using DocumentClassifier.API.Domain.Models;

namespace DocumentClassifier.API.Domain.Repositories
{
    public interface IDocumentRepository
    {
        Task<IEnumerable<Document>> ListAsync();
        Task<Document> AddAsync(Document document);
        Task<Document> FindByIdAsync(int id);
        void Update(Document document);
        void Remove(Document document);
    }
}