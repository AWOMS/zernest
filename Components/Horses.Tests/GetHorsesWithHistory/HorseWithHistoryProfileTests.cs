using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using AutoMapper;
using AWOMS.Zernest.Components.Horses.GetHorsesWithHistory;

namespace AWOMS.Zernest.Components.Horses.Tests.GetHorsesWithHistory
{
    public class HorseWithHistoryProfileTests
    {
        [Fact]
        public void HorseWithHistoryProfileTests_AutomapperConfig_IsValid()
        {
            var _configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<HorseWithHistoryProfile>();
            });
            _configuration.AssertConfigurationIsValid();
        }
    }
}