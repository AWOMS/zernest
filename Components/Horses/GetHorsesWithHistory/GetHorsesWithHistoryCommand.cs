using System.Collections.Generic;
using MediatR;

namespace AWOMS.Zernest.Components.Horses.GetHorsesWithHistory;

public class GetHorsesWithHistoryCommand : IRequest<IEnumerable<HorseWithHistory>>, IBaseRequest
{
	public GetHorsesWithHistoryInputDTO GetHorsesWithHistoryInputDTO { get; }

	public GetHorsesWithHistoryCommand(GetHorsesWithHistoryInputDTO getHorsesWithHistoryInputDTO)
	{
		GetHorsesWithHistoryInputDTO = getHorsesWithHistoryInputDTO;
	}
}
