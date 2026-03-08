namespace PoeFixer;

public class DeliriumPatch : IPatch
{
    public string[] FilesToPatch => [];

    public string[] DirectoriesToPatch => [
        "metadata/effects/environment/league_affliction"
        ];


    public string Extension => "*.ao|*.aoc";

    public string? PatchFile(string text)
    {
        if (string.IsNullOrEmpty(text)) return null;

        // fogAttachment.ao has a .aoc companion in the GGPK that cannot be
        // safely replaced. Skip it to avoid "non-virtual and does not have a
        // physical file" crash in the game engine.
        if (text.Contains("fogAttachment_ao_pass0.mat")) return null;

        if (text.Contains("Metadata/FmtParent"))
        {
            text = "version 2\nextends \"Metadata/FmtParent\"";
        }
        else if (text.Contains("default_animation = \"loop\""))
        {
            string[] separator = [Environment.NewLine];
            string[] lines = text.Split(separator, StringSplitOptions.RemoveEmptyEntries);
            IEnumerable<string> filteredLines = lines.Where(line => !line.Contains("default_animation = \"loop\""));
            text = string.Join(Environment.NewLine, filteredLines);
        }
        else if (text.Contains("BoneGroups"))
        {
            // Strip default_animation so the particle loop doesn't trigger,
            // and output a minimal file with empty client block.
            string[] separator = [Environment.NewLine];
            string[] lines = text.Split(separator, StringSplitOptions.RemoveEmptyEntries);
            IEnumerable<string> filteredLines = lines.Where(line => !line.Contains("default_animation"));
            text = string.Join(Environment.NewLine, filteredLines);
        }

        return text;
    }

    public bool ShouldPatch(Dictionary<string, bool> bools, Dictionary<string, float> floats)
    {
        bools.TryGetValue("removeDelirium", out bool enabled);
        return enabled;
    }
}