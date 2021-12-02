using MongoDB.Driver;
using Pandaros.WoWLogParser.Parser.DataAccess.Constants;
using Pandaros.WoWLogParser.Parser.DataAccess.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pandaros.WoWLogParser.Parser.DataAccess
{
    public class UserDataProvider : MongoBase<User>
    {
        protected override string DatabaseName { get; set; } = DatabaseNames.PandarosParser;
        protected override string CollectionName { get; set; } = CollectionNames.User;

        public UserDataProvider(IMongoClient client)
        {
            Initialize(client);
        }

        public User GetUser(string user)
        {
            return Collection.Find(u => u.EmailAddress == user).FirstOrDefault();
        }

        public void Upsert(User user)
        {
            var existing = GetUser(user.EmailAddress);

            if (existing == null)
                Collection.InsertOne(user);
            else
                Collection.ReplaceOne(u => u.EmailAddress == user.EmailAddress, user);
        }

    }
}
