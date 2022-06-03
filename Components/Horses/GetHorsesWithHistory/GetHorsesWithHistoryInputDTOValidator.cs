using FluentValidation;

namespace AWOMS.Zernest.Components.Horses.GetHorsesWithHistory;

public class GetHorsesWithHistoryInputDTOValidator : AbstractValidator<GetHorsesWithHistoryInputDTO>
{
	public GetHorsesWithHistoryInputDTOValidator()
	{
		RuleFor((GetHorsesWithHistoryInputDTO x) => x.HorseIds).NotEmpty().WithMessage("One or more horse IDs are required");
	}
}
