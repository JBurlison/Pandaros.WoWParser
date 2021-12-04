using System;
using System.Collections.Generic;
using System.Text;

namespace Pandaros.WoWParser.Parser.Models
{
    public class SpellFailed : SpellBase, ISpellFailed
    {
        public string FailedType { get; set; }
    }
}
