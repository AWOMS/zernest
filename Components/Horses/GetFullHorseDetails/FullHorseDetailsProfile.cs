using AutoMapper;

namespace AWOMS.Zernest.Components.Horses.GetFullHorseDetails;

public class FullHorseDetailsProfile : Profile
{
	public FullHorseDetailsProfile()
	{
		CreateMap<ZedRun.Features.Horses.GetHorsesWithHistory.CareerDTO, Career>().ReverseMap();
		CreateMap<ZedRun.Features.Horses.GetHorsesWithHistory.HashInfoDTO, HashInfo>().ReverseMap();
		CreateMap<ZedRun.Features.Horses.GetHorsesWithHistory.HorseWithHistoryDTO, FullHorseDetails>().ReverseMap();

		CreateMap<ZedRun.Features.Horses.GetStableHorses.CareerDTO, Career>().ReverseMap();
		CreateMap<ZedRun.Features.Horses.GetStableHorses.HashInfoDTO, HashInfo>().ReverseMap();
		CreateMap<ZedRun.Features.Horses.GetStableHorses.StableHorseDTO, FullHorseDetails>().ReverseMap();
	}
}
