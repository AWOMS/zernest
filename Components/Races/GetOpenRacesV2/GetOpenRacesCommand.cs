using System.Collections.Generic;
using MediatR;

namespace AWOMS.Zernest.Components.Races.GetOpenRacesV2;

public class GetOpenRacesCommand : IRequest<IEnumerable<OpenRace>>, IBaseRequest
{
	public GetOpenRacesInputDTO GetOpenRacesInputDTO { get; }

	public GetOpenRacesCommand(GetOpenRacesInputDTO getOpenRacesInputDTO)
	{
		GetOpenRacesInputDTO = getOpenRacesInputDTO;
	}
}
