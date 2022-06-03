using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using AWOMS.Zernest.Components.ZedRun.Features.Races.GetOpenRacesV2;
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
using AWOMS.Zernest.Components.ZedRun.GraphQL;
using System.Collections.ObjectModel;

namespace AWOMS.Zernest.Components.ZedRun.Tests.Features.Races.GetOpenRacesV2
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
        public async Task GetOpenRacesV2_ReturnsListOpenRaceDTO()
        {
            //arrange
            var zedRunGraphQLService = new ZedRunGraphQLService(XUnitLogger.CreateLogger<ZedRunGraphQLService>(_testOutputHelper),
                _config, _httpClient, _memoryCache);
            var feat = new GetOpenRacesFeature(XUnitLogger.CreateLogger<GetOpenRacesFeature>(_testOutputHelper),
                _memoryCache, zedRunGraphQLService);

            bool classAllIsSelected = true;
            bool classDiscoveryIsSelected = false;
            bool classOpenIsSelected = false;
            bool class1IsSelected = false;
            bool class2IsSelected = false;
            bool class3IsSelected = false;
            bool class4IsSelected = false;
            bool class5IsSelected = false;
            bool class6IsSelected = false;
            bool distanceAllIsSelected = true;
            bool distance1000IsSelected = false;
            bool distance1200IsSelected = false;
            bool distance1400IsSelected = false;
            bool distance1600IsSelected = false;
            bool distance1800IsSelected = false;
            bool distance2000IsSelected = false;
            bool distance2200IsSelected = false;
            bool distance2400IsSelected = false;
            bool distance2600IsSelected = false;
            bool eventTypeAllIsSelected = false;
            bool feeAllIsSelected = true;
            bool feeFreeIsSelected = false;
            bool feePaidIsSelected = false;
            bool surfaceAllIsSelected = true;
            bool surfaceRigidIsSelected = false;
            bool surfaceHardIsSelected = false;
            bool surfaceYieldingIsSelected = false;
            bool surfaceSoftIsSelected = false;
            bool surfaceOffIsSelected = false;
            bool surfaceDirtIsSelected = false;

            //act
            var races = await feat.GetOpenRacesV2(classAllIsSelected, classDiscoveryIsSelected,
                classOpenIsSelected, class1IsSelected, class2IsSelected, class3IsSelected,
                class4IsSelected, class5IsSelected, class6IsSelected, distanceAllIsSelected,
                distance1000IsSelected, distance1200IsSelected, distance1400IsSelected,
                distance1600IsSelected, distance1800IsSelected, distance2000IsSelected,
                distance2200IsSelected, distance2400IsSelected, distance2600IsSelected,
                eventTypeAllIsSelected, feeAllIsSelected, feeFreeIsSelected,
                feePaidIsSelected, surfaceAllIsSelected, surfaceRigidIsSelected,
                surfaceHardIsSelected, surfaceYieldingIsSelected, surfaceSoftIsSelected,
                surfaceOffIsSelected, surfaceDirtIsSelected);

            //assert
            races.Should().NotBeNull();
            races.Should().AllBeOfType<Node>();
            races.Should().HaveCountGreaterThan(10);

            var race = races.First();
            race.Should().NotBeNull();
            race.City.Should().NotBeNullOrWhiteSpace();
            race.Class.Should().BeGreaterThanOrEqualTo(0); // discovery = 0
            race.Country.Should().NotBeNullOrWhiteSpace();
            race.CountryCode.Should().NotBeNullOrWhiteSpace();
            //race.EventType.Should().NotBeNullOrWhiteSpace();
            race.Fee.Should().NotBeNullOrWhiteSpace();
            race.Horses.Should().BeOfType<ReadOnlyCollection<Horse>>();
            race.Length.Should().BeGreaterThan(0);
            race.Name.Should().NotBeNullOrWhiteSpace();
            race.Prize.Should().BeOfType<Prize>();
            race.PrizePool.Should().BeOfType<PrizePool>();
            //race.RaceFactor.Should().BeOfType<RaceFactor>();
            //race.RaceFactor.SurfaceValue.Should().NotBeNullOrWhiteSpace();
            race.RaceId.Should().NotBeNullOrWhiteSpace();
            race.Status.Should().NotBeNullOrWhiteSpace();
            race.Weather.Should().NotBeNullOrWhiteSpace();
        }

        [Fact]
        public async Task GetOpenRacesV2_FilterClassI_ReturnsListOpenRaceDTO_Filtered()
        {
            //arrange
            var zedRunGraphQLService = new ZedRunGraphQLService(XUnitLogger.CreateLogger<ZedRunGraphQLService>(_testOutputHelper),
                _config, _httpClient, _memoryCache);
            var feat = new GetOpenRacesFeature(XUnitLogger.CreateLogger<GetOpenRacesFeature>(_testOutputHelper),
                _memoryCache, zedRunGraphQLService);

            bool classAllIsSelected = false;
            bool classDiscoveryIsSelected = false;
            bool classOpenIsSelected = false;
            bool class1IsSelected = true;
            bool class2IsSelected = false;
            bool class3IsSelected = false;
            bool class4IsSelected = false;
            bool class5IsSelected = false;
            bool class6IsSelected = false;
            bool distanceAllIsSelected = true;
            bool distance1000IsSelected = false;
            bool distance1200IsSelected = false;
            bool distance1400IsSelected = false;
            bool distance1600IsSelected = false;
            bool distance1800IsSelected = false;
            bool distance2000IsSelected = false;
            bool distance2200IsSelected = false;
            bool distance2400IsSelected = false;
            bool distance2600IsSelected = false;
            bool eventTypeAllIsSelected = false;
            bool feeAllIsSelected = true;
            bool feeFreeIsSelected = false;
            bool feePaidIsSelected = false;
            bool surfaceAllIsSelected = true;
            bool surfaceRigidIsSelected = false;
            bool surfaceHardIsSelected = false;
            bool surfaceYieldingIsSelected = false;
            bool surfaceSoftIsSelected = false;
            bool surfaceOffIsSelected = false;
            bool surfaceDirtIsSelected = false;

            //act
            var races = await feat.GetOpenRacesV2(classAllIsSelected, classDiscoveryIsSelected,
                classOpenIsSelected, class1IsSelected, class2IsSelected, class3IsSelected,
                class4IsSelected, class5IsSelected, class6IsSelected, distanceAllIsSelected,
                distance1000IsSelected, distance1200IsSelected, distance1400IsSelected,
                distance1600IsSelected, distance1800IsSelected, distance2000IsSelected,
                distance2200IsSelected, distance2400IsSelected, distance2600IsSelected,
                eventTypeAllIsSelected, feeAllIsSelected, feeFreeIsSelected,
                feePaidIsSelected, surfaceAllIsSelected, surfaceRigidIsSelected,
                surfaceHardIsSelected, surfaceYieldingIsSelected, surfaceSoftIsSelected,
                surfaceOffIsSelected, surfaceDirtIsSelected);

            //assert
            races.Should().NotBeNull();
            races.Should().AllBeOfType<Node>();
            races.Should().HaveCountGreaterThan(10);

            var race = races.First();
            race.Should().NotBeNull();
            race.City.Should().NotBeNullOrWhiteSpace();
            race.Class.Should().Be(AWOMS.Zernest.Models.ZedRun.Classes.One);
            race.Country.Should().NotBeNullOrWhiteSpace();
            race.CountryCode.Should().NotBeNullOrWhiteSpace();
            //race.EventType.Should().NotBeNullOrWhiteSpace();
            race.Fee.Should().NotBeNullOrWhiteSpace();
            race.Horses.Should().BeOfType<ReadOnlyCollection<Horse>>();
            race.Length.Should().BeGreaterThan(0);
            race.Name.Should().NotBeNullOrWhiteSpace();
            race.Prize.Should().BeOfType<Prize>();
            race.PrizePool.Should().BeOfType<PrizePool>();
            //race.RaceFactor.Should().BeOfType<RaceFactor>();
            //race.RaceFactor.SurfaceValue.Should().NotBeNullOrWhiteSpace();
            race.RaceId.Should().NotBeNullOrWhiteSpace();
            race.Status.Should().NotBeNullOrWhiteSpace();
            race.Weather.Should().NotBeNullOrWhiteSpace();
        }

        [Fact]
        public async Task GetOpenRacesV2_FilterClassV_ReturnsListOpenRaceDTO_Filtered()
        {
            //arrange
            var zedRunGraphQLService = new ZedRunGraphQLService(XUnitLogger.CreateLogger<ZedRunGraphQLService>(_testOutputHelper),
                _config, _httpClient, _memoryCache);
            var feat = new GetOpenRacesFeature(XUnitLogger.CreateLogger<GetOpenRacesFeature>(_testOutputHelper),
                _memoryCache, zedRunGraphQLService);

            bool classAllIsSelected = false;
            bool classDiscoveryIsSelected = false;
            bool classOpenIsSelected = false;
            bool class1IsSelected = false;
            bool class2IsSelected = false;
            bool class3IsSelected = false;
            bool class4IsSelected = false;
            bool class5IsSelected = true;
            bool class6IsSelected = false;
            bool distanceAllIsSelected = true;
            bool distance1000IsSelected = false;
            bool distance1200IsSelected = false;
            bool distance1400IsSelected = false;
            bool distance1600IsSelected = false;
            bool distance1800IsSelected = false;
            bool distance2000IsSelected = false;
            bool distance2200IsSelected = false;
            bool distance2400IsSelected = false;
            bool distance2600IsSelected = false;
            bool eventTypeAllIsSelected = false;
            bool feeAllIsSelected = true;
            bool feeFreeIsSelected = false;
            bool feePaidIsSelected = false;
            bool surfaceAllIsSelected = true;
            bool surfaceRigidIsSelected = false;
            bool surfaceHardIsSelected = false;
            bool surfaceYieldingIsSelected = false;
            bool surfaceSoftIsSelected = false;
            bool surfaceOffIsSelected = false;
            bool surfaceDirtIsSelected = false;

            //act
            var races = await feat.GetOpenRacesV2(classAllIsSelected, classDiscoveryIsSelected,
                classOpenIsSelected, class1IsSelected, class2IsSelected, class3IsSelected,
                class4IsSelected, class5IsSelected, class6IsSelected, distanceAllIsSelected,
                distance1000IsSelected, distance1200IsSelected, distance1400IsSelected,
                distance1600IsSelected, distance1800IsSelected, distance2000IsSelected,
                distance2200IsSelected, distance2400IsSelected, distance2600IsSelected,
                eventTypeAllIsSelected, feeAllIsSelected, feeFreeIsSelected,
                feePaidIsSelected, surfaceAllIsSelected, surfaceRigidIsSelected,
                surfaceHardIsSelected, surfaceYieldingIsSelected, surfaceSoftIsSelected,
                surfaceOffIsSelected, surfaceDirtIsSelected);

            //assert
            races.Should().NotBeNull();
            races.Should().AllBeOfType<Node>();
            races.Should().HaveCountGreaterThan(1);

            var race = races.First();
            race.Should().NotBeNull();
            race.City.Should().NotBeNullOrWhiteSpace();
            race.Class.Should().Be(AWOMS.Zernest.Models.ZedRun.Classes.Five);
            race.Country.Should().NotBeNullOrWhiteSpace();
            race.CountryCode.Should().NotBeNullOrWhiteSpace();
            //race.EventType.Should().NotBeNullOrWhiteSpace();
            race.Fee.Should().NotBeNullOrWhiteSpace();
            race.Horses.Should().BeOfType<ReadOnlyCollection<Horse>>();
            race.Length.Should().BeGreaterThan(0);
            race.Name.Should().NotBeNullOrWhiteSpace();
            race.Prize.Should().BeOfType<Prize>();
            race.PrizePool.Should().BeOfType<PrizePool>();
            //race.RaceFactor.Should().BeOfType<RaceFactor>();
            //race.RaceFactor.SurfaceValue.Should().NotBeNullOrWhiteSpace();
            race.RaceId.Should().NotBeNullOrWhiteSpace();
            race.Status.Should().NotBeNullOrWhiteSpace();
            race.Weather.Should().NotBeNullOrWhiteSpace();
        }
        
        [Fact]
        public async Task GetOpenRacesV2_FilterClassOpen_ReturnsListOpenRaceDTO_Filtered()
        {
            //arrange
            var zedRunGraphQLService = new ZedRunGraphQLService(XUnitLogger.CreateLogger<ZedRunGraphQLService>(_testOutputHelper),
                _config, _httpClient, _memoryCache);
            var feat = new GetOpenRacesFeature(XUnitLogger.CreateLogger<GetOpenRacesFeature>(_testOutputHelper),
                _memoryCache, zedRunGraphQLService);

            bool classAllIsSelected = false;
            bool classDiscoveryIsSelected = false;
            bool classOpenIsSelected = true;
            bool class1IsSelected = false;
            bool class2IsSelected = false;
            bool class3IsSelected = false;
            bool class4IsSelected = false;
            bool class5IsSelected = false;
            bool class6IsSelected = false;
            bool distanceAllIsSelected = true;
            bool distance1000IsSelected = false;
            bool distance1200IsSelected = false;
            bool distance1400IsSelected = false;
            bool distance1600IsSelected = false;
            bool distance1800IsSelected = false;
            bool distance2000IsSelected = false;
            bool distance2200IsSelected = false;
            bool distance2400IsSelected = false;
            bool distance2600IsSelected = false;
            bool eventTypeAllIsSelected = false;
            bool feeAllIsSelected = true;
            bool feeFreeIsSelected = false;
            bool feePaidIsSelected = false;
            bool surfaceAllIsSelected = true;
            bool surfaceRigidIsSelected = false;
            bool surfaceHardIsSelected = false;
            bool surfaceYieldingIsSelected = false;
            bool surfaceSoftIsSelected = false;
            bool surfaceOffIsSelected = false;
            bool surfaceDirtIsSelected = false;

            //act
            var races = await feat.GetOpenRacesV2(classAllIsSelected, classDiscoveryIsSelected,
                classOpenIsSelected, class1IsSelected, class2IsSelected, class3IsSelected,
                class4IsSelected, class5IsSelected, class6IsSelected, distanceAllIsSelected,
                distance1000IsSelected, distance1200IsSelected, distance1400IsSelected,
                distance1600IsSelected, distance1800IsSelected, distance2000IsSelected,
                distance2200IsSelected, distance2400IsSelected, distance2600IsSelected,
                eventTypeAllIsSelected, feeAllIsSelected, feeFreeIsSelected,
                feePaidIsSelected, surfaceAllIsSelected, surfaceRigidIsSelected,
                surfaceHardIsSelected, surfaceYieldingIsSelected, surfaceSoftIsSelected,
                surfaceOffIsSelected, surfaceDirtIsSelected);

            //assert
            races.Should().NotBeNull();
            races.Should().AllBeOfType<Node>();
            races.Should().HaveCountGreaterThan(1);

            var race = races.First();
            race.Should().NotBeNull();
            race.City.Should().NotBeNullOrWhiteSpace();
            race.Class.Should().Be(AWOMS.Zernest.Models.ZedRun.Classes.Open);
            race.Country.Should().NotBeNullOrWhiteSpace();
            race.CountryCode.Should().NotBeNullOrWhiteSpace();
            //race.EventType.Should().NotBeNullOrWhiteSpace();
            race.Fee.Should().NotBeNullOrWhiteSpace();
            race.Horses.Should().BeOfType<ReadOnlyCollection<Horse>>();
            race.Length.Should().BeGreaterThan(0);
            race.Name.Should().NotBeNullOrWhiteSpace();
            race.Prize.Should().BeOfType<Prize>();
            race.PrizePool.Should().BeOfType<PrizePool>();
            //race.RaceFactor.Should().BeOfType<RaceFactor>();
            //race.RaceFactor.SurfaceValue.Should().NotBeNullOrWhiteSpace();
            race.RaceId.Should().NotBeNullOrWhiteSpace();
            race.Status.Should().NotBeNullOrWhiteSpace();
            race.Weather.Should().NotBeNullOrWhiteSpace();
        }
    }
}