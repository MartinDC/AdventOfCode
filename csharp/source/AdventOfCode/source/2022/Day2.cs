namespace AdventOfCode.Y22;

using AdventOfCode.Cli;
using AdventOfCode.Cli.Annotation;
using AdventOfCode.Cli.Extension;

using static AdventOfCode.Common.InputDataReader;

[CommandAttribute("Solution for Day2")]
public class Day2: ConsoleCommand {
    // A = ROCK, B = PAPER, C = SCISSORS
    // X = ROCK, Y = PAPER, Z = SCISSORS
    
    private Dictionary<string, string> Lookup = new() { 
        ["A"] = "X", 
        ["B"] = "Y",
        ["C"] = "Z",
    };

    private Dictionary<string, string> ScoringCombinations = new() { 
        ["A"] = "Y", // ROCK beat SCISSORS 
        ["B"] = "Z", // PAPER beats ROCK
        ["C"] = "X", // SCISSORS beat PAPER
    };

    public Day2() : base(nameof(Day2)) { }

    public int Solution1(List<(string left, string right)> input) {
        return 0;
    }

    public int Solution2(List<(string left, string right)> input) {
        return 0;
    }

    public override ICommandResult Execute() {
        return this.UseResult(() => ReadInputAsTextAsync("2022", "2").GetAwaiter().GetResult() /*sync*/)
            .ForSolution((input) => SplitTupleOnSeparator(input, " "), Solution1, Solution2);
    }
}