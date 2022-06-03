using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using AWOMS.Zernest.Components.Horses.GetStableHorses;
using FluentValidation.TestHelper;

namespace AWOMS.Zernest.Components.Horses.Tests.GetStableHorses
{
    public class GetStableHorsesInputDTOValidatorTests
    {
        [Fact]
        public void InputDTOValidator_StableAddress_ValidAddress_SucceedsValidation()
        {
            //arrange
            var inputDTO = new GetStableHorsesInputDTO
            {
                StableAddress = "0xfD0De0CAd2A2Ef78654681b5648e28250c814C7D"
            };
            var validator = new GetStableHorsesInputDTOValidator();

            //act
            var result = validator.TestValidate(inputDTO);

            //assert
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Fact]
        public void InputDTOValidator_StableAddress_Empty_FailsValidation()
        {
            //arrange
            var inputDTO = new GetStableHorsesInputDTO();
            var validator = new GetStableHorsesInputDTOValidator();

            //act
            var result = validator.TestValidate(inputDTO);

            //assert
            result.ShouldHaveValidationErrorFor(x => x.StableAddress);
        }

        [Fact]
        public void InputDTOValidator_StableAddress_TooShort_FailsValidation()
        {
            //arrange
            var inputDTO = new GetStableHorsesInputDTO
            {
                StableAddress = "0x123"
            };
            var validator = new GetStableHorsesInputDTOValidator();

            //act
            var result = validator.TestValidate(inputDTO);

            //assert
            result.ShouldHaveValidationErrorFor(x => x.StableAddress);
        }

        [Fact]
        public void InputDTOValidator_StableAddress_TooLong_FailsValidation()
        {
            //arrange
            var inputDTO = new GetStableHorsesInputDTO
            {
                StableAddress = "0x1234567891123456790123456789112345679012345678911234567901234567891123456790"
            };
            var validator = new GetStableHorsesInputDTOValidator();

            //act
            var result = validator.TestValidate(inputDTO);

            //assert
            result.ShouldHaveValidationErrorFor(x => x.StableAddress);
        }
    }
}