﻿namespace SearchIndexer.Outputs.OutputPlugin
{
    public interface IIndexCreateRequest
    {
        string IndexerEndpoint { get; }
        string IndexName { get; }
        string IndexDefinitionFilePath { get; }
    }
}