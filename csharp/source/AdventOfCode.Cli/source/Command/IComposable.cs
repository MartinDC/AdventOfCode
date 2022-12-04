namespace AdventOfCode.Cli;

public interface IComposable<T> where T : ICommand {
    ICommandComposer<T>? Composer { get; }

    T Compose(Action<CommandComposition<T>> action);
    public T? GetComposedFromResult();
}

public interface ICommandComposer<T> where T : ICommand {
    CommandComposition<T> Composition { get; }
    ICommand Parent { get; set; }

    int CommandIndex { get; set; }

    void Next() => CommandIndex++;
    bool Has() => Composition.ElementAtOrDefault(CommandIndex) is not null;
    
    List<string> Names() => Composition.GetNames();

    T? GetNext() => Composition.ElementAtOrDefault(CommandIndex++);
    T? Get(int idx) => Composition.ElementAtOrDefault(idx);
}
