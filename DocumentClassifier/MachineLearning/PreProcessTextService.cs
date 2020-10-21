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

            string text = document.Text;

            LowerText(text);
            ReplaceNonAlphaNumericCharacters(text);
    	    List<string> bagOfWords = SplitInList(text);
            RemoveSingleLetters(bagOfWords);
            RemoveStopWords(bagOfWords);
            ProcessedDocument processedDocument = new ProcessedDocument(document,BuildWordList(bagOfWords, document));

            Console.WriteLine("## Finished pre processing the document ##");
            return processedDocument;
        }

        private List<Word> BuildWordList(List<string> bagOfWords, Document document){
            List<Word> words = new List<Word>();
            foreach(string word in bagOfWords){
                words.Add(new Word(word,document.Topic));
            }
            return words;
        }

        private List<string> SplitInList(string text){
            return text.Split(' ').ToList();
        }

        private void LowerText(string text){
            text.ToLower();
        }

        /*
        Todo: in a case where "bla\nbla" becomes "blabla" after this method FIX
        */
        private void ReplaceNonAlphaNumericCharacters(string text){
            Regex rgx = new Regex("[^a-zA-Z0-9 ]");
            text = rgx.Replace(text, "");
        }

        private void RemoveStopWords(List<string> text){
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
        }

        private void RemoveSingleLetters(List<string> text){
            for(int i=text.Count - 1; i > -1; i--)
            {
                if(text[i].Length==1)
                {
                    text.RemoveAt(i);
                }
            }
        }
    }
}