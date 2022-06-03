using FluentValidation;

namespace AWOMS.Zernest.Components.Horses.GetFullHorseDetails;

public class GetFullHorseDetailsInputDTOValidator : AbstractValidator<GetFullHorseDetailsInputDTO>
{
	public GetFullHorseDetailsInputDTOValidator()
	{
		RuleFor((GetFullHorseDetailsInputDTO x) => x.HorseId).NotEmpty().WithMessage("Horse ID is required");
	}
}
