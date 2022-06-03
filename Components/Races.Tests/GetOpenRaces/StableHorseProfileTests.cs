using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using AutoMapper;
using AWOMS.Zernest.Components.Races.GetOpenRaces;

namespace AWOMS.Zernest.Components.Races.Tests.GetOpenRaces
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