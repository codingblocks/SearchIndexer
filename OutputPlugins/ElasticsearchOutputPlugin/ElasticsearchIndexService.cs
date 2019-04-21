using System;
using System.Collections.Generic;
using Elasticsearch.Net;
using Microsoft.Extensions.Logging;
using Nest;
using SearchIndexer.Outputs.OutputPlugin;

namespace ElasticsearchOutputPlugin
{
    public class ElasticsearchIndexService : IIndexService
    {
        private ILogger<ElasticsearchIndexService> Logger { get; }
        public ElasticsearchIndexService(ILogger<ElasticsearchIndexService> logger) {
            Logger = logger;
        }

        public bool AddOrUpdateDocuments<TIndexDefinition, TDocument>(TIndexDefinition definition, IEnumerable<TDocument> documents)
        {
            throw new System.NotImplementedException();
        }

        public bool CreateIndex(IIndexCreateRequest definition)
        {
            // TODO Validation
            if(definition is null)
            {
                Logger.LogError("Definition is invalid");
                throw new ArgumentException("Argument \"definition\" is null or not of type ElasticIndexDefinition");
            }
            const string indexEndpoint = "http://localhost:9200"; // TODO setting
            var node = new Uri(indexEndpoint);
            var fileText = System.IO.File.ReadAllText(definition.FilePath);

            var settings = new ConnectionSettings(node);
            var postData = PostData.String(fileText);
            var client = new ElasticLowLevelClient(settings);
            var response = client.DoRequest<StringResponse>(HttpMethod.PUT, definition.Name, postData);
            if (response.HttpStatusCode.HasValue)
            {
                // TODO Good or bad?
                Logger.LogInformation($"StatusCode {response.HttpStatusCode}");
            }
            Logger.LogDebug(response.DebugInformation);
            Logger.LogInformation(response.Body);
            
            return true;
        }

        public bool DeleteDocuments<TDeleteIndexDefinition, TDeleteDocumentDefinition>(TDeleteIndexDefinition definition, IEnumerable<TDeleteDocumentDefinition> documents)
        {
            throw new System.NotImplementedException();
        }

        public bool DeleteIndex<TDeleteIndexDefinition>(TDeleteIndexDefinition definition)
        {
            throw new System.NotImplementedException();
        }

        public TIndexResult GetIndex<TGetIndexDefinition, TIndexResult>(TGetIndexDefinition definition)
        {
            throw new System.NotImplementedException();
        }
    }
}
