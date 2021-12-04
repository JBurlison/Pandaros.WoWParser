using MongoDB.Driver;
using Pandaros.WoWParser.Parser.DataAccess.Constants;
using Pandaros.WoWParser.Parser.DataAccess.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pandaros.WoWParser.Parser.DataAccess
{
    internal class InstanceInfoData : MongoBase<WoWInstanceInfo>
    {
        protected override internal string DatabaseName { get; set; } = DatabaseNames.PandarosParser;
        protected override internal string CollectionName { get; set; } = CollectionNames.InstanceInfo;

        public InstanceInfoData(IMongoClient client)
        {
            Initialize(client);
        }

    }
}
