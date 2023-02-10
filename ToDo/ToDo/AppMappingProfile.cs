using AutoMapper;
using System;
using ToDo.Entity;
using ToDo.Model;

namespace ToDo
{
    public class AppMappingProfile : Profile
    {
        public AppMappingProfile()
        {
            CreateMap<Tag, TagModel>().ReverseMap();


            CreateMap<Post, PostModel>().ReverseMap();
        }
    }
}
