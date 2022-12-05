namespace AdventOfCode.Y22;

using AdventOfCode.Cli;
using AdventOfCode.Cli.Annotation;
using AdventOfCode.Cli.Extension;

using static AdventOfCode.Common.InputDataReader;

[CommandAttribute("Solution for Day2")]
public class Day2: ConsoleCommand {
    public Day2() : base(nameof(Day2)) { }

    // A = ROCK, B = PAPER, C = SCISSORS
    // X = ROCK, Y = PAPER, Z = SCISSORS

    public int Solution1(List<(string left, string right)> input) {
        Dictionary<string, (string sign, int score)> Lookup = new() { ["A"] = ("X", 1), ["B"] = ("Y", 2), ["C"] = ("Z", 3) };
        Dictionary<string, string> Rules = new() { ["A"] = "Y", ["B"] = "Z", ["C"] = "X" };

        return input.Select(round => {
            var wcondition = Rules[round.left];
            var me = Lookup.Values.Single(x => x.sign.Equals(round.right));
            return (wcondition.Equals(round.right) ? 6 : me.sign.Equals(Lookup[round.left].sign) ? 3 : 0 ) + me.score;
        }).Sum();
    }

    public int Solution2(List<(string left, string right)> input) {
        Dictionary<string, int> Score = new() { ["A"] = 1, ["B"] = 2, ["C"] = 3 };
        Dictionary<string, (string sign, int score)> Rules = new() { ["A"] = ("C", 1), ["B"] = ("A", 2), ["C"] = ("B", 3) };

        return input.Select(round => {
            (int score, string token) outcome = round.right switch {
                    "X" => (0, Rules[round.left].sign), // Lost
                    "Y" => (3, round.left), // Draw
                    "Z" => (6, Rules.First(x => x.Value.sign.Equals(round.left)).Key), // Win
                    _ => (0, "")
            };

            var a = 0;
            return outcome.score + Rules[outcome.token].score;
        }).Sum();
    }

    public override ICommandResult Execute() {
        return this.UseResult(() => ReadInputAsTextAsync("2022", "2").GetAwaiter().GetResult() /*sync*/)
            .ForSolution((input) => SplitTupleOnSeparator(input.Replace("\r",string.Empty), " "), Solution1, Solution2);
    }
}