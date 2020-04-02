using AutoMapper;
using Easybook.Api.Core.Model.EasyWallet.DataTransferObject;
using Easybook.Api.Core.Model.EasyWallet.Models;
using Wallet_Account = Easybook.Api.Core.Model.EasyWallet.Models.Wallet_Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Easybook.Api.Core.CrossCutting.Utility
{
    public class AutoMapper
    {
        public static void ConfigForWallet()
        {
            Mapper.CreateMap<Wallet_Account, WalletTransferObject>()
                // .ForMember(dest => dest.Available_Balance, opt => opt.MapFrom(src => Math.Round(src.DailyPrice, 2)))
                //.ForMember(
                //    dest => dest.DailyPriceWeekend,
                //    opt => opt.MapFrom(src => Math.Round(src.DailyPriceWeekend ?? src.DailyPrice, 2)))
                //.ForMember(dest => dest.CompanyId, opt => opt.MapFrom(src => src.Company.Id))
                //.ForMember(dest => dest.CompanyName, opt => opt.MapFrom(src => src.Company.Name))
                //.ForMember(dest => dest.CompanyRating, opt => opt.MapFrom(src => src.Company.CompanyRating))
                //.ForMember(
                //    dest => dest.CompanyTermsAndConditions, opt => opt.MapFrom(src => src.Company.TermAndCondition))
                //.ForMember(dest => dest.CollisionDamageWaiver, opt => opt.MapFrom(src => src.Company.CDWRemark))
                //.ForMember(dest => dest.CarModel, opt => opt.MapFrom(src => src.Brand.Name))
                //.ForMember(dest => dest.CompanyLogo, opt => opt.MapFrom(src => src.Company.CompanyLogoPath))
                //.ForMember(dest => dest.CarImage, opt => opt.MapFrom(src => src.CarModel.ImageUrl))
                //.ForMember(dest => dest.ThreeDayPrice, opt => opt.MapFrom(src => src.ThreeDayPrice))
                //.ForMember(dest => dest.WeeklyPrice, opt => opt.MapFrom(src => src.WeeklyPrice));

            ;

        }
    }
}
