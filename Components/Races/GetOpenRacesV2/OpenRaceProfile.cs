using AutoMapper;
using AWOMS.Zernest.Components.ZedRun.Features.Races.GetOpenRacesV2;

namespace AWOMS.Zernest.Components.Races.GetOpenRacesV2;

public class OpenRaceProfile : Profile
{
	public OpenRaceProfile()
	{
		CreateMap<ZedRun.Features.Races.GetOpenRacesV2.Skin, Skin>().ReverseMap();
		CreateMap<ZedRun.Features.Races.GetOpenRacesV2.RaceFactor, RaceFactor>().ReverseMap();
		CreateMap<ZedRun.Features.Races.GetOpenRacesV2.PrizePool, PrizePool>().ReverseMap();
		CreateMap<ZedRun.Features.Races.GetOpenRacesV2.Prize, Prize>().ReverseMap();
		CreateMap<ZedRun.Features.Races.GetOpenRacesV2.Label, Label>().ReverseMap();
		CreateMap<ZedRun.Features.Races.GetOpenRacesV2.Horse, Horse>().ReverseMap();
		CreateMap<ZedRun.Features.Races.GetOpenRacesV2.EventType, EventType>().ReverseMap();
		CreateMap<ZedRun.Features.Races.GetOpenRacesV2.Node, OpenRace>().ReverseMap();
	}
}
