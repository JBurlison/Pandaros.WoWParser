using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pandaros.WoWParser.Parser.Models
{
    /// <summary>
    ///     Metadata about the Instance, sucha as name and boss fights
    /// </summary>
    public class InstanceInfo
    {
        public string Name { get; set; }

        public string Location { get; set; }

        public Dictionary<string, List<string>> BossFights { get; set; }
    }
}
