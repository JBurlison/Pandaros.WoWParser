using AutoMapper;
using Pandaros.WoWParser.Parser.DataAccess;
using Pandaros.WoWParser.Parser.DataAccess.DTO;
using Pandaros.WoWParser.Parser.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pandaros.WoWParser.Parser.Repositories
{
    public class UserRepo : RepoBase<User, WoWUser, UserData>
    {
        public UserRepo(UserData dataAccess, IMapper mapper) : base(dataAccess, mapper)
        {
        }
    }
}
