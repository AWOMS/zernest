using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using AutoMapper;
using AWOMS.Zernest.Components.Races.GetOpenRacesV2;

namespace AWOMS.Zernest.Components.Races.Tests.GetOpenRacesV2
{
    public class OpenRaceProfileTests
    {
        [Fact]
        public void OpenRaceProfileTests_AutomapperConfig_IsValid()
        {
            var _configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<OpenRaceProfile>();
            });
            _configuration.AssertConfigurationIsValid();
        }
    }
}