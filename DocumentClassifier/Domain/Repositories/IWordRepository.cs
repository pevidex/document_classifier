using System.Collections.Generic;
using System.Threading.Tasks;
using DocumentClassifier.API.Domain.Models;

namespace DocumentClassifier.API.Domain.Repositories
{
    public interface IWordRepository
    {
        void UpdateOrInsert(Word word);
        void AddAsync(Word word);
        void AddMultipleWordsAsync(List<Word> words);
        void SaveContext();
        float GetOverallProbabilityByClass(string possibleClass);
        int GetTotalWordsPerClass(string possibleClass);
        int GetTotalCountOfDistinctWords();
        int getWordCountByClass(Word word, string possibleClass);
    }
}