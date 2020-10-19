using System.Collections.Generic;
using DocumentClassifier.API.Domain.Models;
using System;
using System.Text.RegularExpressions;
using System.Linq;

namespace DocumentClassifier.API.MachineLearning
{
    /*
    TODO
    - Remove stop words
    - Stemming and Lemmatization
    */
    public class PreProcessTextService: IPreProcessTextService
    {
        public List<string> PreProcessDocument(Document document){
            string preProcessedText = document.Text.ToLower();
            preProcessedText = ReplaceNonLetterCharacters(preProcessedText);
    	    List<string> preProcessedTextAsList = preProcessedText.Split(' ').ToList();
            preProcessedTextAsList = RemoveSingleLetters(preProcessedTextAsList);
            return preProcessedTextAsList;
        }

        private string ReplaceNonLetterCharacters(string text){
            Regex rgx = new Regex("[^a-zA-Z0-9 -]");
            return rgx.Replace(text, "");
        }

        private List<string> RemoveSingleLetters(List<string> text){
            for(int i=text.Count - 1; i > -1; i--)
            {
                if(text[i].Length==1)
                {
                    text.RemoveAt(i);
                }
            }
            return text;
        }
    }
}