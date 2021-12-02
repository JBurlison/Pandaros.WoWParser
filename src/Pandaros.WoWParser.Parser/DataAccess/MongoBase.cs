using System;
using System.Collections.Generic;
using System.Text;
using MongoDB;
using MongoDB.Driver;

namespace Pandaros.WoWLogParser.Parser.DataAccess
{
    public abstract class MongoBase<T>
    {
        protected IMongoCollection<T> Collection { get; private set; }

        protected abstract string DatabaseName { get; set; }
        protected abstract string CollectionName { get; set; }

        internal void Initialize(IMongoClient client)
        {
            var db = client.GetDatabase(DatabaseName);
            Collection = db.GetCollection<T>(CollectionName);
        }
    }
}
