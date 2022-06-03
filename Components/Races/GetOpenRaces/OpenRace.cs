using System;
using System.Collections.Generic;

namespace AWOMS.Zernest.Components.Races.GetOpenRaces;

public class OpenRace
{
	public string City { get; set; }

	public int Class { get; set; }

	public string Country { get; set; }

	public string CountryCode { get; set; }

	public string EventType { get; set; }

	public object ExtendedTimeoutAt { get; set; }

	public string Fee { get; set; }

	public object FinalPositions { get; set; }

	public Gates Gates { get; set; }

	public int Length { get; set; }

	public string Name { get; set; }

	public Prize Prize { get; set; }

	public RaceFactor RaceFactor { get; set; }

	public string RaceId { get; set; }

	public string RacetrackCode { get; set; }

	public List<Rule> Rules { get; set; }

	public List<object> Sponsors { get; set; }

	public DateTime StartTime { get; set; }

	public string Status { get; set; }

	public object TimeoutAt { get; set; }

	public string Weather { get; set; }
}
