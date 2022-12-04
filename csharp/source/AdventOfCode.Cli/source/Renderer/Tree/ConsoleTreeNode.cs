namespace AdventOfCode.Cli;

public class ConsoleTreeNode {
    public string Markup { get; set; } = default!;
    public ConsoleTreeNode? Parent { get; set; } = default!;
    public List<ConsoleTreeNode>? Nodes { get; set; } = default!;
}