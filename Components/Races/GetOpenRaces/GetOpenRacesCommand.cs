using System.Collections.Generic;
using MediatR;

namespace AWOMS.Zernest.Components.Races.GetOpenRaces;

public class GetOpenRacesCommand : IRequest<IEnumerable<OpenRace>>, IBaseRequest
{
}
