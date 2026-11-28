using AutoMapper;
using GymManagementSystem.Entities;
using GymManagementSystem.ViewModels.Member;

namespace GymManagementSystem.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            MapMember();
        }

        private void MapMember()
        {
            CreateMap<Member, MemberViewModel>()
                .ForMember(dest => dest.Gender, options => options.MapFrom(src => src.Gender.ToString()));
            
            CreateMap<Member, MemberDetailsViewModel>()
                .ForMember(dest => dest.Gender, options => options.MapFrom(src => src.Gender.ToString()))
                .ForMember(dest => dest.DateOfBirth, options => options.MapFrom(src => src.DateOfBirth.ToString()))
                .ForMember(dest => dest.Address, options => options.MapFrom(src => $"{src.Address.BuildingNumber}-{src.Address.Street}-{src.Address.City}"))
                .ForMember(dest => dest.PlanName, options => options.MapFrom(src => src.Memberships.FirstOrDefault().Plan.Name))
                .ForMember(dest => dest.MembershipStatus, options => options.MapFrom(src => src.Memberships.FirstOrDefault().Status))
                .ForMember(dest => dest.MembershipStartDate, options => options.MapFrom(src => src.Memberships.FirstOrDefault().StartDate.ToString()))
                .ForMember(dest => dest.MembershipEndDate, options => options.MapFrom(src => src.Memberships.FirstOrDefault().EndDate.ToString()));

            CreateMap<CreateMemberViewModel, Member>()
                .ForMember(dest => dest.Address, options => options.MapFrom(src => new Address
                {
                    BuildingNumber = src.BuildingNumber,
                    Street = src.Street,
                    City = src.City
                }));
        }
    }
}
