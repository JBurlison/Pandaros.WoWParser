using System;
using System.Collections.Generic;
using System.Text;

namespace Pandaros.WoWParser.Parser.Models
{
    public class SpellDispel : SpellBase, ISpellDispel
    {
        public int ExtraSpellId { get; set; }
        public string ExtraSpellName { get; set; }
        public SpellSchool ExtraSchool { get; set; }
        public string AuraType { get; set; }
    }
}
