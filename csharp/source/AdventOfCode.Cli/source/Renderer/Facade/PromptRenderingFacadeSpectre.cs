namespace AdventOfCode.Cli;

/**
    A Facade for Spectre.Console - Simplify rendering prompts to the console and hide Spectre.Console details.
*/

using Spectre.Console;

public class PromptRenderingFacadeSpectre : IPromptRenderingFacade {
    private IPromptRenderingFacade DefaultImpl => this;
    
    public T DrawPrompt<T>(string message, Action<TextPrompt<T>> performConfiguration) {
        return DefaultImpl.ChainingFunction<T>(() => {
            TextPrompt<T> prompt = new TextPrompt<T>(message);
            if (performConfiguration is not null) {
                performConfiguration(prompt);
            }
            return prompt;
        });
    }

    public T DrawSelection<T>(string message, string desc, Action<SelectionPrompt<T>> performConfiguration) where T : notnull {
        return DefaultImpl.ChainingFunction<T>(() => {
            SelectionPrompt<T> prompt = new SelectionPrompt<T>();
            if (performConfiguration is not null) {
                performConfiguration(prompt);
            }
            return prompt;
        });
    }

    public string DrawPrompt(string message, string color = "blue") {
        return DrawPrompt<string>(message, (prompt) => prompt.PromptStyle(color));
    }

    public string DrawPromptOptions(string message, List<string> choises, string color = "blue") {
        return DrawPrompt<string>(message, (prompt) => prompt.PromptStyle(color)
            .AddChoices(choises).DefaultValue(choises.First()));
    }

    public string DrawSelectionPrompt(string message, string desc, List<CommandInfo> choises, string color = "blue") {
        return DrawSelection<CommandInfo>(message, desc, (prompt) => prompt.HighlightStyle(color)
            .AddChoices(choises).MoreChoicesText(desc).UseConverter(x => $"{x.Name} - {x.Description}")).Name;
    }
}
