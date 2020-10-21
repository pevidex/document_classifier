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
        public async void AddAsync(Word word)
        {
            await _context.Words.AddAsync(word);
        }
        public async void SaveContext(){
            await _context.SaveChangesAsync();
        }
        public async Task<Word> FindByIdAsync(int id)
        {
            return await _context.Words.FindAsync(id);
        }  

        public void AddMultipleWordsAsync(List<Word> words){
            foreach(Word word in words){
                UpdateOrInsert(word);
            }
        }

        public float GetOverallProbabilityByClass(string possibleClass){
            var countByClass = _context.Documents.Where(x => x.Topic == possibleClass).Count();
            
            var totalDocuments = _context.Documents.Count();

            return (float) countByClass/totalDocuments;
        }

        public int GetTotalWordsPerClass(string possibleClass){
            //todo test
            var result = _context.Words.Where(w => w.Topic == possibleClass).Sum(a => a.Count);
            return (int) result;
        }

        public int getWordCountByClass(Word word, string possibleClass){
             return _context.Words.Where(a => a.Topic == possibleClass && a.Text == word.Text).Select(w => w.Count).FirstOrDefault();
        }

        public int GetTotalCountOfDistinctWords(){
            return _context.Words.Select(w => w.Text).Distinct().Count();
        }

        public void UpdateOrInsert(Word word)
        {
            var word_entry = _context.Words.FirstOrDefault(s => s.Topic == word.Topic && s.Text == word.Text);
            if (word_entry==null){
                _context.Words.Add(word);
            }
            else{
                Console.WriteLine(word_entry.Id + " " +word_entry.Text + " " + word_entry.Count);
                word_entry.Count += word.Count;
            }
        }     
    }
}