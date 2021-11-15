using System;
using System.Collections.Generic;
using System.Text;

namespace Pandaros.WoWLogParser.Parser.Models
{
    public class SpellEnergize : SpellBase, ISpellEnergize
    {
        public int EneryAmount { get; set; }
        public PowerType PowerType { get; set; }
    }
}
