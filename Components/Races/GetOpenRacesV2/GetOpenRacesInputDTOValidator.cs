using FluentValidation;

namespace AWOMS.Zernest.Components.Races.GetOpenRacesV2;

public class GetOpenRacesInputDTOValidator : AbstractValidator<GetOpenRacesInputDTO>
{
	public GetOpenRacesInputDTOValidator()
	{
		//RuleFor((GetOpenRacesInputDTO x) => x..HorseIds).NotEmpty().WithMessage("One or more horse IDs are required");
	}
}
