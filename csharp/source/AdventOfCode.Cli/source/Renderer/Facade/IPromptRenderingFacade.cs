namespace AdventOfCode.Cli;

using Spectre.Console;

public interface IPromptRenderingFacade {
    T ChainingFunction<T>(Func<IPrompt<T>> action, bool newline = true) {
        return AnsiConsole.Prompt(action.Invoke());
    }

    string DrawPrompt(string message, string color = "blue");
    string DrawPromptOptions(string message, List<string> choises, string color = "blue");
    string DrawSelectionPrompt(string message, string desc, List<CommandInfo> choises, string color = "blue");
}
