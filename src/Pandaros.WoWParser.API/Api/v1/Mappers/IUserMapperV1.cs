using Pandaros.WoWParser.API.Api.v1.ViewModels;
using Pandaros.WoWParser.API.DomainModels;
using Pandaros.WoWParser.Parser.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pandaros.WoWParser.API.Api.v1.Mappers
{
    public interface IUserMapperV1
    {
        public UserViewV1 Map(User user);
        public User Map(UserViewV1 user);
    }
}
