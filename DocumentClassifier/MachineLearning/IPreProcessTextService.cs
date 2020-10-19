using System.Collections.Generic;
using DocumentClassifier.API.Domain.Models;

namespace DocumentClassifier.API.MachineLearning
{
    public interface IPreProcessTextService
    {
        public List<string> PreProcessDocument(Document document);
    }
}