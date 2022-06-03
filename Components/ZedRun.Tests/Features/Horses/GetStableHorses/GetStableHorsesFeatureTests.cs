using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using AWOMS.Zernest.Components.ZedRun.API;
using AWOMS.Zernest.Components.ZedRun.Features.Horses.GetStableHorses;
using Microsoft.Extensions.Configuration;
using FluentAssertions;
using System.Net.Http;
using Xunit.Abstractions;
using AWOMS.Zernest.Components.TestHelpers;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Caching.Memory;
using System.Collections.Specialized;
using Microsoft.Extensions.Internal;
using Microsoft.Extensions.Options;

namespace AWOMS.Zernest.Components.ZedRun.Tests.Features.Horses.GetStableHorses
{
    public class GetStableHorsesFeatureTests
    {
        private readonly ITestOutputHelper _testOutputHelper;
        private readonly IMemoryCache _memoryCache;
        private readonly HttpClient _httpClient;
        private readonly IConfigurationRoot _config;
        long? NO_SIZE_LIMIT = null;
        
        public GetStableHorsesFeatureTests(ITestOutputHelper testOutputHelper)
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
        public async Task GetStableHorses_StableAddress_ReturnsListStableHorseDTO()
        {
            //arrange
            var stableAddress = "0xfD0De0CAd2A2Ef78654681b5648e28250c814C7D";
            var zedRunApiService = new ZedRunApiService(XUnitLogger.CreateLogger<ZedRunApiService>(_testOutputHelper),
                _config, _httpClient, _memoryCache);
            var feat = new GetStableHorsesFeature(XUnitLogger.CreateLogger<GetStableHorsesFeature>(_testOutputHelper),
                _memoryCache, zedRunApiService);

            //act
            var horses = await feat.GetStableHorses(stableAddress);

            //assert
            horses.Should().NotBeNull();
            horses.Should().AllBeOfType<StableHorseDTO>();
            horses.Should().HaveCountGreaterThan(2);

            var horse = horses.Single(x => x.HorseId == 396876);
            horse.Should().NotBeNull();
            horse.Bloodline.Should().NotBeNullOrWhiteSpace();
            horse.BreedType.Should().NotBeNullOrWhiteSpace();
            horse.BreedingCounter.Should().BeGreaterThan(0);
            horse.BreedingCycleReset.Should().BeAfter(DateTime.Now);
            horse.BreedingDecayLevel.Should().Be(0);
            horse.BreedingDecayLimit.Should().BeGreaterThan(0);
            horse.Career.Should().NotBeNull();
            horse.Career.First.Should().BeGreaterThan(0);
            horse.Career.Second.Should().BeGreaterThan(0);
            horse.Career.Third.Should().BeGreaterThan(0);
            horse.Class.Should().BeGreaterThan(0);
            horse.CurrentFatigue.Should().BeNull();
            horse.Gender.Should().NotBeNullOrWhiteSpace();
            horse.Genotype.Should().NotBeNullOrWhiteSpace();
            horse.HashInfo.Should().NotBeNull();
            horse.HashInfo.Color.Should().NotBeNullOrWhiteSpace();
            horse.HashInfo.HexCode.Should().NotBeNullOrWhiteSpace();
            horse.HashInfo.Name.Should().NotBeNullOrWhiteSpace();
            horse.HorseId.Should().Be(396876);
            horse.ImgUrl.Should().NotBeNullOrWhiteSpace();
            horse.IneligibleReason.Should().BeNull();
            horse.IsApprovedForRacing.Should().BeFalse();
            horse.IsAvailable.Should().BeTrue();
            horse.IsBeingTransferred.Should().BeFalse();
            horse.IsEligible.Should().BeNull();
            horse.IsInStud.Should().BeFalse();
            horse.IsOnRacingContract.Should().BeFalse();
            horse.IsRunningFreeRace.Should().BeFalse();
            horse.IsUpgraded.Should().BeTrue();
            horse.LastStudDuration.Should().Be(0);
            horse.LastStudTimestamp.Should().Be(0);
            horse.MatingPrice.Should().NotBeNull();
            horse.NumberOfRaces.Should().BeGreaterThan(0);
            horse.OffspringCount.Should().Be(0);
            horse.OffspringCountAfterDecay.Should().Be(0);
            horse.Owner.Should().NotBeNull();
            horse.Rating.Should().BeGreaterThan(0);
            horse.Skin.Should().BeNull();
            horse.SuperCoat.Should().BeFalse();
            horse.SurfacePreference.Should().Be(50);
            horse.Tx.Should().NotBeNullOrWhiteSpace();
            horse.TxDate.Should().BeBefore(DateTime.Now);
            horse.WinRate.Should().BeGreaterThan(0);
        }
    }
}