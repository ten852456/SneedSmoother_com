namespace PoeFixer;

public class WeatherPatch : IPatch
{
    public string Extension => "*.fxgraph";

    public string[] DirectoriesToPatch =>
    [
        "metadata/effects/environment/rain",
        "metadata/effects/weather"
    ];

    public string[] FilesToPatch => [];

    public string? PatchFile(string text)
    {
        // Replace weather shader instructions with an empty graph
        // This renders rain and snow completely invisible and stops the game
        // from loading the heavy weather textures into VRAM.
        return "{\n  \"version\": 3,\n  \"nodes\": [],\n  \"links\": []\n}";
    }

    public bool ShouldPatch(Dictionary<string, bool> bools, Dictionary<string, float> floats)
    {
        bools.TryGetValue("removeWeather", out bool enabled);
        return enabled;
    }
}
