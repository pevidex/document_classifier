using System.Collections.Generic;
using System.Threading.Tasks;
using DocumentClassifier.API.Domain.Models;

namespace DocumentClassifier.API.Domain.Repositories
{
    public interface IWordRepository
    {
        void UpdateOrInsert(Word word);
        void AddMultipleWords(List<Word> words);
        float GetOverallProbabilityByTopic(string topic);
        int GetTotalWordsPerTopic(string topic);
        int GetTotalCountOfDistinctWords();
        int getWordCountByTopic(Word word, string topic);
    }
}