using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DocumentClassifier.API.Domain.Models
{
    public class Document
    {
        public int Id {get; set;}

        [Required]
        public string Text {get; set;}

        public string Topic { get; set;}
    }
}