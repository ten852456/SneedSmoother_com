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

        // All FmtParent-based .ao files in the delirium folder have .aoc
        // companions in the GGPK that cannot be safely replaced. Patching
        // the .ao without matching .aoc causes a game engine crash.
        if (text.Contains("Metadata/FmtParent")) return null;

        if (text.Contains("default_animation = \"loop\""))
        {
            string[] separator = [Environment.NewLine];
            string[] lines = text.Split(separator, StringSplitOptions.RemoveEmptyEntries);
            IEnumerable<string> filteredLines = lines.Where(line => !line.Contains("default_animation = \"loop\""));
            text = string.Join(Environment.NewLine, filteredLines);
        }
        else if (text.Contains("BoneGroups"))
        {
            string[] separator = [Environment.NewLine];
            string[] lines = text.Split(separator, StringSplitOptions.RemoveEmptyEntries);
            IEnumerable<string> filteredLines = lines.Where(line => !line.Contains("default_animation"));
            text = string.Join(Environment.NewLine, filteredLines);
        }

        return text;
    }

    public bool ShouldPatch(Dictionary<string, bool> bools, Dictionary<string, float> floats)
    {
        // Delirium patch is disabled. The 3.28 GGPK has a broken fogAttachment.aoc 
        // compiled cache. Patching any .ao file in this directory triggers an engine 
        // revalidation that crashes when it hits the broken .aoc.
        // Delirium particles are already handled by ParticlePatch.
        return false;
    }
}