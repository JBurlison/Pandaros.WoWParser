using MongoDB.Driver;
using Pandaros.WoWParser.Parser.DataAccess.Constants;
using Pandaros.WoWParser.Parser.DataAccess.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pandaros.WoWParser.Parser.DataAccess
{
    public class UserData : MongoBase<WoWUser>
    {
        protected override internal string DatabaseName { get; set; } = DatabaseNames.PandarosParser;
        protected override internal string CollectionName { get; set; } = CollectionNames.Users;

        public UserData(IMongoClient client)
        {
            Initialize(client);
        }

    }
}
