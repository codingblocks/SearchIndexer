using System;
using System.Collections.Generic;
using System.Threading;
using Elasticsearch.Net;
using Microsoft.Extensions.Logging;
using Nest;
using SearchIndexer.Outputs.OutputPlugin;
using SearchIndexer.Outputs.OutputPlugin.Requests;

namespace ElasticsearchOutputPlugin
{
    public class ElasticsearchIndexService : IIndexService
    {
        private ILogger<ElasticsearchIndexService> Logger { get; }
        public ElasticsearchIndexService(ILogger<ElasticsearchIndexService> logger) {
            Logger = logger;
        }

        public bool AddOrUpdateDocuments<T>(IIndexUpdateRequest request, IEnumerable<T> documents) where T:class
        {
            var client = CreateClient(request);
            var bulkAll = client.BulkAll(documents, b => b
                .Index(request.IndexName) /* index */
                .BackOffRetries(2)
                .BackOffTime("30s")
                .RefreshOnCompleted(true)
                .MaxDegreeOfParallelism(4)
                .Size(1000)
            );

            var waitHandle = new CountdownEvent(1);
            bulkAll.Subscribe(new BulkAllObserver(
                onNext: (b) => { Console.Write("."); },
                onError: (e) => { throw e; },
                onCompleted: () => waitHandle.Signal()
            ));

            waitHandle.Wait();
            return true;
        }

        public bool CreateIndex(IIndexCreateRequest request)
        {
            // TODO Validation
            if(request is null)
            {
                Logger.LogError("Definition is invalid");
                throw new ArgumentException("Argument \"definition\" is null or not of type ElasticIndexDefinition");
            }
            
            var fileText = System.IO.File.ReadAllText(request.IndexDefinitionFilePath);
            var postData = PostData.String(fileText);
            var client = CreateClient(request);
            var response = client.LowLevel.DoRequest<StringResponse>(HttpMethod.PUT, request.IndexName, postData);
            if (response.HttpStatusCode.HasValue)
            {
                // TODO Good or bad?
                Logger.LogInformation($"StatusCode {response.HttpStatusCode}");
            }
            Logger.LogDebug(response.DebugInformation);
            Logger.LogInformation(response.Body);
            return response.Success;
        }

        public bool DeleteIndex(IIndexDeleteRequest request)
        {
            var client = CreateClient(request);
            var existingIndex = Indices.Index(request.IndexName);
            if (existingIndex != null)
            {
                Logger.LogInformation($"Deleting index {request.IndexName}");
                var deleteResponse = client.DeleteIndex(existingIndex);
                if(deleteResponse.Acknowledged)
                {
                    Logger.LogInformation($"Index {request.IndexName} has been deleted");
                    return true;
                }
                else
                {
                    Logger.LogWarning($"Unable to delete index {request.IndexName} has been deleted");
                    return false;
                }
            }

            Logger.LogWarning($"Index {request.IndexName} does not exist, cannot delete");
            return false;
        }

        public bool IndexExists(SearchIndexer.Outputs.OutputPlugin.Requests.IIndexExistsRequest request)
        {
            var client = CreateClient(request);
            var result = client.GetIndex(request.IndexName);
            Logger.LogInformation($"{result.Indices.Count} found for name {request.IndexName}");
            Logger.LogInformation($"{result.Indices.Count} found with that name");
            return result.Indices.Count > 0;
        }

        private ElasticClient CreateClient(IIndexEndpoint endpointConfig)
        {
            var node = new Uri(endpointConfig.IndexerEndpoint);
            var settings = new ConnectionSettings(node);

            if (!string.IsNullOrEmpty(endpointConfig.UserName))
            {
                Logger.LogInformation("Using basic authentication");
                settings.BasicAuthentication(endpointConfig.UserName, endpointConfig.Password);
            } else
            {
                Logger.LogWarning("No authentication information");
            }

            var client = new ElasticClient(settings);
            return client;
        }
    }
}
