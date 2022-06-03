using System.Collections.Generic;
using MediatR;

namespace AWOMS.Zernest.Components.Horses.GetStableHorses;

public class GetStableHorsesCommand : IRequest<IEnumerable<StableHorse>>, IBaseRequest
{
	public GetStableHorsesInputDTO GetStableHorsesInputDTO { get; }

	public GetStableHorsesCommand(GetStableHorsesInputDTO getStableHorsesInputDTO)
	{
		GetStableHorsesInputDTO = getStableHorsesInputDTO;
	}
}
