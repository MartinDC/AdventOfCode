namespace AdventOfCode.Cli;

public class ConsoleConfig {
    public string MenuTitle { get; set; } = $"[bold fuchsia] MENU OPTIONS[/]";
    public string MenuDesc { get; set; } = $"[dim fuchsia] More choice text[/]";
    public bool RenderMenu { get; set; } = true;

    public Dictionary<int, ICommand> Commands { get; set; } = default!;
}