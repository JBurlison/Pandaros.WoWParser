using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pandaros.WoWParser.API.Api.v1.ViewModels
{
    /// <summary>
    ///     Metadata about the Instance, sucha as name and boss fights
    /// </summary>
    public class WoWInstanceInfoViewV1
    {
        public string Name { get; set; }

        public string Location { get; set; }

        public Dictionary<string, List<string>> BossFights { get; set; }
    }
}
