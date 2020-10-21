using System.Collections.Generic;
using DocumentClassifier.API.Domain.Models;

namespace DocumentClassifier.API.MachineLearning
{
    public interface IPreProcessTextService
    {
        public ProcessedDocument PreProcessDocument(Document document);
    }
}