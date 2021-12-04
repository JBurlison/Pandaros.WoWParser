using System;
using System.Collections.Generic;
using System.Text;

namespace Pandaros.WoWParser.Parser.Models
{
    public class SpellInterrupt : SpellBase, ISpellInterrupt
    {
        public int ExtraSpellId { get; set; }
        public string ExtraSpellName { get; set; }
        public SpellSchool ExtraSchool { get; set; }
    }
}
