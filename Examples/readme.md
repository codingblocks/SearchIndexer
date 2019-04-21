# Example setup instructions

## 1. Fire up the Elastic stack as a daemon:

```bash
docker-compose -f Examples/elastic-docker-compose.yml up -d
```

Give it a minute or so, and check that everything is working by hitting the following url in a browser:
http://localhost:5601 (kibana admin panel)
http://localhost:9200 (elastic REST endpoint)

## 2. Create the index, and load podcast data

```bash
# information about the loading and indexer are done in configuration
dotnet ./App/bin/Debug/netcoreapp2.2/App.dll create-index -e "http://localhost:9200" -n podcasts -f Examples\elastic-podcast-index-definition.json
dotnet ./App/bin/Debug/netcoreapp2.2/App.dll update-documents -f  Examples\podcast-feeds.json -e "http://localhost:9200" -n podcasts
```

## 3. Check out the data!

Navigate to the "Dev Tools" in Kibana, and paste this into the console and verify that you've got data coming back:

```bash
GET podcasts/_search # Find everything
GET podcasts/_search?q=title:Coding+Blocks # Find episodes for podcast "Coding Blocks", note this is NOT fuzzy currently
POST podcasts/_search # Get all the MS Dev Show episodes where they talk about Docker or (boosted) Kubernetes
{
  "query": {
    "bool": {
      "filter": {
        "term": {
          "title": "MS Dev Show"
        }
      },
      "should": [
        {
          "multi_match": {
            "query": "Docker",
            "fields": [
              "episode",
              "description"
            ]
          }
        },
        {
          "multi_match": {
            "query": "Kubernetes",
            "fields": [
              "episode",
              "description"
            ],
            "boost": 1.2
          }
        }
      ]
    }
  }
}
```