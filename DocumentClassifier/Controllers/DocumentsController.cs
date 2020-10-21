using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DocumentClassifier.API.Domain.Models;
using DocumentClassifier.API.Domain.Services;

namespace DocumentClassifier.Controllers
{
    /*
    Controller responsible for training and testing documents.
    Also has an endpoint to reset the db and get a list of all documents.
    */
    [Route("api/")]
    public class DocumentsController : Controller
    {
        private readonly IDocumentService _documentService;

        public DocumentsController(IDocumentService documentService)
        {
            _documentService = documentService;
        }
        
        [HttpGet("documents")]
        public async Task<IEnumerable<Document>> GetAllAsync()
        {
            var documents = await _documentService.ListAsync();
            return documents;
        }

        [HttpGet("documents/{id}")]
        public async Task<Document> Get(int id)
        {
            var document = await _documentService.FindByIdAsync(id);
            return document;
        }

        [HttpPost("training/document")]
        public async Task<ActionResult<Document>> TrainDocument([FromBody] Document document)
        {
            if (!TryValidateModel(document, nameof(document)) || document.Topic==null)
            {
                return BadRequest();
            }
           await _documentService.TrainDocument(document);
            return Ok();
        }

        [HttpPost("test/document")]
        public async Task<ActionResult<Document>> TestDocument([FromBody] Document document)
        {
            if (!TryValidateModel(document, nameof(document)))
            {
                return BadRequest();
            }
            string _topic = await _documentService.TestDocument(document);
            return Ok(new {topic = _topic});
        }

        [HttpPost("reset")]
        public async void Reset()
        {
           _documentService.Reset();
        }
    }
}
