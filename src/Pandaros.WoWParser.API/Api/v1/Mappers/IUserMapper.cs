using Pandaros.WoWParser.API.Api.v1.ViewModels;
using Pandaros.WoWParser.API.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pandaros.WoWParser.API.Api.v1.Mappers
{
    public interface IUserMapperV1
    {
        public UserViewV1 Map(ParserUser user);
        public ParserUser Map(UserViewV1 user);
    }
}
