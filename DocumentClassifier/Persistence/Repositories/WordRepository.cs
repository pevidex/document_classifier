using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DocumentClassifier.API.Domain.Models;
using DocumentClassifier.API.Domain.Repositories;
using DocumentClassifier.API.Persistence.Contexts;
using System.Linq;
using System;
namespace DocumentClassifier.API.Persistence.Repositories
{
    public class WordRepository : BaseRepository, IWordRepository
    {
        public WordRepository(AppDbContext context) : base(context)
        { }
        

        public void AddMultipleWords(List<Word> words){
            foreach(Word word in words){
                UpdateOrInsert(word);
            }
        }

        /*
        Returns the overall probability of a certain topic.
        In other words: P(topic_X) = nr_documents_with_topic_X/nr_total_documents
        todo: requires refactor -> this logic should not be here
        */
        public float GetOverallProbabilityByTopic(string topic){
            var countWordsByTopic = _context.Documents.Where(x => x.Topic == topic).Count();
            
            var totalDocuments = _context.Documents.Count();

            return (float) countWordsByTopic/totalDocuments;
        }

        /*
        Returns the number words in documents with a certain topic
        */
        public int GetTotalWordsPerTopic(string topic){
           return _context.Words.Where(w => w.Topic == topic).Sum(a => a.Count);
        }

        /*
        Returns the number of times a certain word appears within a topic
        */
        public int getWordCountByTopic(Word word, string topic){
             return _context.Words.Where(a => a.Topic == topic && a.Text == word.Text).Select(w => w.Count).FirstOrDefault();
        }

        /*
        Returns the total count of distinct words across all documents
        todo: This value can be kept in cache until new documents are trained
        */
        public int GetTotalCountOfDistinctWords(){
            return _context.Words.Select(w => w.Text).Distinct().Count();
        }

        /*
        Searchs for the key (word,topic). If it does not exist inserts with count = 1.
        Otherwise increments count column of the respective (word,topic) row
        */
        public void UpdateOrInsert(Word word)
        {
            var word_entry = _context.Words.FirstOrDefault(s => s.Topic == word.Topic && s.Text == word.Text);
            if (word_entry==null){
                _context.Words.Add(word);
            }
            else{
                word_entry.Count += word.Count;
            }
        }     
    }
}