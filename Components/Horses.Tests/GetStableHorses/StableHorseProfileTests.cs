using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using AutoMapper;
using AWOMS.Zernest.Components.Horses.GetStableHorses;

namespace AWOMS.Zernest.Components.Horses.Tests.GetStableHorses
{
    public class StableHorseProfileTests
    {
        [Fact]
        public void StableHorseProfileTests_AutomapperConfig_IsValid()
        {
            var _configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<StableHorseProfile>();
            });
            _configuration.AssertConfigurationIsValid();
        }
    }
}