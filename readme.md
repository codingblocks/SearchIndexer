# Search Indexer

The idea is to have a simple console UI that can read in documents and export them to an index.

Each type of input or output should have it's own assembly.

## TODO
* Podcast Input Plugin is a mess
* The divide shouldn't be between input and output, more like document provider and index services
* Configurations specific to the plugin shouldn't be in the args
* Shouldn't call these plugins, if they're bundled then they're just strategies!

## Input Examples (aka Document Providers)
* JSON file of podcasts (converts to documents for each episode)
* Postgres query with conferences (each row is a document)

## Output Examples (aka Indexers)
* Azure Search
* ElasticSearch

Settings that are specific to an input or output _should_ (TODO, for example: security) be managed via configuration settings - not passed in via command line

## Example usage:
```bash
# information about the loading and indexer are done in configuration
dotnet ./App.dll create-index -e "http://localhost:9200" -n podcasts -f "C:\Users\me\Projects\Courses\Elasticsearch\YouTube\docker\podcasts.json"
dotnet ./App.dll delete-index -e "http://localhost:9200" -n podcasts
dotnet ./App.dll get-documents
dotnet ./App.dll update-documents
```
