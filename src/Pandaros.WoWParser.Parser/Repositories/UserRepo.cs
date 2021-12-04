using AutoMapper;
using Pandaros.WoWParser.Parser.DataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pandaros.WoWParser.Parser.Repositories
{
    public class UserRepo
    {
        UserData _userData;
        IMapper _mapper;

        internal UserRepo(UserData userData, IMapper mapper)
        {
            _userData = userData;
            _mapper = mapper;
        }


    }
}
