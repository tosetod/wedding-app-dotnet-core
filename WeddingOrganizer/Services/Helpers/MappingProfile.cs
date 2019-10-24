using AutoMapper;
using DataModels.Entities;
using Models;

namespace Services.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Guest, GuestModel>()
                .ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opts => opts.MapFrom(src => src.Name))
                .ForMember(dest => dest.Confirmed, opts => opts.MapFrom(src => src.Confirmed))
                .ForMember(dest => dest.IsInvited, opts => opts.MapFrom(src => src.IsInvited))
                .ForMember(dest => dest.HasPlusOne, opts => opts.MapFrom(src => src.HasPlusOne))
                .ForMember(dest => dest.User.Id, opts => opts.MapFrom(src => src.UserId))
                .ReverseMap()
                .ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opts => opts.MapFrom(src => src.Name))
                .ForMember(dest => dest.Confirmed, opts => opts.MapFrom(src => src.Confirmed))
                .ForMember(dest => dest.IsInvited, opts => opts.MapFrom(src => src.IsInvited))
                .ForMember(dest => dest.HasPlusOne, opts => opts.MapFrom(src => src.HasPlusOne))
                .ForMember(dest => dest.UserId, opts => opts.MapFrom(src => src.User.Id))
                .ForAllOtherMembers(x => x.Ignore());

            CreateMap<Restaurant, RestaurantModel>()
                .ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.Id))
                .ForMember(dest => dest.Details, opts => opts.MapFrom(src => src.Details))
                .ForMember(dest => dest.Directions, opts => opts.MapFrom(src => src.Directions))
                .ForMember(dest => dest.MoreDetails, opts => opts.MapFrom(src => src.MoreDetails))
                .ForMember(dest => dest.Facebook, opts => opts.MapFrom(src => src.Facebook))
                .ForMember(dest => dest.LogoUrl, opts => opts.MapFrom(src => src.LogoUrl))
                .ForMember(dest => dest.Name, opts => opts.MapFrom(src => src.Name))
                .ForMember(dest => dest.Phone, opts => opts.MapFrom(src => src.Phone))
                .ForMember(dest => dest.Website, opts => opts.MapFrom(src => src.Website))
            .ReverseMap()
                .ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.Id))
                .ForMember(dest => dest.Details, opts => opts.MapFrom(src => src.Details))
                .ForMember(dest => dest.Directions, opts => opts.MapFrom(src => src.Directions))
                .ForMember(dest => dest.MoreDetails, opts => opts.MapFrom(src => src.MoreDetails))
                .ForMember(dest => dest.Facebook, opts => opts.MapFrom(src => src.Facebook))
                .ForMember(dest => dest.LogoUrl, opts => opts.MapFrom(src => src.LogoUrl))
                .ForMember(dest => dest.Name, opts => opts.MapFrom(src => src.Name))
                .ForMember(dest => dest.Phone, opts => opts.MapFrom(src => src.Phone))
                .ForMember(dest => dest.Website, opts => opts.MapFrom(src => src.Website))
                .ForAllOtherMembers(x => x.Ignore());

            CreateMap<BudgetItem, BudgetItemModel>()
                .ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.Id))
                .ForMember(dest => dest.Type, opts => opts.MapFrom(src => src.Type))
                .ForMember(dest => dest.Amount, opts => opts.MapFrom(src => src.Amount))
                .ForMember(dest => dest.Budget, opts => opts.MapFrom(src => src.Budget))
                .ForMember(dest => dest.User.Id, opts => opts.MapFrom(src => src.UserId))
                .ReverseMap()
                .ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.Id))
                .ForMember(dest => dest.Type, opts => opts.MapFrom(src => src.Type))
                .ForMember(dest => dest.Amount, opts => opts.MapFrom(src => src.Amount))
                .ForMember(dest => dest.Budget, opts => opts.MapFrom(src => src.Budget))
                .ForMember(dest => dest.UserId, opts => opts.MapFrom(src => src.User.Id))
                .ForAllOtherMembers(x => x.Ignore());

            CreateMap<User, UserModel>()
                .ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.Id))
                .ForMember(dest => dest.Email, opts => opts.MapFrom(src => src.Email))
                .ForMember(dest => dest.LastName, opts => opts.MapFrom(src => src.LastName))
                .ForMember(dest => dest.FirstName, opts => opts.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.Age, opts => opts.MapFrom(src => src.Age))
                .ForMember(dest => dest.PartnerName, opts => opts.MapFrom(src => src.PartnerName))
                .ForMember(dest => dest.PartnerAge, opts => opts.MapFrom(src => src.PartnerAge))
                .ForMember(dest => dest.Restaurant.Id, opts => opts.MapFrom(src => src.RestaurantId))
                .ForMember(dest => dest.WeddingDate, opts => opts.MapFrom(src => src.WeddingDate))
                .ReverseMap()
                .ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.Id))
                .ForMember(dest => dest.Email, opts => opts.MapFrom(src => src.Email))
                .ForMember(dest => dest.LastName, opts => opts.MapFrom(src => src.LastName))
                .ForMember(dest => dest.FirstName, opts => opts.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.Age, opts => opts.MapFrom(src => src.Age))
                .ForMember(dest => dest.PartnerName, opts => opts.MapFrom(src => src.PartnerName))
                .ForMember(dest => dest.PartnerAge, opts => opts.MapFrom(src => src.PartnerAge))
                .ForMember(dest => dest.RestaurantId, opts => opts.MapFrom(src => src.Restaurant.Id))
                .ForMember(dest => dest.WeddingDate, opts => opts.MapFrom(src => src.PartnerAge))
                .ForAllOtherMembers(x => x.Ignore());


        }
    }
}
