using System.Collections.Generic;
using DocumentClassifier.API.Domain.Models;
using System;
using System.Text.RegularExpressions;
using System.Linq;
using Utils;

namespace DocumentClassifier.API.MachineLearning
{
    /*
    TODO
    - Stemming and Lemmatization
    */
    public class PreProcessTextService: IPreProcessTextService
    {
        public ProcessedDocument PreProcessDocument(Document document){
            Console.WriteLine("## Pre processing the document ##");
            string preProcessedText = document.Text.ToLower();
            preProcessedText = ReplaceNonAlphaNumericCharacters(preProcessedText);
    	    List<string> preProcessedTextAsList = preProcessedText.Split(' ').ToList();
            preProcessedTextAsList = RemoveSingleLetters(preProcessedTextAsList);
            preProcessedTextAsList = RemoveStopWords(preProcessedTextAsList);
            List<Word> words = new List<Word>();
            foreach(string word in preProcessedTextAsList){
                words.Add(new Word(word,document.Topic));
            }
            ProcessedDocument processedDocument = new ProcessedDocument(document,words);
            Console.WriteLine("## Finished pre processing the document ##");
            return processedDocument;
        }

        private string ReplaceNonAlphaNumericCharacters(string text){
            Regex rgx = new Regex("[^a-zA-Z0-9 ]");
            text = rgx.Replace(text, "");
            return text;
        }

        private List<string> RemoveStopWords(List<string> text){
            for(int i=text.Count - 1; i > -1; i--)
            {
                foreach(string word in UtilsService._stopWords){
                    if(text[i]==word)
                        {
                            text.RemoveAt(i);
                            break;
                        }
                }
            }
            return text;
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