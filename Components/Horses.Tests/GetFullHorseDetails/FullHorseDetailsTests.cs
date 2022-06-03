using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using AutoMapper;
using AWOMS.Zernest.Components.Horses.GetFullHorseDetails;
using FluentAssertions;

namespace AWOMS.Zernest.Components.Horses.Tests.GetFullHorseDetails
{
    public class FullHorseDetailsTests
    {
        [Fact]
        public void FullHorseDetails_WithRaceHistory_WinPct_ShouldWork()
        {
            var horse = new FullHorseDetails
            {
                NumberOfRaces = 13,
                Career = new Career
                {
                    First = 4,
                    Second = 2,
                    Third = 1
                }
            };

            horse.FirstWinPct.Should().Be(Math.Round((((double)4 / (double)13) * 100), 2));
            horse.SecondWinPct.Should().Be(Math.Round((((double)2 / (double)13) * 100), 2));
            horse.ThirdWinPct.Should().Be(Math.Round((((double)1 / (double)13) * 100), 2));
            horse.NonPlacePct.Should().Be(Math.Round((((double)6 / (double)13) * 100), 2));
        }
    }
}