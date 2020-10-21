using System.Collections.Generic;
using DocumentClassifier.API.Domain.Models;
using DocumentClassifier.API.Domain.Repositories;
using System;

namespace DocumentClassifier.API.MachineLearning
{
    public class NaiveBayesService : ILearningService
    {
        IWordRepository _iWordRepository;
        Dictionary<string,double> _possibleClasses;
        ProcessedDocument _document;
        int _nrDistinctWords;

        public NaiveBayesService(IWordRepository iWordRepository)
        {
            _iWordRepository = iWordRepository;
            _possibleClasses = new Dictionary<string,double>();
        }

        private void SetRepository(IWordRepository iWordRepository){
            _iWordRepository = iWordRepository;
        }

        //todo: build a dict with distinct words before insert in the db to save time
        public void Train(ProcessedDocument document){
            _iWordRepository.AddMultipleWordsAsync(document.Words);
            return;
        }
        public string Classify(ProcessedDocument document){
            Console.WriteLine("## Start classifying document ##");
            _document = document;
            //todo keep this value in cache
            _nrDistinctWords = _iWordRepository.GetTotalCountOfDistinctWords();
            CalculatePerClass("business");
            CalculatePerClass("entertainment");
            CalculatePerClass("politics");
            CalculatePerClass("sport");
            CalculatePerClass("tech");
            Console.WriteLine("## Finished classifying document ##");
            return ChooseClass();
        }
        private string ChooseClass(){
            string topic = "";
            double value = 0;
            foreach(KeyValuePair<string, double> entry in _possibleClasses)
            {
                if(topic == "" || entry.Value > value){
                    topic = entry.Key;
                    value = entry.Value;
                }
            }
            return topic;
        }
        private void CalculatePerClass(string possibleClass){
            float probabilityByClass = _iWordRepository.GetOverallProbabilityByClass(possibleClass);
            _possibleClasses[possibleClass] = Math.Log(probabilityByClass) + ConditionalProbabilities(possibleClass);
        }

        private double ConditionalProbabilities(string possibleClass)
        {
            int totalWordsPerClass = _iWordRepository.GetTotalWordsPerClass(possibleClass);
            int dividor = totalWordsPerClass +_nrDistinctWords;
            double conditionalProbability = 0;
            foreach(Word word in _document.Words)
            {
                int countWordsByClass = getWordCountByClass(word, possibleClass);
                double divisionValue = ((double)(countWordsByClass + 1) / (dividor + totalWordsPerClass));
                double logValue = Math.Log(divisionValue);
                conditionalProbability += logValue;
            }
            return conditionalProbability;
        }
        private int getWordCountByClass(Word word, string possibleClass){
            return _iWordRepository.getWordCountByClass(word,possibleClass);

        }
    }
}