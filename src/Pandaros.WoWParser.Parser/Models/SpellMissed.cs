using System;
using System.Collections.Generic;
using System.Text;

namespace Pandaros.WoWParser.Parser.Models
{
    public class SpellMissed : SpellBase, IMissed
    {
        public MissType MissType { get; set; }
    }
}
