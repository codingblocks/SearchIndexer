# Search Indexer

The idea is to have a simple console UI that can read in documents and export them to an index.

Each type of input or output should have it's own assembly.

## Inputs
* JSON file of podcasts (converts to documents for each episode)
* Postgres query with conferences

## Outputs
* Azure Search
* ElasticSearch

Settings that are specific to an input or output should be managed via configuration settings - not passed in via command line

## Example usage:
```bash
# information about the loading and indexer are done in configuration
dotnet run ./SearchIndexer.dll create-index
dotnet run ./SearchIndexer.dll delete-index
dotnet run ./SearchIndexer.dll update-documents
```

## Configuration
I dunno yet