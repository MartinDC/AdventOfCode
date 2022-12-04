namespace AdventOfCode.Y22;

using AdventOfCode.Cli;
using AdventOfCode.Cli.Annotation;
using AdventOfCode.Cli.Extension;

using AdventOfCode.Common;
using static AdventOfCode.Common.InputDataReader;

[CommandAttribute("Solution for Day1")]
public class Day1: ConsoleCommand {
    public Day1() : base(nameof(Day1)) { }

    public int Solution1(List<string> input) {
        var sums = input.Select(l => l.Split("\n").Select(x => int.Parse(x)).Sum());
        return sums.ToList().FindHighestNumber();
    }

    public int Solution2(List<string> input) {
        var sums = input.Select(l => l.Split("\n").Select(x => int.Parse(x)).Sum());
        return sums.OrderByDescending(x => x).Take(3).Sum();
    }

    public override ICommandResult Execute() {
        return this.UseResult(() => ReadInputAsTextAsync("2022", "1").GetAwaiter().GetResult() /*sync*/)
            .ForSolution((input) => SplitStringOnSeparator(input, "\r\n\r\n"), Solution1, Solution2);
    }
}