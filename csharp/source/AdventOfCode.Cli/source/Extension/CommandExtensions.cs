namespace AdventOfCode.Cli.Extension;

using static AdventOfCode.Cli.Vars.Vars;

public static class CommandExtensions {
    public static bool IsYes(this ICommandResult item) => item.StringValue?.Equals(PostitiveReply) ?? throw new CommandExpectedConfirmationException();
    public static bool IsNo(this ICommandResult item) => item.StringValue?.Equals(NegativeReply) ?? throw new CommandExpectedConfirmationException();

    public static string AsNormalizedName(this string item) => item.Replace("Day", String.Empty).ToLower();

    public static ICommand? AsComposedCommandFromResult(this ICommandResult result) {
        if (result is null || result.Parent is not ConsoleCommand command) { return null; }
        return command?.Composer?.Composition.Commands.Select((command, index) => new { command, index })
            .SingleOrDefault(x => x.index == result.IntValue || x.command.Info.Name.ToLower().Equals(result.StringValue!.ToLower()))?.command;
    } 

    public static ICommandResult UseResult<T>(this ConsoleCommand command, Func<T> fn) {
        if (fn is not null) { command.Result = new ConsoleCommandResult { Data = fn(), Parent = command }; }
        return command.Result;
    }

    public static ICommandResult ForResult(this ICommandResult res, Action<ICommandResult> fn) {
        if (fn is not null) { fn(res); }
        return res;
    }

    public static ICommandResult ForSolution<T>(this ICommandResult result, Func<string, T> process, Func<T, int> s1, Func<T, int> s2) {
        return result.ForResult((d) => {
            ConsolePrompt.GetRenderer().DrawText($@"Got it:
                [yellow]Solution 1: [bold]{s1(process(d.StringValue ?? string.Empty))}[/][/]
                [yellow]Solution 2: [bold]{s2(process(d.StringValue ?? string.Empty))}[/][/]
            ");
        });
    }
}