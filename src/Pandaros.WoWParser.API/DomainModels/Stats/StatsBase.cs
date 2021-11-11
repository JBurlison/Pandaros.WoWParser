using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pandaros.WoWParser.API.DomainModels.Stats
{
    public abstract class StatsBase : IStats
    {
        // Key of InstanceId and FightId for whole raid OR InstanceId, FightId and CharacterId for specifc character
        public string InstanceId { get; set; }
        public string FightId { get; set; }
        public string CharacterId { get; set; }
    }
}
