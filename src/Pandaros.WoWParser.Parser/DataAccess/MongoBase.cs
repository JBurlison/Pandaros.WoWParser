using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MongoDB;
using MongoDB.Driver;
using Pandaros.WoWParser.Parser.DataAccess.DTO;

namespace Pandaros.WoWParser.Parser.DataAccess
{
    public abstract class MongoBase<T> where T : BaseDto
    {
        protected internal IMongoCollection<T> Collection { get; private set; }

        protected abstract internal string DatabaseName { get; set; }
        protected abstract internal string CollectionName { get; set; }

        internal void Initialize(IMongoClient client)
        {
            var db = client.GetDatabase(DatabaseName);
            Collection = db.GetCollection<T>(CollectionName);
        }

        internal async virtual Task<T> GetAsync(string id)
        {
            var result = await Collection.FindAsync(u => u.Id == id);
            return result.FirstOrDefault();
        }

        internal async virtual Task Delete(string id)
        {
            await Collection.DeleteOneAsync(u => u.Id == id);
        }

        internal async virtual Task Upsert(T obj)
        {
            var existing = await GetAsync(obj.Id);

            if (existing == null)
                await Collection.InsertOneAsync(obj);
            else
                await Collection.ReplaceOneAsync(u => u.Id == obj.Id, obj);
        }
    }
}
