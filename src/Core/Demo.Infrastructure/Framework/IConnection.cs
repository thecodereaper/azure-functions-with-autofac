using System.Collections.Generic;
using Microsoft.Azure.Documents.Client;

namespace Demo.Infrastructure.Framework
{
    internal interface IConnection
    {
        string Database { get; }

        DocumentClient DocumentClient { get; }

        IDictionary<string, string> Collections { get; }
    }
}