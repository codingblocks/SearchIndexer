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
            var node = new Uri(definition.IndexerEndpoint);
            var fileText = System.IO.File.ReadAllText(definition.IndexDefinitionFilePath);

            var settings = new ConnectionSettings(node);
            var postData = PostData.String(fileText);
            var client = new ElasticLowLevelClient(settings);
            var response = client.DoRequest<StringResponse>(HttpMethod.PUT, definition.IndexName, postData);
            if (response.HttpStatusCode.HasValue)
            {
                // TODO Good or bad?
                Logger.LogInformation($"StatusCode {response.HttpStatusCode}");
            }
            Logger.LogDebug(response.DebugInformation);
            Logger.LogInformation(response.Body);
            return response.Success;
        }

        public bool DeleteDocuments<TDeleteIndexDefinition, TDeleteDocumentDefinition>(TDeleteIndexDefinition definition, IEnumerable<TDeleteDocumentDefinition> documents)
        {
            throw new System.NotImplementedException();
        }

        public bool DeleteIndex(IIndexDeleteRequest definition)
        {
            var client = CreateClient(definition.IndexerEndpoint);
            var existingIndex = Indices.Index(definition.IndexName);
            if (existingIndex != null)
            {
                Logger.LogInformation($"Deleting index {definition.IndexName}");
                var deleteResponse = client.DeleteIndex(existingIndex);
                if(deleteResponse.Acknowledged)
                {
                    Logger.LogInformation($"Index {definition.IndexName} has been deleted");
                    return true;
                }
                else
                {
                    Logger.LogWarning($"Unable to delete index {definition.IndexName} has been deleted");
                    return false;
                }
            }

            Logger.LogWarning($"Index {definition.IndexName} does not exist, cannot delete");
            return false;
        }

        public bool IndexExists(SearchIndexer.Outputs.OutputPlugin.IIndexExistsRequest definition)
        {
            var client = CreateClient(definition.IndexerEndpoint);
            var result = client.GetIndex(definition.IndexName);
            Logger.LogInformation($"{result.Indices.Count} found for name {definition.IndexName}");
            Logger.LogInformation($"{result.Indices.Count} found with that name");
            return result.Indices.Count > 0;
        }

        private ElasticClient CreateClient(string endpoint)
        {
            var node = new Uri(endpoint);
            var settings = new ConnectionSettings(node);
            var client = new ElasticClient(settings);
            return client;
        }
    }
}
