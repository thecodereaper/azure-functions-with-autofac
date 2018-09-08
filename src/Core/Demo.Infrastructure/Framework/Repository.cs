using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Threading.Tasks;
using Demo.Core.Models;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents.Linq;

namespace Demo.Infrastructure.Framework
{
    internal sealed class Repository : IRepository
    {
        private readonly DocumentClient _documentClient;
        private readonly IConnection _connection;

        public Repository(IConnection connection)
        {
            _connection = connection;
            _documentClient = _connection.DocumentClient;
        }

        public async Task<T> Get<T>(string id)
        {
            try
            {
                Document document = await _documentClient.ReadDocumentAsync(CreateDocumentUri<T>(id));
                return (T) (dynamic) document;
            }
            catch (DocumentClientException e)
            {
                if (e.StatusCode != HttpStatusCode.NotFound)
                    throw;

                return (T) (dynamic) null;
            }
        }

        public async Task<IEnumerable<T>> Get<T>()
        {
            return await GetAll<T>();
        }

        public async Task<IEnumerable<T>> Get<T>(Expression<Func<T, bool>> predicate)
        {
            return await GetAll(predicate);
        }

        public async Task<T> Create<T>(T item)
        {
            Document document = await _documentClient.CreateDocumentAsync(CreateDocumentUri<T>(), item);
            return (T) (dynamic) document;
        }

        public async Task<T> Update<T>(T item)
        {
            IModel itemDocument = (IModel) item;

            AccessCondition accessCondition = new AccessCondition { Condition = itemDocument.Version, Type = AccessConditionType.IfMatch };
            RequestOptions requestOptions = new RequestOptions { AccessCondition = accessCondition };
            Document document = await _documentClient.ReplaceDocumentAsync(CreateDocumentUri<T>(itemDocument.Id), item, requestOptions);

            return (T) (dynamic) document;
        }

        public async Task<bool> Delete<T>(string id)
        {
            await _documentClient.DeleteDocumentAsync(CreateDocumentUri<T>(id));
            return true;
        }

        private async Task<IEnumerable<T>> GetAll<T>(Expression<Func<T, bool>> predicate = null)
        {
            Uri uri = CreateDocumentUri<T>();
            FeedOptions feedOptions = new FeedOptions { MaxItemCount = -1 };

            IDocumentQuery<T> query = predicate == null
                ? _documentClient.CreateDocumentQuery<T>(uri, feedOptions).AsDocumentQuery()
                : _documentClient.CreateDocumentQuery<T>(uri, feedOptions).Where(predicate).AsDocumentQuery();

            List<T> results = new List<T>();

            while (query.HasMoreResults)
                results.AddRange(await query.ExecuteNextAsync<T>());

            return results;
        }

        private Uri CreateDocumentUri<T>(string id = null)
        {
            string collection = _connection.Collections.SingleOrDefault(c => c.Key == typeof(T).FullName).Value;

            return string.IsNullOrEmpty(id)
                ? UriFactory.CreateDocumentCollectionUri(_connection.Database, collection)
                : UriFactory.CreateDocumentUri(_connection.Database, collection, id);
        }
    }
}