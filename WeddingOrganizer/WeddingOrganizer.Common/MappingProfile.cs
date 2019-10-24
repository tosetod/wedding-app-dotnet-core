using System;
using AutoMapper;
using DataModels.Entities;
using Models;

namespace WeddingOrganizer.Common
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Guest, GuestModel>()
                .ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.Id))



                .ReverseMap()
                .ForMember();

            CreateMap<>()

        }
    }
}
