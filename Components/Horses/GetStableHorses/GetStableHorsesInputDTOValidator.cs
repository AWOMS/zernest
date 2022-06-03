using FluentValidation;

namespace AWOMS.Zernest.Components.Horses.GetStableHorses;

public class GetStableHorsesInputDTOValidator : AbstractValidator<GetStableHorsesInputDTO>
{
	public GetStableHorsesInputDTOValidator()
	{
		RuleFor((GetStableHorsesInputDTO x) => x.StableAddress).NotEmpty().WithMessage("Stable address is required");
		RuleFor((GetStableHorsesInputDTO x) => x.StableAddress).Length(42, 42).WithMessage("Stable address should be 42 characters long in format of 0x...");
	}
}
