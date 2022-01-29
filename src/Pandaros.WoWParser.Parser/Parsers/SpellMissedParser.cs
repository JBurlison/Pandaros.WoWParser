using Pandaros.WoWParser.Parser.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pandaros.WoWParser.Parser.Parsers
{
    public class SpellMissedParser : SpellParser, ICombatParser<SpellMissed>
    {
        public new SpellMissed Parse(DateTime timestamp, string eventName, string[] eventData)
        {
            return Parse(timestamp, eventName, eventData, new SpellMissed());
        }

        public SpellMissed Parse(DateTime timestamp, string eventName, string[] eventData, SpellMissed obj)
        {
            obj = (SpellMissed)base.Parse(timestamp, eventName, eventData, obj);
            obj.MissType = (MissType)Enum.Parse(typeof(MissType), eventData[Indexes.SPELL_MISSED.MissedType], true);

            if (eventData.Length >= Indexes.SPELL_MISSED.Absorbed)
               obj.Absorbed = eventData[Indexes.SPELL_MISSED.Absorbed].ToInt();

            return obj;
        }
    }
}
