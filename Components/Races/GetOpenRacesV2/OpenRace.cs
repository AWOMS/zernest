using System;
using System.Collections.Generic;

namespace AWOMS.Zernest.Components.Races.GetOpenRacesV2;

public class Data
{
    public Races Races { get; set; }
}

public class Edge
{
    public string Typename { get; set; }
    public string Cursor { get; set; }
    public OpenRace OpenRace { get; set; }
}

public class EventType
{
    public string Typename { get; set; }
    public string Description { get; set; }
    public IReadOnlyList<Label> Labels { get; set; }
    public string Title { get; set; }
}

public class Horse
{
    public string Typename { get; set; }
    public string Bloodline { get; set; }
    public string BreedType { get; set; }
    public string Career { get; set; }
    public int Class { get; set; }
    public string Coat { get; set; }
    public string Gate { get; set; }
    public string Gender { get; set; }
    public string Genotype { get; set; }
    public string HexCode { get; set; }
    public int HorseId { get; set; }
    public string Name { get; set; }
    public string Owner { get; set; }
    public int Races { get; set; }
    public double Rating { get; set; }
    public Skin Skin { get; set; }
    public string Stable { get; set; }
    public double WinRate { get; set; }
}

public class Label
{
    public string Typename { get; set; }
    public string Description { get; set; }
    public string Name { get; set; }
}

public class OpenRace
{
    public string Typename { get; set; }
    public string City { get; set; }
    public int Class { get; set; }
    public string Country { get; set; }
    public string CountryCode { get; set; }
    public EventType EventType { get; set; }
    public string Fee { get; set; }
    public IReadOnlyList<Horse> Horses { get; set; }
    public int Length { get; set; }
    public string Name { get; set; }
    public Prize Prize { get; set; }
    public PrizePool PrizePool { get; set; }
    public RaceFactor RaceFactor { get; set; }
    public string RaceId { get; set; }
    public DateTime? StartTime { get; set; }
    public string Status { get; set; }
    public string Weather { get; set; }
}

public class PageInfo
{
    public string Typename { get; set; }
    public string EndCursor { get; set; }
    public bool HasNextPage { get; set; }
    public bool HasPreviousPage { get; set; }
    public string StartCursor { get; set; }
}

public class Prize
{
    public string Typename { get; set; }
    public string First { get; set; }
    public string Second { get; set; }
    public string Third { get; set; }
    public string Total { get; set; }
}

public class PrizePool
{
    public string Typename { get; set; }
    public string First { get; set; }
    public string Second { get; set; }
    public string Third { get; set; }
    public string Total { get; set; }
}

public class RaceFactor
{
    public string Typename { get; set; }
    public string SurfaceValue { get; set; }
}

public class Races
{
    public string Typename { get; set; }
    public IReadOnlyList<Edge> Edges { get; set; }
    public PageInfo PageInfo { get; set; }
}

public class Root
{
    public Data Data { get; set; }
}

public class Skin
{
    public string Typename { get; set; }
    public string Image { get; set; }
    public string Name { get; set; }
    public string Texture { get; set; }
}
