namespace AdventOfCode.Cli.Extension;

public static class MenuExtensions {
    public static List<CommandInfo> TranslateMenuCommands(this Dictionary<int, ICommand> commands) {
        return commands.Select(x => x.Value.TranslateCommandInfo()).ToList();
    }

    public static CommandInfo TranslateCommandInfo(this ICommand command) {
        var commandAttribute = command.GetConsoleCommandAttribute();
        return new CommandInfo {
            Name = commandAttribute?.Name ?? command.Info?.Name ?? "Unkown",
            Description = commandAttribute?.Description ?? string.Empty
        };
    }

    public static ConsoleTreeNode TranslateTreeNode(this ICommand command) {
        var nodeChilds = (command is IComposable<ConsoleCommand> composable) ? composable?.Composer?.Composition.Commands : null;
        return new ConsoleTreeNode() {
            Nodes = nodeChilds?.Select(child => child.TranslateTreeNode()).ToList(),
            Markup = $"[bold blue]{command.Info.Name}[/] - [dim grey]{command.Info.Description}[/]",
        };
    }
}
