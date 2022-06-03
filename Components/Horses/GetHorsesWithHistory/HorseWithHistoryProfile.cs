using AutoMapper;
using AWOMS.Zernest.Components.ZedRun.Features.Horses.GetHorsesWithHistory;

namespace AWOMS.Zernest.Components.Horses.GetHorsesWithHistory;

public class HorseWithHistoryProfile : Profile
{
	public HorseWithHistoryProfile()
	{
		CreateMap<CareerDTO, Career>().ReverseMap();
		CreateMap<HashInfoDTO, HashInfo>().ReverseMap();
		CreateMap<HorseWithHistoryDTO, HorseWithHistory>().ReverseMap();
	}
}
