using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DocumentClassifier.API.Domain.Models
{
    public class Word
    {
        public Word(string text, string topic){
            Text = text;
            Topic = topic;
            Count = 1;
        }
        public int Id {get; set;}

        [Required]
        public string Text {get; set;}

        public string Topic { get; set;}
        public int Count { get; set;}
    }
}