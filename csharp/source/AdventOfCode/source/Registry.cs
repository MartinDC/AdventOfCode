namespace AdventOfCode;

using System.Linq;

using AdventOfCode.Cli;
using AdventOfCode.Cli.Annotation;
using AdventOfCode.Cli.Extension;

[CommandAttribute("Solutions for the year of 2022")]
public class Registry: ConsoleCommand {
    public Registry() : base("AdventOfCode22") { }

    public static ConsoleCommand Get(string year) {
        return new Registry().Compose(c => {
            c.AddCommand(new AdventOfCode.Y22.Day1());
            c.AddCommand(new AdventOfCode.Y22.Day2());
        });
    }

    public override ICommandResult Execute() {
        List<CommandInfo> CalendarDays = Get("2022").Composer?.Composition.Commands.Select(x => x.Info).ToList() ?? new();
        return this.UseResult(() => ConsolePrompt.GetPrompt().DrawSelectionPrompt("Choise","More", CalendarDays)).ForResult(result => {
            ConsolePrompt.GetRenderer().DrawText($"Got it, you want to solve for {this.Info.Name} Day {result.Data}");
        });
    }
}
