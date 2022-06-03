using System.Net.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using AWOMS.Zernest.Components.ZedRun.API;
using AWOMS.Zernest.Components.ZedRun.Features.Horses.GetHorsesWithHistory;
using Microsoft.Extensions.Configuration;
using FluentAssertions;
using AWOMS.Zernest.Components.TestHelpers;
using Xunit.Abstractions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Caching.Memory;
using System.Collections.Specialized;
using Microsoft.Extensions.Internal;
using Microsoft.Extensions.Options;

namespace AWOMS.Zernest.Components.ZedRun.Tests.Features.Horses.GetHorsesWithHistory
{
    public class GetHorsesWithHistoryFeatureTests
    {
        private readonly ITestOutputHelper _testOutputHelper;
        private readonly IMemoryCache _memoryCache;
        private readonly HttpClient _httpClient;
        private readonly IConfigurationRoot _config;
        long? NO_SIZE_LIMIT = null;

        public GetHorsesWithHistoryFeatureTests(ITestOutputHelper testOutputHelper)
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
        public async Task GetHorseWithHistory_SingleHorseId_ReturnsSingleHorseWithHistoryDTO()
        {
            //arrange
            var horseId = "396876";
            var zedRunApiService = new ZedRunApiService(XUnitLogger.CreateLogger<ZedRunApiService>(_testOutputHelper),
                _config, _httpClient, _memoryCache);
            var feat = new GetHorsesWithHistoryFeature(XUnitLogger.CreateLogger<GetHorsesWithHistoryFeature>(_testOutputHelper),
                _memoryCache, zedRunApiService);

            //act
            var horse = await feat.GetHorseWithHistory(horseId);

            //assert
            horse.Should().NotBeNull();
            horse.Bloodline.Should().NotBeNullOrWhiteSpace();
            horse.BreedType.Should().NotBeNullOrWhiteSpace();
            horse.Career.Should().NotBeNull();
            horse.Career.First.Should().BeGreaterThan(0);
            horse.Career.Second.Should().BeGreaterThan(0);
            horse.Career.Third.Should().BeGreaterThan(0);
            horse.Class.Should().BeGreaterThan(0);
            horse.Genotype.Should().NotBeNullOrWhiteSpace();
            horse.HashInfo.Should().NotBeNull();
            horse.HashInfo.Color.Should().NotBeNullOrWhiteSpace();
            horse.HashInfo.HexCode.Should().NotBeNullOrWhiteSpace();
            horse.HashInfo.Name.Should().NotBeNullOrWhiteSpace();
            horse.HorseId.Should().Be(int.Parse(horseId));
            horse.HorseType.Should().NotBeNullOrWhiteSpace();
            horse.ImgUrl.Should().NotBeNullOrWhiteSpace();
            horse.IsTrialHorse.Should().BeFalse();
            horse.OwnerStable.Should().NotBeNullOrWhiteSpace();
            horse.Rating.Should().BeGreaterThan(0);
            horse.Skin.Should().BeNull();
            horse.SurfacePreference.Should().Be(50);
            horse.Type.Should().NotBeNullOrWhiteSpace();
            horse.WinRate.Should().BeGreaterThan(0);
        }

        [Fact]
        public async Task GetHorsesWithHistory_MultipleHorseIds_ReturnsListHorseWithHistoryDTO()
        {
            //arrange
            var horseIds = new string[] { "396876", "183253" };
            var zedRunApiService = new ZedRunApiService(XUnitLogger.CreateLogger<ZedRunApiService>(_testOutputHelper),
                _config, _httpClient, _memoryCache);
            var feat = new GetHorsesWithHistoryFeature(XUnitLogger.CreateLogger<GetHorsesWithHistoryFeature>(_testOutputHelper),
                _memoryCache, zedRunApiService);

            //act
            var horses = await feat.GetHorsesWithHistory(horseIds);

            //assert
            horses.Should().NotBeNull();
            horses.Should().AllBeOfType<HorseWithHistoryDTO>();
            horses.Should().HaveCount(2);

            var horse = horses.Single(x => x.HorseId == 396876);
            horse.Should().NotBeNull();
            horse.Bloodline.Should().NotBeNullOrWhiteSpace();
            horse.BreedType.Should().NotBeNullOrWhiteSpace();
            horse.Career.Should().NotBeNull();
            horse.Career.First.Should().BeGreaterThan(0);
            horse.Career.Second.Should().BeGreaterThan(0);
            horse.Career.Third.Should().BeGreaterThan(0);
            horse.Class.Should().BeGreaterThan(0);
            horse.Genotype.Should().NotBeNullOrWhiteSpace();
            horse.HashInfo.Should().NotBeNull();
            horse.HashInfo.Color.Should().NotBeNullOrWhiteSpace();
            horse.HashInfo.HexCode.Should().NotBeNullOrWhiteSpace();
            horse.HashInfo.Name.Should().NotBeNullOrWhiteSpace();
            horse.HorseId.Should().Be(396876);
            horse.HorseType.Should().NotBeNullOrWhiteSpace();
            horse.ImgUrl.Should().NotBeNullOrWhiteSpace();
            horse.IsTrialHorse.Should().BeFalse();
            horse.OwnerStable.Should().NotBeNullOrWhiteSpace();
            horse.Rating.Should().BeGreaterThan(0);
            horse.Skin.Should().BeNull();
            horse.SurfacePreference.Should().Be(50);
            horse.Type.Should().NotBeNullOrWhiteSpace();
            horse.WinRate.Should().BeGreaterThan(0);
        }
    }
}