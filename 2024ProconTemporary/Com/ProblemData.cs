using System.Text.Json.Serialization;

namespace _2024ProconTemporary.Com;

public abstract class ProblemData
{
    [JsonPropertyName("board")] public required BoardData Board { get; set; }

    [JsonPropertyName("general")] public required GeneralData General { get; set; }

    public abstract class BoardData
    {
        [JsonPropertyName("width")] public required int Width { get; set; }

        [JsonPropertyName("height")] public required int Height { get; set; }

        [JsonPropertyName("start")] public required IList<string> Start { get; set; }

        [JsonPropertyName("goal")] public required IList<string> Goal { get; set; }
    }

    public abstract class GeneralData
    {
        [JsonPropertyName("n")] public required int N { get; set; }

        [JsonPropertyName("patterns")] public required IList<PatternData> Patterns { get; set; }

        public abstract class PatternData
        {
            [JsonPropertyName("p")] public required int P { get; set; }

            [JsonPropertyName("width")] public required int Width { get; set; }

            [JsonPropertyName("height")] public required int Height { get; set; }

            [JsonPropertyName("cells")] public required IList<string> Cells { get; set; }
        }
    }
}