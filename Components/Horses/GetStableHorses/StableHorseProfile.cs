using AutoMapper;
using AWOMS.Zernest.Components.ZedRun.Features.Horses.GetStableHorses;

namespace AWOMS.Zernest.Components.Horses.GetStableHorses;

public class StableHorseProfile : Profile
{
    public StableHorseProfile()
    {
        CreateMap<CareerDTO, Career>().ReverseMap();
        CreateMap<HashInfoDTO, HashInfo>().ReverseMap();
        CreateMap<StableHorseDTO, StableHorse>().ReverseMap();
    }
}
