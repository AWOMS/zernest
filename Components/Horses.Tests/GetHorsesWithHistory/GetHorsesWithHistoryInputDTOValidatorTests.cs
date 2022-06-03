using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using AWOMS.Zernest.Components.Horses.GetHorsesWithHistory;
using FluentValidation.TestHelper;

namespace AWOMS.Zernest.Components.Horses.Tests.GetHorsesWithHistory
{
    public class GetHorsesWithHistoryInputDTOValidatorTests
    {
        [Fact]
        public void InputDTOValidator_HorseIds_Empty_FailsValidation()
        {
            //arrange
            var inputDTO = new GetHorsesWithHistoryInputDTO();
            var validator = new GetHorsesWithHistoryInputDTOValidator();

            //act
            var result = validator.TestValidate(inputDTO);

            //assert
            result.ShouldHaveValidationErrorFor(x => x.HorseIds);
        }

        [Fact]
        public void InputDTOValidator_HorseIds_Single_SucceedsValidation()
        {
            //arrange
            var inputDTO = new GetHorsesWithHistoryInputDTO
            {
                HorseIds = new string[] { "396876" }
            };
            var validator = new GetHorsesWithHistoryInputDTOValidator();

            //act
            var result = validator.TestValidate(inputDTO);

            //assert
            result.ShouldNotHaveAnyValidationErrors();
        }
        
        [Fact]
        public void InputDTOValidator_HorseIds_Multiple_SucceedsValidation()
        {
            //arrange
            var inputDTO = new GetHorsesWithHistoryInputDTO
            {
                HorseIds = new string[] { "396876", "183253" }
            };
            var validator = new GetHorsesWithHistoryInputDTOValidator();

            //act
            var result = validator.TestValidate(inputDTO);

            //assert
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}