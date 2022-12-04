namespace AdventOfCode.Cli;

public interface ICommand {
    CommandInfo Info { get; internal set; }
    ICommandResult Result { get; set; }

    ICommandResult Execute();
}

public interface ICommandResult {
    ICommand? Parent { get; set; }
    object? Data { get; set; }

    string? StringValue => Data as string;
    int? IntValue => Data as int?;
}