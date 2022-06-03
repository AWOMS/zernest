using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using AWOMS.Zernest.Components.ZedRun.Features.Races.GetOpenRaces;

namespace AWOMS.Zernest.Components.Races.GetOpenRaces;

public class GetOpenRacesCommandHandler : IRequestHandler<GetOpenRacesCommand, IEnumerable<OpenRace>>
{
	private readonly IMapper _mapper;

	private readonly GetOpenRacesFeature _feature;

	public GetOpenRacesCommandHandler(IMapper mapper, GetOpenRacesFeature feature)
	{
		_mapper = mapper;
		_feature = feature;
	}

	public async Task<IEnumerable<OpenRace>> Handle(GetOpenRacesCommand request, CancellationToken cancellationToken)
	{
		IEnumerable<OpenRaceDTO> dtos = await _feature.GetOpenRaces();

//		var v2 = await _feature.GetOpenRacesV2();

		return _mapper.Map<IEnumerable<OpenRace>>(dtos);
	}
}
