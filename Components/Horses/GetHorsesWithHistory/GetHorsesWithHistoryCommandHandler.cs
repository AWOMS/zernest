using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using AWOMS.Zernest.Components.ZedRun.API;
using AWOMS.Zernest.Components.ZedRun.Features.Horses.GetHorsesWithHistory;

namespace AWOMS.Zernest.Components.Horses.GetHorsesWithHistory;

public class GetHorsesWithHistoryCommandHandler : IRequestHandler<GetHorsesWithHistoryCommand, IEnumerable<HorseWithHistory>>
{
	private readonly IValidator<GetHorsesWithHistoryInputDTO> _getHorsesWithHistoryInputDTOValidator;

	private readonly IMapper _mapper;

	private readonly GetHorsesWithHistoryFeature _feature;

	public GetHorsesWithHistoryCommandHandler(IValidator<GetHorsesWithHistoryInputDTO> getHorsesWithHistoryInputDTOValidator,
		IMapper mapper, GetHorsesWithHistoryFeature feature)
	{
		_getHorsesWithHistoryInputDTOValidator = getHorsesWithHistoryInputDTOValidator;
		_mapper = mapper;
		_feature = feature;
	}

	public async Task<IEnumerable<HorseWithHistory>> Handle(GetHorsesWithHistoryCommand request, CancellationToken cancellationToken)
	{
		ValidationResult validationResult = _getHorsesWithHistoryInputDTOValidator.Validate(request.GetHorsesWithHistoryInputDTO);
		if (!validationResult.IsValid)
		{
			throw new ValidationException(validationResult.Errors);
		}
		IEnumerable<HorseWithHistoryDTO> dtos = await _feature.GetHorsesWithHistory(request.GetHorsesWithHistoryInputDTO.HorseIds);
		return _mapper.Map<IEnumerable<HorseWithHistory>>(dtos);
	}
}
