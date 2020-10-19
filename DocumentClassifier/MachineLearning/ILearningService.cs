using System.Collections.Generic;
using DocumentClassifier.API.Domain.Models;

namespace DocumentClassifier.API.MachineLearning
{
    public interface ILearningService
    {
        public void Train(Document document);
        public void Classify(Document document);
    }
}