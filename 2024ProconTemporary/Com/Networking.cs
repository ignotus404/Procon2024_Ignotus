using System.Net;
using System.Text;
using System.Text.Json;
using DotNetEnv;

namespace _2024ProconTemporary.Com;

public static class Networking
{
    private static readonly string ENV_PATH = @"./.env";
    private static readonly string ServerIp = Env.GetString("SERVER_IP", "127.0.0.1");

    private static readonly string ServerPort = Env.GetString("SERVER_PORT", "3000");

    private static readonly string Token = Env.GetString("PROCON_TOKEN");

    private static readonly string AnswerEndPoint;
    private static readonly string ProblemEndPoint;

    static Networking()
    {
        Env.Load(ENV_PATH);
        ServerIp = Env.GetString("SERVER_IP", "127.0.0.1");
        ServerPort = Env.GetString("SERVER_PORT", "3000");
        Token = Env.GetString("PROCON_TOKEN", "natori4d166dbccdd7a5a875f1246f18bf46c1f9d85d9c6e2d8f1f05c82f85da");
        AnswerEndPoint = $"http://{ServerIp}:{ServerPort}/answer";
        ProblemEndPoint = $"http://{ServerIp}:{ServerPort}/problem";
        Console.WriteLine($"Server IP: {ServerIp}");
        Console.WriteLine($"Server Port: {ServerPort}");
        Console.WriteLine($"Token: {Token}");
    }


    public static HttpClient CreateClient()
    {
        return new HttpClient();
    }

    // ReSharper disable once MemberCanBePrivate.Global
    public static async Task<ProblemData?> GetProblemDataAsync(HttpClient client)
    {
        var req = new HttpRequestMessage(HttpMethod.Get, ProblemEndPoint);
        AddTokenHeader(ref req);
        try
        {
            var res = await client.SendAsync(req);
            if (!res.StatusCode.Equals(HttpStatusCode.OK))
            {
                await Console.Error.WriteLineAsync($"Failed to get problem data. Status Code is {res.StatusCode}");
                return null;
            }

            var body = await res.Content.ReadAsStreamAsync();
            var obj = (await JsonSerializer.DeserializeAsync<ProblemData>(body))!;
            return obj;
        }
        catch (HttpRequestException e)
        {
            await Console.Error.WriteLineAsync(
                $"Failed to get problem data due to network error. {e.HttpRequestError}");
            return null;
        }
    }

    public static ProblemData? GetProblemData(HttpClient client)
    {
        return GetProblemDataAsync(client).Result;
    }

    // ReSharper disable once MemberCanBePrivate.Global
    public static async Task<bool> SendAnswerDataAsync(HttpClient client, AnswerData answer)
    {
        var req = new HttpRequestMessage(HttpMethod.Post, AnswerEndPoint);
        AddTokenHeader(ref req);
        using var jsonStream = new MemoryStream();
        await JsonSerializer.SerializeAsync(jsonStream, answer);
        var json = jsonStream.ToString()!;
        req.Content = new StringContent(json, Encoding.UTF8, "application/json");
        try
        {
            var res = await client.SendAsync(req);
            if (res.StatusCode.Equals(HttpStatusCode.OK)) return true;
            await Console.Error.WriteLineAsync($"Failed to send answer data. Status Code is {res.StatusCode}");
            return false;
        }
        catch (HttpRequestException e)
        {
            await Console.Error.WriteLineAsync(
                $"Failed to send answer data due to network error. {e.HttpRequestError}");
            return false;
        }
    }

    public static bool SendAnswerData(HttpClient client, AnswerData answer)
    {
        return SendAnswerDataAsync(client, answer).Result;
    }

    private static void AddTokenHeader(ref HttpRequestMessage req)
    {
        req.Headers.Add("Procon-Token", Token);
    }
}