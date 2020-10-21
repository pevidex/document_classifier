using System.Collections.Generic;
using DocumentClassifier.API.Domain.Models;
using DocumentClassifier.API.Domain.Repositories;
using System;

namespace DocumentClassifier.API.MachineLearning
{
    /*
    Future improvements (mainly for better performance):
    1) Keep a cache for values that only changed when new documents are trained;
    2) When the words of a document are inserted, equal words can be inserted all at once. Example: word_a = word_b -> insert(text=word_a,count=2);
    */
    public class NaiveBayesService : ILearningService
    {
        IWordRepository _iWordRepository;
        Dictionary<string,double> _topics;
        ProcessedDocument _document;
        int _nrDistinctWords;

        public NaiveBayesService(IWordRepository iWordRepository)
        {
            _iWordRepository = iWordRepository;
            _topics = new Dictionary<string,double>();
        }

        private void SetRepository(IWordRepository iWordRepository){
            _iWordRepository = iWordRepository;
        }

        //todo: build a dict with distinct words before insert in the db to save time
        public void Train(ProcessedDocument document){
            _iWordRepository.AddMultipleWords(document.Words);
            return;
        }
        public string Classify(ProcessedDocument document){
            Console.WriteLine("## Start classifying document ##");

            _document = document;
            //todo keep this value in cache
            _nrDistinctWords = _iWordRepository.GetTotalCountOfDistinctWords();
            CalculateProbabilityPerTopic("business");
            CalculateProbabilityPerTopic("entertainment");
            CalculateProbabilityPerTopic("politics");
            CalculateProbabilityPerTopic("sport");
            CalculateProbabilityPerTopic("tech");
            Console.WriteLine("## Finished classifying document ##");

            return ChooseClass();
        }

        /*
        Final method to choose the least negative value from the dictionary and returns to the user
        */
        private string ChooseClass(){
            string topic = "";
            double value = 0;
            foreach(KeyValuePair<string, double> entry in _topics)
            {
                if(topic == "" || entry.Value > value){
                    topic = entry.Key;
                    value = entry.Value;
                }
            }
            return topic;
        }
        private void CalculateProbabilityPerTopic(string topic){
            float probabilityByTopic = _iWordRepository.GetOverallProbabilityByTopic(topic);
            _topics[topic] = Math.Log(probabilityByTopic) + ConditionalProbabilities(topic);
        }

        /*
        Naive Bayes formula with Laplace smoothing
        P(word/topic) = (countWordsByTopic + 1)/(totalWordsPerTopic + _nrDistinctWords)
        Then it is applied a logarithm to avoid dealing with values too close to 0
        */
        private double ConditionalProbabilities(string topic)
        {
            //this value can also be in cache
            int totalWordsPerTopic = _iWordRepository.GetTotalWordsPerTopic(topic);
            int dividor = totalWordsPerTopic + _nrDistinctWords;
            double conditionalProbability = 0;
            foreach(Word word in _document.Words)
            {
                int countWordsByTopic = getWordCountByTopic(word, topic);
                double divisionValue = (double)(countWordsByTopic + 1) / dividor;
                double logValue = Math.Log(divisionValue);
                conditionalProbability += logValue;
            }
            return conditionalProbability;
        }
        private int getWordCountByTopic(Word word, string topic){
            return _iWordRepository.getWordCountByTopic(word,topic);

        }
    }
}