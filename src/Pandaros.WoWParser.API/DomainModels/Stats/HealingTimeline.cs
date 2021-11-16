using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pandaros.WoWParser.API.DomainModels.Stats
{
    public class HealingTimeline : StatsBase
    {
        public Dictionary<string, HealingEvent> HealEvents { get; set; } = new Dictionary<string, HealingEvent>();
    }

    public class HealingEvent
    {
        public List<HealingInstance> Events { get; set; } = new List<HealingInstance>();
        public long HPS { get; set; }
        public long TotalHealing { get; set; }
    }

    public class HealingInstance
    {
        public HealingInstance() { }
        public HealingInstance(string spellName, long heal)
        {
            SpellName = spellName;
            HealAmount = heal;
        }

        public string SpellName { get; set; }
        public long HealAmount { get; set; }
    }
}
