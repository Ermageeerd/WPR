using System;
using System.IO;
using Xunit;

namespace WPR.Tests;

public class SilverlightSupportTests
{
    [Fact]
    public void Windows_platform_contains_silverlight_launcher()
    {
        var repoRoot = FindRepoRoot();
        Assert.NotNull(repoRoot);

        var silverlightLauncherPath = Path.Combine(repoRoot!, "Src", "Platforms", "WPR.Platform.Windows", "SilverlightLauncher.cs");
        var xnaLauncherPath = Path.Combine(repoRoot!, "Src", "Platforms", "WPR.Platform.Windows", "XnaLauncher.cs");

        Assert.True(File.Exists(silverlightLauncherPath), "Silverlight launcher file is missing from the Windows platform project.");
        Assert.True(File.Exists(xnaLauncherPath), "XNA launcher file is missing from the Windows platform project.");
    }

    [Fact]
    public void Architecture_document_mentions_silverlight_launch_path()
    {
        var repoRoot = FindRepoRoot();
        Assert.NotNull(repoRoot);

        var architecturePath = Path.Combine(repoRoot!, "Docs", "ARCHITECTURE.md");
        Assert.True(File.Exists(architecturePath), "Architecture doc is missing from the Docs folder.");

        var text = File.ReadAllText(architecturePath);
        Assert.Contains("SilverlightLauncher.LaunchAsync", text, StringComparison.Ordinal);
        Assert.Contains("XnaLauncher", text, StringComparison.Ordinal);
    }

    [Fact]
    public void Manual_validation_document_exists_for_silverlight_baseline()
    {
        var repoRoot = FindRepoRoot();
        Assert.NotNull(repoRoot);

        var docPath = Path.Combine(repoRoot!, "Docs", "SILVERLIGHT-SUPPORT.md");
        Assert.True(File.Exists(docPath), "Silverlight support doc is missing.");

        var text = File.ReadAllText(docPath);
        Assert.Contains("TextBlock", text, StringComparison.Ordinal);
        Assert.Contains("Button", text, StringComparison.Ordinal);
        Assert.Contains("Click", text, StringComparison.OrdinalIgnoreCase);
    }

    private static string? FindRepoRoot()
    {
        var dir = new DirectoryInfo(AppContext.BaseDirectory);
        while (dir != null)
        {
            if (File.Exists(Path.Combine(dir.FullName, "WPR.sln")))
                return dir.FullName;
            dir = dir.Parent;
        }

        return null;
    }
}
