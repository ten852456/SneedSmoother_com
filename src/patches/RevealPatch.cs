namespace PoeFixer;

public class RevealPatch : IPatch
{
    public string[] FilesToPatch => ["shaders/minimap_visibility_pixel.hlsl"];
    public string[] DirectoriesToPatch => [];
    public string Extension => "*";

    public bool ShouldPatch(Dictionary<string, bool> bools, Dictionary<string, float> floats)
    {
        bools.TryGetValue("revealEnabled", out bool enabled);
        return enabled;
    }

    public string? PatchFile(string text)
    {
        return text
            .Replace("res_color = float4(0.0f, 0.0f, 0.0f, 1.0f);", "return float4(0.0f, 0.0f, 0.0f, 1.0f);")
            .Replace("res_color = float4(1.0f, 0.0f, 0.0f, 1.0f);", "res_color.r = max(res_color.r, 1.0f);")
            .Replace("return res_color;", "res_color.r = max(res_color.r, 0.18f);\n\t\treturn res_color;");
    }
}