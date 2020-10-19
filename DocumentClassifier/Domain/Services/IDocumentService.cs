using System.Collections.Generic;
using System.Threading.Tasks;
using DocumentClassifier.API.Domain.Models;

namespace DocumentClassifier.API.Domain.Services
{
    public interface IDocumentService
    {
        Task<IEnumerable<Document>> ListAsync();
        Task<Document> FindByIdAsync(int id);
        Task<Document> TrainDocument(Document document);
        Task<Document> TestDocument(Document document);
    }
}