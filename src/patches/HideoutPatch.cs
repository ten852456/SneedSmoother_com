namespace PoeFixer;

public class HideoutPatch : IPatch
{
    public string Extension => "*.fxgraph";

    public string[] DirectoriesToPatch =>
    [
        "metadata/effects/environment/hideout",
        "metadata/effects/microtransactions/hideout",
        "metadata/effects/microtransactions/mapdevice"
    ];

    public string[] FilesToPatch => [];

    public string? PatchFile(string text)
    {
        // Replace hideout environment and MTX decoration shaders with an empty graph
        // This renders massive player-placed decorations, glowing map devices, and ambient
        // environment effects completely invisible. It forces the engine to skip loading 
        // hundreds of heavy textures into VRAM, drastically improving load times between hideouts.
        return "{\n  \"version\": 3,\n  \"nodes\": [],\n  \"links\": []\n}";
    }

    public bool ShouldPatch(Dictionary<string, bool> bools, Dictionary<string, float> floats)
    {
        bools.TryGetValue("removeHideoutFX", out bool enabled);
        return enabled;
    }
}
