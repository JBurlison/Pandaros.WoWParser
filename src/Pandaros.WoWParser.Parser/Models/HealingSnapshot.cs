using System;
using System.Collections.Generic;
using System.Text;

namespace Pandaros.WoWLogParser.Parser.Models
{
    public class HealingSnapshot
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public List<long> HealingDone { get; set; } = new List<long>();
        public DateTime StartTime { get; set; }
    }
}
