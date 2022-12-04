namespace AdventOfCode.Cli;

using AdventOfCode.Cli.Extension;

public class CommandExecutor {
    public bool IsConsumingCommands { get; set; } = true;

    public void Execute(string commandName, Dictionary<int, ICommand> commands) {
        var commandPredicate = (KeyValuePair<int, ICommand> item, string? name, int? number) => 
            item.Key == number || item.Value.Info.Name.Equals(name);

        while (IsConsumingCommands) {
            try {
                var command = commands.SingleOrDefault(x => commandPredicate(x, commandName, null));
                TryExecuteCommand(command.Value);
            } catch (CommandException e) {
                ConsolePrompt.GetRenderer().DrawText(e.Message);
                continue;
            }
            break;
        }
    }

    private ICommand? TryExecuteCommand(ICommand? command) {
        var result = command?.Execute() ?? throw new CommandParseException($"Failed to parse command {nameof(command)}");
        if (command is IComposable<ConsoleCommand> composable) { // Execute leafs/children
            if (composable?.Composer is not null && composable.Composer.Has()) {
                TryExecuteCommand(composable?.GetComposedFromResult());
            }
        };
        return command;
    }
}