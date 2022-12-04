namespace AdventOfCode.Cli;

using AdventOfCode.Cli.Extension;

public class ConsoleCommandResult : ICommandResult {
    public ICommand? Parent { get; set; }
    public object? Data { get; set; }
}

public abstract class ConsoleCommand : ICommand, IComposable<ConsoleCommand> {
    public ICommandComposer<ConsoleCommand>? Composer { get; private set; } = default!;
    public ICommandResult Result { get; set; } = default!;
    public CommandInfo Info { get; set; }

    public ConsoleCommand(string name) {
        Info = new CommandInfo(name.AsNormalizedName(), string.Empty);
    }

    public ConsoleCommand Compose(Action<CommandComposition<ConsoleCommand>> composeaction) {
        Composer ??= new ConsoleCommandComposer() { Parent = this };
        composeaction(Composer.Composition);
        return this;
    }

    public ConsoleCommand? GetComposedFromResult() => Result.AsComposedCommandFromResult() as ConsoleCommand;
    public virtual ICommandResult Execute() => default!;
}