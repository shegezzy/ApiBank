using ApiBank.Models;
using AutoMapper;

namespace ApiBank.Profiles
{
    public class AutomapperProfiles : Profile
    {
        public AutomapperProfiles()
        {
            CreateMap<RegisterNewAccountModel, Account>();

            CreateMap<UpdateAccountModel, Account>();
            CreateMap<Account, GetAccountModel>();
            //I'll create these dto classes
            



        }
    }
}
