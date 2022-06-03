using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using AWOMS.Zernest.Components.ZedRun.Features.Horses.GetHorsesWithHistory;
using AWOMS.Zernest.Components.ZedRun.Features.Horses.GetStableHorses;

namespace AWOMS.Zernest.Components.Horses.GetFullHorseDetails;

public class GetFullHorseDetailsCommandHandler : IRequestHandler<GetFullHorseDetailsCommand, FullHorseDetails>
{
	private readonly IValidator<GetFullHorseDetailsInputDTO> _getFullHorseDetailsInputDTOValidator;
	private readonly IMapper _mapper;
	private readonly GetHorsesWithHistoryFeature _getHorsesWithHistoryFeature;
	private readonly GetStableHorsesFeature _getStableHorsesFeature;

	public GetFullHorseDetailsCommandHandler(IValidator<GetFullHorseDetailsInputDTO> getFullHorseDetailsInputDTOValidator,
		IMapper mapper, GetHorsesWithHistoryFeature getHorsesWithHistoryFeature, GetStableHorsesFeature getStableHorsesFeature)
	{
		_getHorsesWithHistoryFeature = getHorsesWithHistoryFeature;
		_getStableHorsesFeature = getStableHorsesFeature;
		_getFullHorseDetailsInputDTOValidator = getFullHorseDetailsInputDTOValidator;
		_mapper = mapper;
	}

	public async Task<FullHorseDetails> Handle(GetFullHorseDetailsCommand request, CancellationToken cancellationToken)
	{
		ValidationResult validationResult = _getFullHorseDetailsInputDTOValidator.Validate(request.GetFullHorseDetailsInputDTO);
		if (!validationResult.IsValid)
		{
			throw new ValidationException(validationResult.Errors);
		}
		// FullHorseDetails is a combination of HorseWithHistory + StableHorse
		var fullHorseDetails = new FullHorseDetails();
		var horseWithHistory = await _getHorsesWithHistoryFeature.GetHorseWithHistory(request.GetFullHorseDetailsInputDTO.HorseId);
		var stableHorse = await _getStableHorsesFeature.GetSingleStableHorses(horseWithHistory.Owner, horseWithHistory.HashInfo.Name);
		_mapper.Map<HorseWithHistoryDTO, FullHorseDetails>(horseWithHistory, fullHorseDetails);
		_mapper.Map<StableHorseDTO, FullHorseDetails>(stableHorse, fullHorseDetails);
		return fullHorseDetails;
	}
}
