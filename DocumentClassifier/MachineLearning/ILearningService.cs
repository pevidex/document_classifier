using System.Collections.Generic;
using DocumentClassifier.API.Domain.Models;
using DocumentClassifier.API.Domain.Repositories;

namespace DocumentClassifier.API.MachineLearning
{
    public interface ILearningService
    {
        public void Train(ProcessedDocument document);
        public string Classify(ProcessedDocument document);
    }
}