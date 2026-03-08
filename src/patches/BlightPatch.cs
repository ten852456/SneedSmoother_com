namespace PoeFixer;

public class BlightPatch : IPatch
{
    public string Extension => "*.fxgraph";

    public string[] DirectoriesToPatch =>
    [
        "metadata/effects/environment/league_blight/infection"
    ];

    public string[] FilesToPatch => [];

    public string? PatchFile(string text)
    {
        // Replace blight infection ground shader instructions with an empty graph
        // This stops the game from rendering the heavy fungal mycelium ground effects
        // and drops them from the VRAM loading profile.
        return "{\n  \"version\": 3,\n  \"nodes\": [],\n  \"links\": []\n}";
    }

    public bool ShouldPatch(Dictionary<string, bool> bools, Dictionary<string, float> floats)
    {
        bools.TryGetValue("removeBlight", out bool enabled);
        return enabled;
    }
}
