using SearchIndexer.InputPlugin;
using SearchIndexer.OutputPlugin;
using System.Collections.Generic;
using System.Linq;

namespace SearchIndexer.App
{
    class Bindings
    {
        internal void LoadPlugins()
        {
            // TODO
            var pluginPaths = new string[] {};

            // TODO
            var availableDocumentProviders = pluginPaths.SelectMany(pluginPath =>
            {
                return new List<IDocumentProvider>();
                // Load all the plugins
                //Assembly pluginAssembly = LoadPlugin(pluginPath);

            });

            var availableIndexServices = pluginPaths.SelectMany(pluginPath =>
            {
                return new List<IIndexService>();
                // Load all the plugins
                //Assembly pluginAssembly = LoadPlugin(pluginPath);

            });
        }

    }
}
