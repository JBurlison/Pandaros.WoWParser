using Pandaros.WoWParser.Parser.DataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pandaros.WoWParser.Parser.Repositories
{
    public class UserRepo
    {
        UserData _userData;

        internal UserRepo(UserData userData)
        {
            _userData = userData;
        }


    }
}
