using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

public class Program
{
    private const string ApiUrl = "https://jsonmock.hackerrank.com/api/football_matches";

    public static async Task Main()
    {
        await GetAndPrintTotalScoredGoals("Paris Saint-Germain", 2013);
        await GetAndPrintTotalScoredGoals("Chelsea", 2014);
    }

    public static async Task GetAndPrintTotalScoredGoals(string teamName, int year)
    {
        int totalGoals = await GetTotalScoredGoals(teamName, year);

        Console.WriteLine($"Team {teamName} scored {totalGoals} goals in {year}");
    }

    public static async Task<int> GetTotalScoredGoals(string teamName, int year)
    {
        int totalGoals = 0;

        // Consulta para o time como team1
        totalGoals += await GetTotalGoalsForTeam(teamName, year, "team1");

        // Consulta para o time como team2
        totalGoals += await GetTotalGoalsForTeam(teamName, year, "team2");

        return totalGoals;
    }

    public static async Task<int> GetTotalGoalsForTeam(string teamName, int year, string teamField)
    {
        string apiUrlWithParams = $"{ApiUrl}?year={year}&{teamField}={teamName}";

        using (var httpClient = new HttpClient())
        {
            var response = await httpClient.GetAsync(apiUrlWithParams);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var data = JObject.Parse(content);

                if (data["data"] != null)
                {
                    int totalGoals = 0;

                    foreach (var match in data["data"])
                    {
                        totalGoals += int.Parse(match[$"{teamField}goals"].ToString());
                    }

                    return totalGoals;
                }
                else
                {
                    Console.WriteLine($"No data available for {teamName} in {year}");
                    return 0;
                }
            }
            else
            {
                Console.WriteLine($"Failed to get data for {teamName} in {year}");
                return 0;
            }
        }
    }
}
