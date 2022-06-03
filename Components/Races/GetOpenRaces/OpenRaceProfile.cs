using AutoMapper;
using AWOMS.Zernest.Components.ZedRun.Features.Races.GetOpenRaces;

namespace AWOMS.Zernest.Components.Races.GetOpenRaces;

public class OpenRaceProfile : Profile
{
	public OpenRaceProfile()
	{
		CreateMap<_1DTO, _1>().ReverseMap();
		CreateMap<_2DTO, _2>().ReverseMap();
		CreateMap<_3DTO, _3>().ReverseMap();
		CreateMap<_4DTO, _4>().ReverseMap();
		CreateMap<_5DTO, _5>().ReverseMap();
		CreateMap<_6DTO, _6>().ReverseMap();
		CreateMap<_7DTO, _7>().ReverseMap();
		CreateMap<_8DTO, _8>().ReverseMap();
		CreateMap<_9DTO, _9>().ReverseMap();
		CreateMap<_10DTO, _10>().ReverseMap();
		CreateMap<_11DTO, _11>().ReverseMap();
		CreateMap<_12DTO, _12>().ReverseMap();
		CreateMap<GatesDTO, Gates>().ReverseMap();
		CreateMap<PrizeDTO, Prize>().ReverseMap();
		CreateMap<RaceFactorDTO, RaceFactor>().ReverseMap();
		CreateMap<RuleDTO, Rule>().ReverseMap();
		CreateMap<OpenRaceDTO, OpenRace>().ReverseMap();
	}
}
