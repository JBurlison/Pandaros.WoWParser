using System;
using System.Collections.Generic;
using System.Text;
using MongoDB;
using MongoDB.Driver;

namespace Pandaros.WoWParser.Parser.DataAccess
{
    internal abstract class MongoBase<T> where T : IdEquatable<T>
    {
        protected internal IMongoCollection<T> Collection { get; private set; }

        protected abstract internal string DatabaseName { get; set; }
        protected abstract internal string CollectionName { get; set; }

        internal void Initialize(IMongoClient client)
        {
            var db = client.GetDatabase(DatabaseName);
            Collection = db.GetCollection<T>(CollectionName);
        }

        internal virtual T Get(string id)
        {
            return Collection.Find(u => u.EquilIds(id)).FirstOrDefault();
        }

        internal virtual void Delete(string id)
        {
            Collection.DeleteOne(u => u.EquilIds(id));
        }

        internal virtual void Upsert(T obj)
        {
            var existing = Get(obj.GetId());

            if (existing == null)
                Collection.InsertOne(obj);
            else
                Collection.ReplaceOne(u => u.EquilIds(obj), obj);
        }
    }
}
