using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ETohumProject.BLL.Dtos;
using ETohumProject.DAL.Models;

namespace ETohumProject.BLL.Configurations
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>().ForMember(x=>x.Id, opt=>opt.Ignore());
        }
    }
}
