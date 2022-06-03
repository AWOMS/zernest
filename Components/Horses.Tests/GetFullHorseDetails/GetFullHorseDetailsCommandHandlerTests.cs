// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;
// using Xunit;
// using AWOMS.Zernest.Components.ZedRun.API;
// using AWOMS.Zernest.Components.ZedRun.Features.Horses.GetStableHorses;
// using Microsoft.Extensions.Configuration;
// using FluentAssertions;
// using System.Net.Http;
// using Xunit.Abstractions;
// using AWOMS.Zernest.Components.TestHelpers;
// using Xunit.Abstractions;
// using Microsoft.Extensions.Logging;
// using Microsoft.Extensions.Caching.Memory;
// using System.Collections.Specialized;
// using Microsoft.Extensions.Internal;
// using Microsoft.Extensions.Options;
// using AWOMS.Zernest.Components.Horses.GetFullHorseDetails;
// using AWOMS.Zernest.Components.ZedRun.Features.Horses.GetHorsesWithHistory;

// namespace AWOMS.Zernest.Components.Horses.Tests.GetFullHorseDetails
// {
//     public class GetFullHorseDetailsCommandHandlerTests
//     {
//         private readonly ITestOutputHelper _testOutputHelper;
//         private readonly IMemoryCache _memoryCache;
//         private readonly HttpClient _httpClient;
//         private readonly IConfigurationRoot _config;
//         long? NO_SIZE_LIMIT = null;

//         public GetFullHorseDetailsCommandHandlerTests(ITestOutputHelper testOutputHelper)
//         {
//             _testOutputHelper = testOutputHelper;

//             _config = new ConfigurationBuilder()
//                                 .SetBasePath(AppContext.BaseDirectory)
//                                 .AddJsonFile("appsettings.Test.json", false, true)
//                                 .Build();

//             var options = new MemoryCacheOptions()
//             {
//                 Clock = new SystemClock(),
//                 CompactionPercentage = 1,
//                 ExpirationScanFrequency = TimeSpan.FromSeconds(100),
//                 SizeLimit = NO_SIZE_LIMIT
//             };
//             IOptions<MemoryCacheOptions> optionsAccessor = Options.Create(options);
//             _memoryCache = new MemoryCache(optionsAccessor);

//             _httpClient = new HttpClient();
//         }

//         [Fact]
//         public async Task Test1()
//         {
//             //arrange
//             var zedRunApiService = new ZedRunApiService(XUnitLogger.CreateLogger<ZedRunApiService>(_testOutputHelper),
//                 _config, _httpClient, _memoryCache);

//             var getStableHorsesFeature = new GetStableHorsesFeature(XUnitLogger.CreateLogger<GetStableHorsesFeature>(_testOutputHelper),
//                 _memoryCache, zedRunApiService);
            
//             var getHorsesWithHistoryFeature = new GetHorsesWithHistoryFeature(XUnitLogger.CreateLogger<GetHorsesWithHistoryFeature>(_testOutputHelper),
//                 _memoryCache, zedRunApiService);

//             var horseId = "396876";

//             var cmd = new GetFullHorseDetailsCommandHandler();


//         }
//     }
// }