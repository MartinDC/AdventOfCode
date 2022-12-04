namespace AdventOfCode.Cli;

public class CommandComposition<T> where T : ICommand {
    public List<T> Commands { get; } = new();

    public T? ElementAtOrDefault(int index) => Commands.ElementAtOrDefault(index);
    public List<string> GetNames() => Commands.Select(x => x.Info.Name).ToList();
    public void AddCommand(T command) => Commands.Add(command);
}

public class ConsoleCommandComposer : ICommandComposer<ConsoleCommand> {
    public CommandComposition<ConsoleCommand> Composition { get; set;} = new();
    public ICommand Parent { get; set; } = default!;
    public int CommandIndex { get; set; } = 0;
}