using System;
using System.Collections.Generic;
using System.Text;
using Pandaros.WoWLogParser.Parser.Models;

namespace Pandaros.WoWLogParser.Parser.Parsers
{
    public interface ICombatParser<T> where T : CombatEventBase
    {
        public T Parse(DateTime timestamp, string eventName, string[] eventData);
    }
}
