using System;
using System.Collections.Generic;
using System.Text;

namespace Pandaros.WoWParser.Parser.Models
{
    public class EnviormentalBase : CombatEventBase, IEnviormentalBase
    {
        public string EnvironmentalType { get; set; }
    }
}
