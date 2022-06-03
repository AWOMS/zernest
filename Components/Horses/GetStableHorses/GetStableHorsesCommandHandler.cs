using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using AWOMS.Zernest.Components.ZedRun.API;
using AWOMS.Zernest.Components.ZedRun.Features.Horses.GetStableHorses;

namespace AWOMS.Zernest.Components.Horses.GetStableHorses;

public class GetStableHorsesCommandHandler : IRequestHandler<GetStableHorsesCommand, IEnumerable<StableHorse>>
{
	private readonly IValidator<GetStableHorsesInputDTO> _getStableHorsesInputDTOValidator;

	private readonly IMapper _mapper;

	private readonly GetStableHorsesFeature _feature;

	public GetStableHorsesCommandHandler(IValidator<GetStableHorsesInputDTO> getStableHorsesInputDTOValidator,
		IMapper mapper, GetStableHorsesFeature feature)
	{
		_getStableHorsesInputDTOValidator = getStableHorsesInputDTOValidator;
		_mapper = mapper;
		_feature = feature;
	}

	public async Task<IEnumerable<StableHorse>> Handle(GetStableHorsesCommand request, CancellationToken cancellationToken)
	{
		ValidationResult validationResult = _getStableHorsesInputDTOValidator.Validate(request.GetStableHorsesInputDTO);
		if (!validationResult.IsValid)
		{
			throw new ValidationException(validationResult.Errors);
		}
		IEnumerable<StableHorseDTO> dtos = await _feature.GetStableHorses(request.GetStableHorsesInputDTO.StableAddress);
		return _mapper.Map<IEnumerable<StableHorse>>(dtos);
	}
}
