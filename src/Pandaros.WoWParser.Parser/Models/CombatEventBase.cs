using System;
using System.Collections.Generic;
using System.Text;

namespace Pandaros.WoWParser.Parser.Models
{
    public class CombatEventBase : ICombatEvent
    {
        public DateTime Timestamp { get; set; }
        public string EventName { get; set; }
        public string SourceGuid { get; set; }
        public string SourceName { get; set; }
        public UnitFlags SourceFlags { get; set; }
        public string DestGuid { get; set; }
        public string DestName { get; set; }
        public UnitFlags DestFlags { get; set; }
        public string[] EventParameters { get; set; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(Timestamp.ToString("M/dd HH:mm:ss.fff"));
            sb.Append(" ");
            sb.Append(EventName);
            sb.Append(",");
            sb.Append(SourceGuid);
            sb.Append(",");
            sb.Append(SourceName);
            sb.Append(",");
            sb.Append(SourceFlags.Value.ToString("X"));
            sb.Append(",");
            sb.Append(DestGuid);
            sb.Append(",");
            sb.Append(DestName);
            sb.Append(",");
            sb.Append(DestFlags.Value.ToString("X"));
            sb.Append(",");
            sb.Append(string.Join(',', EventParameters));

            return sb.ToString();
        }
    }
}
