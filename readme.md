# Search Indexer

The idea is to have a simple console UI that can read in documents and export them to an index.

Each type of input or output should have it's own assembly.

## Getting started
The easiest way to get started, is to fire up the example architecture. Check out the readme file in the "Examples" folder for more.


## TODO
* Lots, too much to name
* Podcast lib is .net only (no core) and is missing the latest round of fields
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
dotnet ./App/bin/Debug/netcoreapp2.2/App.dll create-index -e "http://localhost:9200" -n podcasts -f Examples\elastic-podcast-index-definition.json
dotnet ./App/bin/Debug/netcoreapp2.2/App.dll delete-index -e "http://localhost:9200" -n podcasts
dotnet ./App/bin/Debug/netcoreapp2.2/App.dll index-exists -e "http://localhost:9200" -n podcasts
dotnet ./App/bin/Debug/netcoreapp2.2/App.dll get-documents -f Examples\podcast-feeds.json
dotnet ./App/bin/Debug/netcoreapp2.2/App.dll update-documents -f  Examples\podcast-feeds.json -e "http://localhost:9200" -n podcasts
```
## Adding a new command

1. Create a new file for the command in App.Commands that impliments ICommand<WhateverYourCommandNameIs>
2. Add the bindings for the command in CommandRegistryExtensions.cs