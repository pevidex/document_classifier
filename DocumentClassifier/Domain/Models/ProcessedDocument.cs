using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DocumentClassifier.API.Domain.Models
{
    public class ProcessedDocument
    {
        public ProcessedDocument(Document document, List<Word> words){
            Document = document;
            Topic = document.Topic;
            Words = words;

        }
        [Required]
        public Document Document {get; set;}

        public string Topic { get; set;}
        public List<Word> Words { get; set;}
    }
}