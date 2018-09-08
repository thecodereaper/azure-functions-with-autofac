using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Demo.Core;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;

namespace Demo.Infrastructure.Framework
{
    internal sealed class Connection : IConnection
    {
        public Connection(ICoreConfiguration configuration, string database, IDictionary<string, string> collections)
        {
            Database = database;
            Collections = collections;

            Uri endpoint = new Uri(configuration.DocumentDbEndpoint);
            ConnectionPolicy connectionPolicy = new ConnectionPolicy { EnableEndpointDiscovery = false };

            DocumentClient = new DocumentClient(endpoint, configuration.DocumentDbKey, connectionPolicy);

            CreateDatabase().Wait();
            CreateCollections().Wait();
        }

        public string Database { get; }
        public DocumentClient DocumentClient { get; }
        public IDictionary<string, string> Collections { get; }

        private async Task CreateDatabase()
        {
            try
            {
                await DocumentClient.ReadDatabaseAsync(UriFactory.CreateDatabaseUri(Database));
            }
            catch (DocumentClientException e)
            {
                if (e.StatusCode != HttpStatusCode.NotFound)
                    throw;

                await DocumentClient.CreateDatabaseAsync(new Database { Id = Database });
            }
        }

        private async Task CreateCollections()
        {
            foreach (KeyValuePair<string, string> collection in Collections)
            {
                try
                {
                    Uri uri = UriFactory.CreateDocumentCollectionUri(Database, collection.Value);
                    await DocumentClient.ReadDocumentCollectionAsync(uri);
                }
                catch (DocumentClientException e)
                {
                    if (e.StatusCode != HttpStatusCode.NotFound)
                        throw;

                    await DocumentClient.CreateDocumentCollectionAsync(
                        UriFactory.CreateDatabaseUri(Database),
                        new DocumentCollection { Id = collection.Value },
                        new RequestOptions { OfferThroughput = 1000 }
                    );
                }
            }
        }
    }
}