using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AWOMS.Zernest.Components.ZedRun.Features.Races.GetOpenRacesV2;
public static class Query
{
    public static string Text = @"
query ($input: GetRaceByStatusInput, $before: String, $after: String, $first: Int, $last: Int) {
  races: get_race_by_status(
    before: $before
    after: $after
    first: $first
    last: $last
    input: $input
  ) {
    edges {
      cursor
      node {
        ...RaceCommonDetails
        eventType: event_type {
          title
          description
          labels {
            name
            description
            __typename
          }
          __typename
        }
        raceFactor: race_factor {
          surfaceValue: surface_value
          __typename
        }
        prize: prize_pool {
          first
          second
          third
          total
          __typename
        }
        prizePool: prize_pool {
          first
          second
          third
          total
          __typename
        }
        __typename
      }
      __typename
    }
    pageInfo: page_info {
      startCursor: start_cursor
      endCursor: end_cursor
      hasNextPage: has_next_page
      hasPreviousPage: has_previous_page
      __typename
    }
    __typename
  }
}

fragment RaceCommonDetails on Race {
  raceId: race_id
  startTime: start_time
  name
  length
  class
  fee
  status
  weather
  city
  country
  prize: prize_pool {
    first
    second
    third
    total
    __typename
  }
  countryCode: country_code
  horses {
    horseId: horse_id
    breedType: breed_type
    genotype: gen
    hexCode: hex_color
    owner: owner_address
    stable: stable_name
    winRate: win_rate
    skin {
      name
      image
      texture
      __typename
    }
    gate
    bloodline
    career
    rating
    class
    coat
    gender
    name
    races
    rating
    __typename
  }
  __typename
}
";
}