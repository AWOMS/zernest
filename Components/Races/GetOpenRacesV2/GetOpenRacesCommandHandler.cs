using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using AWOMS.Zernest.Components.ZedRun.Features.Races.GetOpenRacesV2;
using FluentValidation;
using FluentValidation.Results;

namespace AWOMS.Zernest.Components.Races.GetOpenRacesV2;

public class GetOpenRacesCommandHandler : IRequestHandler<GetOpenRacesCommand, IEnumerable<OpenRace>>
{
    private readonly IValidator<GetOpenRacesInputDTO> _getOpenRacesInputDTOValidator;

    private readonly IMapper _mapper;

    private readonly GetOpenRacesFeature _feature;

    public GetOpenRacesCommandHandler(IValidator<GetOpenRacesInputDTO> getOpenRacesInputDTOValidator,
        IMapper mapper, GetOpenRacesFeature feature)
    {
        _getOpenRacesInputDTOValidator = getOpenRacesInputDTOValidator;
        _mapper = mapper;
        _feature = feature;
    }

    public async Task<IEnumerable<OpenRace>> Handle(GetOpenRacesCommand request, CancellationToken cancellationToken)
    {
        ValidationResult validationResult = _getOpenRacesInputDTOValidator.Validate(request.GetOpenRacesInputDTO);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }
        var dtos = await _feature.GetOpenRacesV2(request.GetOpenRacesInputDTO.ClassAllIsSelected,
			request.GetOpenRacesInputDTO.ClassDiscoveryIsSelected,
			request.GetOpenRacesInputDTO.ClassOpenIsSelected,
			request.GetOpenRacesInputDTO.Class1IsSelected,
			request.GetOpenRacesInputDTO.Class2IsSelected,
			request.GetOpenRacesInputDTO.Class3IsSelected,
			request.GetOpenRacesInputDTO.Class4IsSelected,
			request.GetOpenRacesInputDTO.Class5IsSelected,
			request.GetOpenRacesInputDTO.Class6IsSelected,
			request.GetOpenRacesInputDTO.DistanceAllIsSelected,
			request.GetOpenRacesInputDTO.Distance1000IsSelected,
			request.GetOpenRacesInputDTO.Distance1200IsSelected,
			request.GetOpenRacesInputDTO.Distance1400IsSelected,
			request.GetOpenRacesInputDTO.Distance1600IsSelected,
			request.GetOpenRacesInputDTO.Distance1800IsSelected,
			request.GetOpenRacesInputDTO.Distance2000IsSelected,
			request.GetOpenRacesInputDTO.Distance2200IsSelected,
			request.GetOpenRacesInputDTO.Distance2400IsSelected,
			request.GetOpenRacesInputDTO.Distance2600IsSelected,
			request.GetOpenRacesInputDTO.EventTypeAllIsSelected,
			request.GetOpenRacesInputDTO.FeeAllIsSelected,
			request.GetOpenRacesInputDTO.FeeFreeIsSelected,
			request.GetOpenRacesInputDTO.FeePaidIsSelected,
			request.GetOpenRacesInputDTO.SurfaceAllIsSelected,
			request.GetOpenRacesInputDTO.SurfaceRigidIsSelected,
			request.GetOpenRacesInputDTO.SurfaceHardIsSelected,
			request.GetOpenRacesInputDTO.SurfaceYieldingIsSelected,
			request.GetOpenRacesInputDTO.SurfaceSoftIsSelected,
			request.GetOpenRacesInputDTO.SurfaceOffIsSelected,
			request.GetOpenRacesInputDTO.SurfaceDirtIsSelected);
        return _mapper.Map<IEnumerable<OpenRace>>(dtos);
    }
}
