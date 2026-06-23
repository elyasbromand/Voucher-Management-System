using AutoMapper;
using VoucherSystem.Application.DTOs;
using VoucherSystem.Domain.Entities;

namespace VoucherSystem.Application.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Voucher, VoucherDto>()
            .ForMember(dst => dst.Code, opt => opt.MapFrom(src => src.Code.Value))
            .ForMember(
                dst => dst.DiscountType,
                opt => opt.MapFrom(src => src.DiscountType.ToString())
            )
            .ForMember(dst => dst.Status, opt => opt.MapFrom(src => src.Status.ToString()))
            .ForMember(
                dst => dst.AllowedUserTier,
                opt =>
                    opt.MapFrom(src =>
                        src.AllowedUserTier.HasValue ? src.AllowedUserTier.Value.ToString() : null
                    )
            );

        CreateMap<Campaign, CampaignDto>()
            .ForMember(dst => dst.Status, opt => opt.MapFrom(src => src.Status.ToString()));

        CreateMap<Redemption, RedemptionDto>()
            .ForMember(dst => dst.Result, opt => opt.MapFrom(src => src.Result.ToString()));
    }
}
