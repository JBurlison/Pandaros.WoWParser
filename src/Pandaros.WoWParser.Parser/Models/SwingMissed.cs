using System;
using System.Collections.Generic;
using System.Text;

namespace Pandaros.WoWLogParser.Parser.Models
{
    public class SwingMissed : SwingBase, IMissed
    {
        public MissType MissType { get; set; }
    }
}
