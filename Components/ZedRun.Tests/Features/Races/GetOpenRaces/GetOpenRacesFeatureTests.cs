using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using AWOMS.Zernest.Components.ZedRun.API;
using AWOMS.Zernest.Components.ZedRun.Features.Races.GetOpenRaces;
using Microsoft.Extensions.Configuration;
using FluentAssertions;
using System.Net.Http;
using Xunit.Abstractions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Caching.Memory;
using System.Collections.Specialized;
using Microsoft.Extensions.Internal;
using Microsoft.Extensions.Options;
using AWOMS.Zernest.Components.TestHelpers;
using System.Collections.ObjectModel;

namespace AWOMS.Zernest.Components.ZedRun.Tests.Features.Races.GetOpenRaces
{
    public class GetOpenRacesFeatureTests
    {
        private readonly ITestOutputHelper _testOutputHelper;
        private readonly IMemoryCache _memoryCache;
        private readonly HttpClient _httpClient;
        private readonly IConfigurationRoot _config;
        long? NO_SIZE_LIMIT = null;

        public GetOpenRacesFeatureTests(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;

            _config = new ConfigurationBuilder()
                                .SetBasePath(AppContext.BaseDirectory)
                                .AddJsonFile("appsettings.Test.json", false, true)
                                .Build();

            var options = new MemoryCacheOptions()
            {
                Clock = new SystemClock(),
                CompactionPercentage = 1,
                ExpirationScanFrequency = TimeSpan.FromSeconds(100),
                SizeLimit = NO_SIZE_LIMIT
            };
            IOptions<MemoryCacheOptions> optionsAccessor = Options.Create(options);
            _memoryCache = new MemoryCache(optionsAccessor);

            _httpClient = new HttpClient();
        }

        [Fact]
        public async Task GetOpenRaces_ReturnsListOpenRaceDTO()
        {
            //arrange
            var zedRunApiService = new ZedRunApiService(XUnitLogger.CreateLogger<ZedRunApiService>(_testOutputHelper),
                _config, _httpClient, _memoryCache);
            var feat = new GetOpenRacesFeature(XUnitLogger.CreateLogger<GetOpenRacesFeature>(_testOutputHelper),
                _memoryCache, zedRunApiService);

            //act
            var races = await feat.GetOpenRaces();

            //assert
            races.Should().NotBeNull();
            races.Should().AllBeOfType<OpenRaceDTO>();
            races.Should().HaveCountGreaterThan(2);

            var race = races.First();
            race.Should().NotBeNull();
            race.City.Should().NotBeNullOrWhiteSpace();
            race.Class.Should().BeGreaterThanOrEqualTo(0); // discovery = 0
            race.Country.Should().NotBeNullOrWhiteSpace();
            race.CountryCode.Should().NotBeNullOrWhiteSpace();
            race.EventType.Should().NotBeNullOrWhiteSpace();
            race.ExtendedTimeoutAt.Should().BeNull();
            race.Fee.Should().NotBeNullOrWhiteSpace();
            race.FinalPositions.Should().BeNull();
            race.Gates.Should().BeOfType<GatesDTO>();
            race.Length.Should().BeGreaterThan(0);
            race.Name.Should().NotBeNullOrWhiteSpace();
            race.Prize.Should().BeOfType<PrizeDTO>();
            //race.RaceFactor.Should().BeOfType<RaceFactorDTO>();
            //race.RaceFactor.SurfaceValue.Should().NotBeNullOrWhiteSpace();
            race.RaceId.Should().NotBeNullOrWhiteSpace();
            race.RacetrackCode.Should().NotBeNullOrWhiteSpace();
            race.Rules.Should().BeOfType<List<RuleDTO>>();
            race.Sponsors.Should().NotBeNull();
            race.StartTime.Should().BeAfter(new DateTime(2022, 5, 18));
            race.Status.Should().NotBeNullOrWhiteSpace();
            race.TimeoutAt.Should().BeNull();
            race.Weather.Should().NotBeNullOrWhiteSpace();
        }
    }
}