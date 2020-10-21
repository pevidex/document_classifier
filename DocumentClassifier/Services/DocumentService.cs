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
        private readonly ILearningService _iLearningService;

        public DocumentService(IDocumentRepository documentRepository,IPreProcessTextService iPreProcessTextService,ILearningService iLearningService)
        {
            _documentRepository = documentRepository;
            _iPreProcessTextService = iPreProcessTextService;
            _iLearningService = iLearningService;
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
            ProcessedDocument processedDocument = _iPreProcessTextService.PreProcessDocument(document);
            _iLearningService.Train(processedDocument);
            return await _documentRepository.AddAsync(document);
        }    

        public async Task<string> TestDocument(Document document)
        {
            //test then add to bd
            ProcessedDocument processedDocument = _iPreProcessTextService.PreProcessDocument(document);
            string topic = _iLearningService.Classify(processedDocument);
            return topic;
        }      

        public void Reset(){
            _documentRepository.Reset();
        }
    }
}