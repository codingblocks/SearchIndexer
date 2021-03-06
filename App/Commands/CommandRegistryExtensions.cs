﻿using CommandLine;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace SearchIndexer.App.Commands
{
    internal static class CommandRegistryExtensions
    {
        public static IServiceCollection ConfigureCommands(this IServiceCollection serviceCollection, string[] args)
        {
            // Note: Currently 3 places to add a command in this file
            // #1: Hooking up the DI binding
            return serviceCollection
                .AddTransient<CreateIndexCommand>()
                .AddTransient<GetDocumentsCommand>()
                .AddTransient<DeleteIndexCommand>()
                .AddTransient<IndexExistsCommand>()
                .AddTransient<UpdateDocumentsCommand>()
                // #2: Telling Command line parser about it
                .AddSingleton<ParserResult<object>>(Parser.Default.ParseArguments<GetDocumentsCommand.Options, CreateIndexCommand.Options, DeleteIndexCommand.Options, IndexExistsCommand.Options, UpdateDocumentsCommand.Options>(args));
        }

        public static int Execute(this ParserResult<object> options, IServiceProvider provider)
        {
            int Execute<T1, T2>(T2 o) where T1 : ICommand<T2>
            {
                return provider.GetRequiredService<T1>().Execute(o);
            };

            // #3: Execute the correct command based on the arguments
            return options.MapResult(
                (GetDocumentsCommand.Options o) => Execute<GetDocumentsCommand, GetDocumentsCommand.Options>(o),
                (CreateIndexCommand.Options o) => Execute<CreateIndexCommand, CreateIndexCommand.Options>(o),
                (DeleteIndexCommand.Options o) => Execute<DeleteIndexCommand, DeleteIndexCommand.Options>(o),
                (IndexExistsCommand.Options o) => Execute<IndexExistsCommand, IndexExistsCommand.Options>(o),
                (UpdateDocumentsCommand.Options o) => Execute<UpdateDocumentsCommand, UpdateDocumentsCommand.Options>(o),
                errs => 1
            );
        }


    }
}
