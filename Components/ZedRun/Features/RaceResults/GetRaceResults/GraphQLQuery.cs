namespace AWOMS.Zernest.Components.ZedRun.Features.RaceResults.GetRaceResults;
public static class GraphQLQuery
{
        public const string QueryText = @"
query ($input: GetRaceResultsInput, $before: String, $after: String, $first: Int, $last: Int) {
  getRaceResults(before: $before, after: $after, first: $first, last: $last, input: $input) {
    edges {
      cursor
      node {
        country
        city
        name
        length
        startTime
        fee
        raceId
        weather
        status
        class
        prizePool {
          first
          second
          third
        }
        horses {
          horseId
          finishTime
          finalPosition
          name
          gate
          ownerAddress
          bloodline
          gender
          breedType
          gen
          coat
          hexColor
          imgUrl
          class
          stableName
        }
      }
    }
    pageInfo {
      startCursor
      endCursor
      hasNextPage
      hasPreviousPage
    }
  }
}";
}
