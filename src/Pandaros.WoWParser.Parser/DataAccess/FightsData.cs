using MongoDB.Driver;
using Pandaros.WoWParser.Parser.DataAccess.Constants;
using Pandaros.WoWParser.Parser.DataAccess.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pandaros.WoWParser.Parser.DataAccess
{
    internal class FightsData : MongoBase<WoWFight>
    {
        protected override internal string DatabaseName { get; set; } = DatabaseNames.PandarosParser;
        protected override internal string CollectionName { get; set; } = CollectionNames.Fights;

        public FightsData(IMongoClient client)
        {
            Initialize(client);
        }

    }
}
