using AWOMS.Zernest.Components.Horses.GetHorsesWithHistory;
using AWOMS.Zernest.Components.Horses.GetStableHorses;
using AWOMS.Zernest.Components.Horses.GetFullHorseDetails;
using AWOMS.Zernest.Components.Races.GetOpenRacesV2;
using AWOMS.Zernest.Components.ZedRun;
using AWOMS.Zernest.Models;
using FluentValidation;
using Microsoft.Extensions.Internal;
using Serilog;
using Majorsoft.Blazor.Components.CssEvents;
using Majorsoft.Blazor.Components.Notifications;

var builder = WebApplication.CreateBuilder(args);

//logging
builder.Host.UseSerilog((ctx, lc) => lc
        //.WriteTo.Console()
        .ReadFrom.Configuration(ctx.Configuration));

//http client
builder.Services.AddHttpClient();

// if enabled, ensure we're allowed to use zedrun api from this IP
var config = new ConfigurationBuilder()
                    .SetBasePath(AppContext.BaseDirectory)
                    .AddJsonFile("appsettings.json", false, true)
                    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
                    .Build();
var settings = config.GetRequiredSection("Settings").Get<Settings>();
if (settings.EnforceOutboundIpCheck)
{
    using (var httpClient = new HttpClient())
    {
        if (!await HttpHelpers.IsOutboundIpAddressAllowed(
            httpClient,
            settings.OutboundIpSource,
            settings.AllowedOutboundIpAddresses))
        {
            throw new UnauthorizedAccessException("Outbound IP address does not pass allowed list check.");
        }
    }
}

//notifications
builder.Services.AddCssEvents();
builder.Services.AddNotifications();

//automapper
builder.Services.AddAutoMapper(typeof(StableHorseProfile));
builder.Services.AddAutoMapper(typeof(HorseWithHistoryProfile));
builder.Services.AddAutoMapper(typeof(OpenRaceProfile));
builder.Services.AddAutoMapper(typeof(FullHorseDetailsProfile));

//zedrun features
builder.Services.AddScoped<AWOMS.Zernest.Components.ZedRun.API.ZedRunApiService>();
builder.Services.AddScoped<AWOMS.Zernest.Components.ZedRun.GraphQL.ZedRunGraphQLService>();

builder.Services.AddScoped<AWOMS.Zernest.Components.ZedRun.Features.Horses.GetHorsesWithHistory.GetHorsesWithHistoryFeature>();
builder.Services.AddScoped<AWOMS.Zernest.Components.ZedRun.Features.Horses.GetStableHorses.GetStableHorsesFeature>();
builder.Services.AddScoped<AWOMS.Zernest.Components.ZedRun.Features.Races.GetOpenRacesV2.GetOpenRacesFeature>();

//zernest components
builder.Services.AddScoped<IValidator<AWOMS.Zernest.Components.Horses.GetHorsesWithHistory.GetHorsesWithHistoryInputDTO>, AWOMS.Zernest.Components.Horses.GetHorsesWithHistory.GetHorsesWithHistoryInputDTOValidator>();
builder.Services.AddScoped<AWOMS.Zernest.Components.Horses.GetHorsesWithHistory.GetHorsesWithHistoryCommandHandler>();

builder.Services.AddScoped<IValidator<AWOMS.Zernest.Components.Horses.GetStableHorses.GetStableHorsesInputDTO>, AWOMS.Zernest.Components.Horses.GetStableHorses.GetStableHorsesInputDTOValidator>();
builder.Services.AddScoped<AWOMS.Zernest.Components.Horses.GetStableHorses.GetStableHorsesCommandHandler>();

builder.Services.AddScoped<IValidator<AWOMS.Zernest.Components.Horses.GetFullHorseDetails.GetFullHorseDetailsInputDTO>, AWOMS.Zernest.Components.Horses.GetFullHorseDetails.GetFullHorseDetailsInputDTOValidator>();
builder.Services.AddScoped<AWOMS.Zernest.Components.Horses.GetFullHorseDetails.GetFullHorseDetailsCommandHandler>();

builder.Services.AddScoped<IValidator<AWOMS.Zernest.Components.Races.GetOpenRacesV2.GetOpenRacesInputDTO>, AWOMS.Zernest.Components.Races.GetOpenRacesV2.GetOpenRacesInputDTOValidator>();
builder.Services.AddScoped<AWOMS.Zernest.Components.Races.GetOpenRacesV2.GetOpenRacesCommandHandler>();

// server
builder.Services.AddRazorPages();

builder.Services.AddServerSideBlazor(options =>
{
    options.DetailedErrors = true;
    options.DisconnectedCircuitMaxRetained = 100;
    options.DisconnectedCircuitRetentionPeriod = TimeSpan.FromMinutes(3);
    options.JSInteropDefaultCallTimeout = TimeSpan.FromMinutes(1);
    options.MaxBufferedUnacknowledgedRenderBatches = 10;
});

builder.Services.AddMemoryCache(x =>
{
    x.Clock = new SystemClock();
    x.CompactionPercentage = 1;
    x.ExpirationScanFrequency = TimeSpan.FromSeconds(100);
    x.SizeLimit = null;
});

//swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//app
var app = builder.Build();

//logging extended
app.UseSerilogRequestLogging();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

if (app.Environment.IsDevelopment())
{
    app.MapSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();
app.MapRazorPages();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
