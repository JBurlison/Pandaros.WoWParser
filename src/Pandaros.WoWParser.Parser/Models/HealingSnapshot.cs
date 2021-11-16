using System;
using System.Collections.Generic;
using System.Text;

namespace Pandaros.WoWLogParser.Parser.Models
{
    public class HealingSnapshot
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public List<Tuple<string, long>> HealingDone { get; set; } = new List<Tuple<string, long>>();
        public DateTime StartTime { get; set; }
    }
}
