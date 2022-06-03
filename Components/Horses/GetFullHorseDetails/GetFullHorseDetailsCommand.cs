using System.Collections.Generic;
using MediatR;

namespace AWOMS.Zernest.Components.Horses.GetFullHorseDetails;

public class GetFullHorseDetailsCommand : IRequest<FullHorseDetails>, IBaseRequest
{
	public GetFullHorseDetailsInputDTO GetFullHorseDetailsInputDTO { get; }

	public GetFullHorseDetailsCommand(GetFullHorseDetailsInputDTO getFullHorseDetailsInputDTO)
	{
		GetFullHorseDetailsInputDTO = getFullHorseDetailsInputDTO;
	}
}
