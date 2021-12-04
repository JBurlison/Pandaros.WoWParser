using System;
using System.Collections.Generic;
using System.Text;
using Pandaros.WoWParser.Parser.Models;

namespace Pandaros.WoWParser.Parser.Parsers
{
    public interface ICombatParser<T> where T : CombatEventBase
    {
        public T Parse(DateTime timestamp, string eventName, string[] eventData);
    }
}
