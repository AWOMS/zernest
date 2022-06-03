# Overview

Zernest is a tool to help ZedRun users analyze their horses and suggest what races to enter them into.

https://zernest.awoms.com/

# Roadmap

## Released

### v0.1.0

**Release Date:** 2022-06-03

<ul>
  <li>Integrate with ZedRun API v1 (REST)</li>
  <li>Integrate with ZedRun API v2 (GraphQL)</li>
  <li>Get horses by stable address</li>
  <li>Get horses by horse IDs</li>
  <li>Get full horse details</li>
  <li>Get open races</li>
  <li>Alert when open race matches criteria</li>
</ul>

## Future

> *Note: This is where ideas are captured. These may or may not get implemented, and the order is not guaranteed.*

- [ ] Get current tournaments
- [ ] Get horse race results history
- [ ] Analyze history of race conditions
- [ ] Analyze history of race results
- [ ] Identify horses rivals
- [ ] Identify horses strengths
- [ ] Identify horses unknowns
- [ ] Identify horses strategy (tournament final distances, class down)
- [ ] Identify races with opportunities matching strategy
- [ ] Alert when race is available I want my horse to run in
- [ ] Alert when race my horse is entered in is starting
- [ ] Identify trends (fav tracks, Thorograph) 

# Architecture

See "doc" folder for diagrams.

## Pattern

**Server > UI Page > UI Shared Component > Component**

- Servers host applications
- UI Pages use UI Shared Components
- UI Shared Components use Components
- Components features are all encompassing in their folder

**Tests > Component**

- Components tests ensure they are working as expected

## Details

**UI Page**

UI Pages are responsible for the User Experience (UX) and compose pages one or more UI Shared Components.

Example:

`Applications\UI.Server\Pages\OpenRaces.razor`
- uses `Applications.UI.Server.Shared.Components`
- calls `<GetOpenRacesComponent />`

**UI Shared Component**

UI Shared Components are re-useable Blazor components to access the underlying application components. They can be used on any page, or multiple on a page.

Example:

`Applications\UI.Server\Shared\Components\GetOpenRacesComponent.razor`
- uses `Components.Races.GetOpenRaces`
- implements `IDisposable` which is important to clean up any data they are re-usable
- injects `PersistentComponentState` which is important for solving the server-side pre-rendering flickering experience
- injects `GetOpenRacesCommandHandler` which is responsible for validating any input and orchestrate calling the underlying components

**Component**

 Components are the core features of the application, typically responsible for data input/outputs and could serve all clients front ends (UI, Mobile, API). 

 All of the external calls to ZedRun have been localized to a "ZedRun" component with various underlying "Features". Zernest components call the ZedRun component to get the data.

 `Components\Races\GetOpenRaces\GetOpenRacesCommandHandler.cs`
 - Domain models - the struture of objects our application uses
 - Command input DTO - user input if applicable
 - **Command handler** - validates the input DTO and calls the ZedRun feature, converting DTOs to domain models
 - Automapper profile - maps DTO/domain models

`Components\ZedRun\Features\Races\GetOpenRaces\GetOpenRacesFeature.cs`
- injects IMemoryCache which is responsible for caching data reads
- injects ZedRunApiService which is responsible for obtaining the external data transfer objects
- Using memory cache it will return data transfer objects from ZedRun if the cache is expired.

# Zernest Servers

- Applications\UI.Server - hosts the Web user interface (Blazor)

# Zernest UI

This is the Zernest web user interface.

- Pages
  - Horses - search for horse(s), view horse details
  - OpenRaces
  - Stable - search for stable, view horses
- Shared
  - Components
    - ChartComponent
    - ConnectWalletComponent
    - GetHorseIdsComponent
    - GetHorsesWithHistoryComponent
    - GetOpenRacesComponent
    - GetStableAddressComponent
    - GetStableHorsesComponent
- wwwroot
  - js
    - zernest
      - components
        - charts - used by ChartComponent
        - wallet - used by ConnectWalletComponent

## Icons

For icons we are using SVG from bootstrap icons

https://icons.getbootstrap.com/

https://useiconic.com/open#icons

# Zernest Components

- Horses
  - GetHorsesWithHistory
  - GetStableHorses
- Horses.Tests
- Races
  - GetOpenRaces
- Races.Tests
- TestHelpers
- ZedRun
  - API
  - Features
    - Horses
      - GetHorsesWithHistory - gets horses by ID(s)
      - GetStableHorses - gets horses by stable address
    - Races
      - GetOpenRaces - gets open races using API
      - GetOpenRacesV2 - gets open races using GraphQL
  - GraphQL
- ZedRun.Tests

## ZedRun

Calls to the ZedRun API must originate from an approved IP address.

The approved IP addresses must be listed in the `appsettings.json` under `Settings.AllowedOutboundIpAddresses`

# Zernest Models

These are shared models throughout the Zernest site.

- ZedRun
  - Classes
  - Distances
  - DistanceCategory
- Settings

# Development

`dotnet watch run --project "AWOMS.Zernest.Applications.UI.Server.csproj"`

# Web UI Deployment

1. Update version number in `appsettings.json` and `AWOMS.Zernest.Applications.UI.Server.csproj`
2. In AWOMS.Zernest.UI folder:

`dotnet publish -c Release`

This produces `bin\Release\.net6.0\publish` folder.

3. In VS code, right click `Applications\UI.Server` folder
4. Click Deploy to Web App

# ZedRun API v1:

- [x] https://racing-api.zed.run/api/v1/races?status=scheduling
- [x] https://racing-api.zed.run/api/v1/races?status=scheduled
- [x] https://racing-api.zed.run/api/v1/races?status=open
- [x] https://tournaments-api.zed.run/tournaments/current
- [x] https://api.zed.run/api/v1/horses/get_user_horses?public_address=0xfd0de0cad2a2ef78654681b5648e28250c814c7d
- [x] https://api.zed.run/api/v1/horses/get_horses/?horse_ids[]=393368&horse_ids[]=56249&horse_ids[]=209299&horse_ids[]=390676&horse_ids[]=142102&horse_ids[]=155136&horse_ids[]=364394&horse_ids[]=172473&horse_ids[]=13900&horse_ids[]=150213&horse_ids[]=390083

# Resources:

- https://getbootstrap.com/docs/5.1/getting-started/introduction/
- https://json2csharp.com/
- https://www.nuget.org/packages/GraphQL/
- https://graphql-dotnet.github.io/docs/getting-started/introduction
- https://referbruv.com/blog/posts/implementing-cqrs-using-mediator-in-aspnet-core-explained
- https://docs.microsoft.com/en-us/aspnet/core/blazor/components/lifecycle?view=aspnetcore-6.0

^-- This explains how to use OnInitializedAsync vs OnAfterRender

- https://docs.microsoft.com/en-us/aspnet/core/blazor/components/prerendering-and-integration?view=aspnetcore-6.0&pivots=server#persist-prerendered-state

^-- This solves the flickering problem from server prerendering.

- https://chrissainty.com/3-ways-to-communicate-between-components-in-blazor/
- https://docs.microsoft.com/en-us/aspnet/core/blazor/forms-validation?view=aspnetcore-6.0
- https://eth-converter.com/extended-converter.html