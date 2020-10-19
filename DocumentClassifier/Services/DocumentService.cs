using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Mvc;
using DocumentClassifier.API.Domain.Models;
using DocumentClassifier.API.Domain.Services;
using DocumentClassifier.API.Domain.Repositories;
using DocumentClassifier.API.MachineLearning;

namespace DocumentClassifier.API.Services
{
    public class DocumentService : IDocumentService
    {
        private readonly IDocumentRepository _documentRepository;
        private readonly IPreProcessTextService _iPreProcessTextService;

        public DocumentService(IDocumentRepository documentRepository,IPreProcessTextService iPreProcessTextService)
        {
            _documentRepository = documentRepository;
            _iPreProcessTextService = iPreProcessTextService;
        }

        public async Task<IEnumerable<Document>> ListAsync()
        {
            return await _documentRepository.ListAsync();
        }

        public async Task<Document> FindByIdAsync(int id)
        {
            return await _documentRepository.FindByIdAsync(id);
        }

        public async Task<Document> TrainDocument(Document document)
        {
            //classify then add to bd

            return await _documentRepository.AddAsync(document);
        }    

        public async Task<Document> TestDocument(Document document)
        {
            //test then add to bd
            List<string> test = _iPreProcessTextService.PreProcessDocument(document);
            foreach(string line in test){
                Console.Write(line);
            }
            return await _documentRepository.AddAsync(document);
        }      
    }
}