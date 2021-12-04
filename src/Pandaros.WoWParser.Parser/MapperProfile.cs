using AutoMapper;
using Pandaros.WoWParser.Parser.DataAccess.DTO;
using Pandaros.WoWParser.Parser.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pandaros.WoWParser.Parser
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<User, WoWUser>().ReverseMap();
            CreateMap<Fight, WoWFight>().ReverseMap();
            CreateMap<Guild, WoWGuild>().ReverseMap();
            CreateMap<Host, WoWHost>().ReverseMap();
            CreateMap<Instance, WoWInstance>().ReverseMap();
            CreateMap<InstanceInfo, WoWInstanceInfo>().ReverseMap();
            CreateMap<Server, WoWServer>().ReverseMap();
        }
    }
}
