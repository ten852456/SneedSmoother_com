namespace PoeFixer;

public class DeliriumPatch : IPatch
{
    public string[] FilesToPatch => [];

    public string[] DirectoriesToPatch => [
        "metadata/effects/environment/league_affliction"
        ];


    public string Extension => "*.fxgraph";

    public string? PatchFile(string text)
    {
        // Replace the fog shader instructions with an empty graph
        // This renders the fog completely invisible without modifying .ao definitions
        // (which crash the game if touched due to a broken .aoc cache in the 3.28 GGPK).
        return "{\n  \"version\": 3,\n  \"nodes\": [],\n  \"links\": []\n}";
    }

    public bool ShouldPatch(Dictionary<string, bool> bools, Dictionary<string, float> floats)
    {
        bools.TryGetValue("removeDelirium", out bool enabled);
        return enabled;
    }
}